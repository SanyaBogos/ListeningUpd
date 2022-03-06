import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { VideoDescription } from '@app/appshared/models/videoDescrption';
import functionPlot from 'function-plot';
import { FunctionPlotDatum } from 'function-plot/dist/types';

@Component({
  selector: 'appc-plot',
  templateUrl: './plot.component.html',
  styleUrls: ['./plot.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlotComponent implements OnInit {

  public funcs: FunctionPlotDatum[];

  public xFrom: number = -10;
  public xTo: number = 10;
  public yFrom: number = -10;
  public yTo: number = 10;
  public isGrid: boolean = true;

  public videoDescriptions: VideoDescription[];

  constructor(
    private _ref: ChangeDetectorRef
  ) {
    const basePath = 'intro-video/mth/';

    this.videoDescriptions = [
      { name: 'mth_plot', src: `${basePath}1-plot`, isAllowed: true, type: 'webm' },
      { name: 'mth_grid', src: `${basePath}2-grd-rngs`, isAllowed: true, type: 'webm' },
    ];

    this.funcs = [{
      fn: '-x^2+2*x+3',

    }];
  }

  ngOnInit() {
    this.createPlot();
  }

  createPlot() {
    console.log(this.funcs);
    functionPlot({
      target: '#plotGraph',
      grid: this.isGrid,
      xAxis: { domain: [this.xFrom, this.xTo] },
      yAxis: { domain: [this.yFrom, this.yTo] },
      data: this.funcs
      // data: [{
      //   fn: this.func,
      //   color: this.color
      // }]
    });
    this._ref.markForCheck();
  }

  addFunc() {
    this.funcs.push({});
    this._ref.markForCheck();
  }

  removeFunc(item: FunctionPlotDatum) {
    // this.funcs = this.funcs.filter(x => x.fn !== item.fn);
    const i = this.funcs.indexOf(item);
    // console.log(i);
    this.funcs.splice(i, 1);
    this._ref.markForCheck();
  }

}
