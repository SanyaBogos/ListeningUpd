import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'appc-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ModalComponent implements OnInit {

  public visible = false;
  public visibleAnimate = false;

  constructor(
    private cdRef: ChangeDetectorRef,
  ) { }

  ngOnInit() { }

  public show(): void {
    this.visible = true;
    this.cdRef.markForCheck();

    setTimeout(() => {
      this.visibleAnimate = true;
      this.cdRef.markForCheck();
    }, 100);
  }

  public hide(): void {
    this.visibleAnimate = false;
    this.cdRef.markForCheck();

    setTimeout(() => {
      this.visible = false;
      this.cdRef.markForCheck();
    }, 300);
  }

  public onContainerClicked(event: MouseEvent): void {
    if ((<HTMLElement>event.target).classList.contains('modal')) {
      this.hide();
    }
  }

}
