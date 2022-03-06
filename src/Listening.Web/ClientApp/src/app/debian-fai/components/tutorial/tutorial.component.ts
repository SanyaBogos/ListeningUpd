import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Status } from '@app/core/models/status';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { Description } from '@app/debian-fai/models/description';
import { ClipboardService } from 'ngx-clipboard';

@Component({
  selector: 'appc-tutorial',
  templateUrl: './tutorial.component.html',
  styleUrls: ['./tutorial.component.scss', '../../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TutorialComponent implements OnInit {

  public descriptions: Description[];
  public isExplaneVisible: boolean;

  constructor(private _ref: ChangeDetectorRef,
    private _clipboardService: ClipboardService,
    private _notificationsService: MyNotificationsService,
  ) {
    this.descriptions = [
      new Description('crt_img', 'qemu-img create debian.img 20G'),
      new Description('emu_cd', 'qemu-system-x86_64 -hda debian.img -cdrom debian-10.7.0-amd64-netinst-1-PRESEEDED.iso -boot d -m 2048 -smp cores=2'),
      new Description('emu_hdd', 'qemu-system-x86_64 -hda debian.img -m 2048 -smp cores=2'),
    ];
    this._ref.markForCheck();
  }

  ngOnInit() {
  }

  copyToClipboard(description: Description) {
    this._clipboardService.copyFromContent(description.command);
    this._notificationsService.notify('copied_to_buffer', Status.Success);
  }

  changeExplanationVisibility() {
    this.isExplaneVisible = !this.isExplaneVisible;
    this._ref.markForCheck();
  }

}
