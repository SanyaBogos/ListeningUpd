import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'appc-complexity',
  templateUrl: './complexity.component.html',
  styleUrls: ['./complexity.component.scss', '../../styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ComplexityComponent implements OnInit {

  @Input() complexity: number;
  @Output() complexitySelected = new EventEmitter<number>();

  public complexityArray: number[] = [1, 2, 3, 4, 5];

  constructor(public ref: ChangeDetectorRef) { }

  ngOnInit() { }

  getRectClass(complexityValue: number) {
    let classValue = 'base-rect-values color-' + complexityValue;

    if (this.complexity !== 0 && this.complexity === complexityValue)
      classValue += ' stroke-5';

    return classValue;
  }

  selected(complexityValue: number) {
    this.complexity = complexityValue;
    this.complexitySelected.emit(complexityValue);
  }

  clear() {
    this.complexity = 0;
    this.complexitySelected.emit(0);
  }

}
