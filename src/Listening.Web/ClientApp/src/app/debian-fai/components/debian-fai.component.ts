import { Component, OnInit, ChangeDetectorRef, ChangeDetectionStrategy } from '@angular/core';
import {
  DebianFAIClient, PreseedSettingsViewModel, DeviceType, PartitionType,
  PartitionConfig, FileSystemType, ArchitectureType, ImageConfig, CaptchaCheckDto
} from 'apiDefinitions';
import { HttpClient } from '@angular/common/http';
import { saveAs } from 'file-saver';
import { DeviceTypeItem } from '../models/device-type';
import { DiagramElement } from '@app/performance-text/models/diagram';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { GenerateObjectByEnumService } from '@app/shared/services/generate-object-by-enum.service';
import { PartitionTypeItem } from '../models/partition-type';
import { FileSystemTypeItem } from '../models/file-system-type';
import { GeneratePreseedService } from '../services/generate-preseed.service';
import { VideoDescription } from '@app/appshared/models/videoDescrption';
import { ArchitectureWithVersionsService } from '../services/architecture-with-versions.service';
import { AppService } from '@app/app.service';
import { ArchitectureTypeItem } from '../models/architecture-type';


@Component({
  selector: 'appc-debian-fai',
  templateUrl: './debian-fai.component.html',
  styleUrls: ['./debian-fai.component.scss', '../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [GeneratePreseedService]
})
export class DebianFaiComponent implements OnInit {

  public diagram = class {
    public static data: DiagramElement[];
    public static view: number[] = [200, 150];
    public static showLegend = false;
    public static showLabels = false;
    public static explodeSlices = false;
    public static doughnut = false;
  };

  public videoDescriptions: VideoDescription[];
  public preseed: string;
  public settings: PreseedSettingsViewModel;
  public isDebSettingsVisible = false;
  public isPreseedVisible = false;
  public isVideoTutorialVisible = true;
  public isHddSplitVisible = false;
  public isShowEdit: boolean = false;

  public deviceTypes: DeviceTypeItem[] = [];
  public allArchitectureTypes: ArchitectureTypeItem[] = [];
  public architectureTypes: ArchitectureTypeItem[] = [];
  public partitionTypes: PartitionTypeItem[] = [];
  public fileSystemTypes: FileSystemTypeItem[] = [];
  public deviceTypeIdSelected: number;
  // public architectureTypeIdSelected: number;
  public partitionTypeIdSelected: number;
  public fileSystemTypeIdSelected: number;

  public versionNames: string[];

  public selectedVersion: string;
  public selectedArchitecture: ArchitectureType;
  public selectedFolderName: string;
  public selectedImageName: string;

  public currentHDDPartitionConfig: PartitionConfig;

  private _currentShortVersionName: string;
  private _currentVersionName: string;
  private _defaultHddPartitions: PartitionConfig[];
  private _allPartitionTypes: PartitionTypeItem[] = [];

  constructor(
    private _ref: ChangeDetectorRef,
    private _http: HttpClient,
    private _debianFAIClient: DebianFAIClient,
    private _appService: AppService,
    private _generateObjectByEnumService: GenerateObjectByEnumService,
    private _generatePreseedService: GeneratePreseedService,
    private _architectureWithVersionsService: ArchitectureWithVersionsService
  ) {
    const basePath = 'intro-video/deb/';

    this.videoDescriptions = [
      { name: 'deb_conf_img', src: `${basePath}0-deb-setup-prsd`, type: 'mp4', isAllowed: true },
      { name: 'deb_inst', src: `${basePath}1-deb-inst`, type: 'mp4', isAllowed: true },
      { name: 'deb_aftr_inst', src: `${basePath}2-deb-after-inst`, type: 'mp4', isAllowed: true },
    ];

    this.versionNames = this._architectureWithVersionsService.getVersionNames();
    this._currentShortVersionName = 'crnt';
    this._currentVersionName = this._appService.appData.content[this._currentShortVersionName];
    this.versionNames.unshift(this._currentVersionName);
    this.selectedVersion = this.versionNames[0];
  }

  ngOnInit() {
    this.currentHDDPartitionConfig = new PartitionConfig();
    this.deviceTypes = this._generateObjectByEnumService.getItems(Object.keys(DeviceType));
    this.allArchitectureTypes = this._generateObjectByEnumService.getItems(Object.keys(ArchitectureType), true);
    this._allPartitionTypes = this._generateObjectByEnumService.getItems(Object.keys(PartitionType));
    // this.architectures = this._generateObjectByEnumService.getItems(Object.keys(Arhitecture));
    this.fileSystemTypes = this._generateObjectByEnumService.getItems(Object.keys(FileSystemType));

    this.deviceTypeIdSelected = this.deviceTypes[0].id;

    const self = this;

    this._debianFAIClient.getDefaultSettings()
      .subscribe(settings => {
        self.settings = settings;
        self.settings.imageConfig = new ImageConfig();
        self.selectedVersion = self.versionNames[0];
        self.settings.imageConfig.name = self._currentShortVersionName;

        self.diagram.data = settings.hddSplitSettingsVM.configs.map(x => new DiagramElement(PartitionType[x.partitionType], x.size));
        self._defaultHddPartitions = JSON.parse(JSON.stringify(settings.hddSplitSettingsVM.configs));
        self._generatePreseed();

        self._setArchitectures(settings.currentArchitectures);
        self._ref.markForCheck();
      });
  }

  changeDebSettingsVisibility() {
    this.isDebSettingsVisible = !this.isDebSettingsVisible;
    this._ref.markForCheck();
  }

  changePreseedVisibility() {
    this.isPreseedVisible = !this.isPreseedVisible;
    this._ref.markForCheck();
  }

  changeHddSplitVisibility() {
    this.isHddSplitVisible = !this.isHddSplitVisible;
    this._ref.markForCheck();
  }

  plus() {
    this.isShowEdit = !this.isShowEdit;
    const partitionIds = this.settings.hddSplitSettingsVM.configs.map(x => x.partitionType);
    this.partitionTypes = this._allPartitionTypes.filter(x => !(partitionIds.indexOf(x.id) > -1));
    this._ref.markForCheck();
  }

  changeVideoTutorialVisibility() {
    this.isVideoTutorialVisible = !this.isVideoTutorialVisible;
    this._ref.markForCheck();
  }

  getEnumName(partitionType: PartitionType) {
    return PartitionType[partitionType];
  }

  editConfig(hddSplitConfig: PartitionConfig) {
    this.isShowEdit = true;
    this.partitionTypes.push(this._allPartitionTypes.find(x => x.id === hddSplitConfig.partitionType));
    this.currentHDDPartitionConfig = hddSplitConfig;
    this._ref.markForCheck();
  }

  isDisabled(partitionType: PartitionType) {
    return PartitionType.Boot === partitionType;
  }

  canClose(partitionType: PartitionType) {
    return partitionType !== PartitionType.Boot && partitionType !== PartitionType.Swap
      && partitionType !== PartitionType.Root;
  }

  removePartition(config: PartitionConfig) {
    this.partitionTypes.push(this._allPartitionTypes.find(x => x.id === config.partitionType));
    this.settings.hddSplitSettingsVM.configs =
      this.settings.hddSplitSettingsVM.configs.filter(x => x.partitionType !== config.partitionType);
  }

  restoreToDefaultPartitions() {
    this.settings.hddSplitSettingsVM.configs = this._defaultHddPartitions;
    this.currentHDDPartitionConfig = new PartitionConfig();
    this.diagram.data = this.settings.hddSplitSettingsVM.configs.map(x => new DiagramElement(PartitionType[x.partitionType], x.size));
    this._ref.markForCheck();
  }

  acceptHddPartitions() {
    this.diagram.data = this.settings.hddSplitSettingsVM.configs.map(x => new DiagramElement(PartitionType[x.partitionType], x.size));
    this._generatePreseed();
    this._ref.markForCheck();
  }

  isAddPartitionDisabled() {
    return !(this.currentHDDPartitionConfig.fileSystemType != null
      && this.currentHDDPartitionConfig.partitionType != null
      && this.currentHDDPartitionConfig.size > 100);
  }

  addPartition() {
    const isExist = this.settings.hddSplitSettingsVM.configs.map(x => x.partitionType).indexOf(this.currentHDDPartitionConfig.partitionType) > -1

    if (!isExist)
      this.settings.hddSplitSettingsVM.configs.push(this.currentHDDPartitionConfig);

    this.isShowEdit = false;
    this.currentHDDPartitionConfig = new PartitionConfig();
    this._ref.markForCheck();
  }


  getImage() {
    const self = this;
    this.settings.deviceType = this.deviceTypeIdSelected;

    if (this.selectedArchitecture)
      this.settings.imageConfig.architectureType = this.selectedArchitecture;
    /*this.settings.architecture = this.selectedArchitecture;
    this.settings.version = this.selectedVersion !== this._currentVersionName ? this.selectedVersion : this._currentShortVersionName;
    this.settings.folderName =
      this.settings.imageName = this.selectedImageName;*/

    this._debianFAIClient.getImage(this.settings).subscribe(
      res => {
        self._downloadFile(res);
      }
    );
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.settings.hddSplitSettingsVM.configs, event.previousIndex, event.currentIndex);
    this._ref.markForCheck();
  }

  updateCaptchaObj(captcha: CaptchaCheckDto) {
    this.settings.captcha = captcha;
  }

  versionChanged(val: string) {

    if (val === this._currentVersionName)
      return;

    if (val === this._currentVersionName) {
      this.selectedVersion = this.versionNames[0];
      this.settings.imageConfig.name = this._currentShortVersionName;
    }
    else {
      this.settings.imageConfig.name = this.selectedVersion = val;
    }

    const architectures = this._architectureWithVersionsService.getArchitectures(val);
    this._setArchitectures(architectures);

    this.settings.imageConfig.urlType = this._architectureWithVersionsService.getUrlType(this.selectedVersion);
  }

  private _generatePreseed() {
    const pressed = this._generatePreseedService.getPreseed(this.settings);
    this.preseed = pressed;
  }

  private _downloadFile(fileName: string): void {
    this._http.get(`api/DebianFAI/downloadFile/${fileName}`, { responseType: 'blob' })
      .subscribe(result => {
        const blob = result as Blob;
        saveAs(blob, fileName);
      }, err => {
        console.log(err);
      });
  }

  private _setArchitectures(architectures: ArchitectureType[]) {
    if (!architectures || architectures.length === 0)
      return;

    this.architectureTypes = this.allArchitectureTypes.filter(x => architectures.includes(x.id));
    this.settings.imageConfig.architectureType = this.selectedArchitecture = this.architectureTypes[0].id;
  }
}
