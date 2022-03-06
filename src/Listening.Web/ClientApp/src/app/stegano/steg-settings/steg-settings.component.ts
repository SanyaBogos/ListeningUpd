import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input, Output, EventEmitter } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { ColorDescription } from '../models/colorDescription';
import { DataOperationsService } from '../data-operations.service';
// import { Simple, Func, Settings } from '../models/settings';
import { Mode } from '../models/mode';
import { DownloadService } from '@app/appshared/download/download.service';
import { Status } from '@app/core/models/status';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { FuncDto, SimpleDto, StegSettingsDto } from 'apiDefinitions';

@Component({
  selector: 'appc-steg-settings',
  templateUrl: './steg-settings.component.html',
  styleUrls: ['./steg-settings.component.scss', '../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class StegSettingsComponent implements OnInit {

  @Input() width: number;
  @Input() height: number;
  @Input() isInject: boolean = false;

  @Output() settingsOut = new EventEmitter<StegSettingsDto>();

  public isSettingsVisible: boolean = false;
  public includeRed: boolean = true;
  public includeGreen: boolean = true;
  public includeBlue: boolean = true;
  public includeAlpha: boolean = true;
  // public modes: string[] = ['', 'simple', 'func'];
  public modes: Mode[] = [/*{ name: '', alias: null },*/ { name: 'simple', alias: 's' }, { name: 'func', alias: 'f' }];
  public selectedMode: Mode;

  // public settings: StegSettingsDto;

  public startIndex: number = 0;
  public step: number = 1;
  public simpleDescription: SimpleDto = new SimpleDto();
  public func: FuncDto;

  // public startIndex: number = 0;
  // public step: number = 1;
  public maxLength: number;


  public allColorDescriptions: ColorDescription[] = [
    { id: 0, name: 'red', className: 'fa-circle text-danger' },
    { id: 1, name: 'green', className: 'fa-circle text-success' },
    { id: 2, name: 'blue', className: 'fa-circle text-primary' },
    { id: 3, name: 'alpha', className: 'fa-genderless' },
  ];

  public selectedColors: ColorDescription[] = [...this.allColorDescriptions];

  constructor(
    private ref: ChangeDetectorRef,
    private dataOperationsService: DataOperationsService,
    private downloadService: DownloadService,
    private notificationsService: MyNotificationsService
  ) { }

  ngOnInit() {
    this.selectedMode = this.modes[0];
  }

  changeSettingsVisibility() {
    this.isSettingsVisible = !this.isSettingsVisible;
    this.ref.markForCheck();
  }

  colorSelectChanged(colorName: string) {
    const isSelected = this.selectedColors && this.selectedColors.length > 0;

    if (isSelected) {
      const indexToRemove = this.selectedColors.findIndex(x => x.name === colorName);

      if (indexToRemove !== -1)
        this.selectedColors.splice(indexToRemove, 1);
      else
        this.addColorToSelected(colorName);
    }
    else {
      this.addColorToSelected(colorName);
    }
  }

  saveKey() {
    // console.log(this.selectedColors);
    // const settings = new Settings();
    // settings.colors = this.selectedColors.map(x => x.id);
    // settings.mode = this.selectedMode.alias;

    // if (this.selectedMode.alias == 's')
    //   settings.simple = this.simpleDescription;
    // else if (this.selectedMode.alias == 'f')
    //   settings.func = this.funcDescription;

    const data = JSON.stringify(this.getSettings());
    const blob = new Blob([data], { type: 'application/octet-stream' });
    // this.fileUrl = this.sanitizer.bypassSecurityTrustResourceUrl(window.URL.createObjectURL(blob));
    let link = URL.createObjectURL(blob);
    // open(link, 'newKey.json');
    this.downloadService.run(link, 'key.kluch');
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.selectedColors, event.previousIndex, event.currentIndex);
  }

  // modeSelected2(item: any){
  //   console.log(item);
  // }

  modeSelected(alias: string) {
    this.selectedMode = this.modes.find(x => x.alias === alias);

    // if (alias === 's') {
    //   this.simpleDescription = new Simple();
    //   this.funcDescription = null;
    // }
    // else if (alias === 'f') {
    //   this.funcDescription = new Func();
    //   this.simpleDescription = null;
    // }
    // else if (alias === 'm') {
    //   this.funcDescription = new Func();
    //   this.simpleDescription = null;
    // }

    switch (alias) {
      case 's':
        this.simpleDescription = new SimpleDto();
        this.simpleDescription.isHorizontal = true;
        this.simpleDescription.isInverted = false;
        this.func = null;
        break;

      case 'f':
        this.func = new FuncDto();
        this.simpleDescription = null;
        break;

      default:
        break;
    }

    this.calculateLength();
  }

  calculateLength() {
    this.maxLength = Math.round((this.dataOperationsService.getMaxLength(this.width, this.height, this.selectedColors.length)
      - this.startIndex) / this.step);
  }

  apply() {
    const settings = this.getSettings();
    // console.log(settings);
    this.settingsOut.emit(settings);
  }

  fileChanged(event: any) {
    const fileList: FileList = event.target.files;
    const self = this;

    if (fileList.length === 1) {
      const file: File = fileList[0];

      const reader = new FileReader();

      reader.onload = (e: any) => {
        const key = JSON.parse(e.target.result) as StegSettingsDto;

        self.selectedColors = [];
        key.colors.forEach(x => self.selectedColors.push(self.allColorDescriptions.find(y => y.id == x)));

        self.startIndex = key.startIndex;
        self.step = key.step;
        self.selectedMode = self.modes.find(x => x.alias == key.mode);

        if (self.selectedMode.alias == 's') {
          // self.simpleDescription = key.simple;
          self.simpleDescription = new SimpleDto();
          self.simpleDescription.isHorizontal = key.simple.isHorizontal;
          self.simpleDescription.isInverted = key.simple.isInverted;

          self.func = null;
        }
        else {
          self.func = new FuncDto();
          self.func.description = key.func.description;
          self.simpleDescription = null;
        }

        self.ref.markForCheck();
      };
      reader.readAsText(file);

      // this.fileUpdated.emit(file);
      this.ref.markForCheck();
    } else if (fileList.length > 1) {
      this.notificationsService.notify(
        'only_one_key', Status.Error, 'to_many_files');
    }
  }

  private addColorToSelected(colorName: string) {
    this.selectedColors.push(this.allColorDescriptions.find(x => x.name === colorName));
  }

  private getSettings(): StegSettingsDto {
    const settings = new StegSettingsDto();
    settings.colors = this.selectedColors.map(x => x.id);
    settings.mode = this.selectedMode.alias;
    settings.startIndex = this.startIndex;
    settings.step = this.step;

    switch (this.selectedMode.alias) {
      case 's':
        settings.simple = this.simpleDescription;
        break;
      case 'f':
        settings.func = this.func;
        break;

      default:
        break;
    }
    
    return settings;
  }


}
