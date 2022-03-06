import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AppService } from '@app/app.service';
import { BaseSubscriptionsComponent } from '@app/shared/components/base-subscriptions/base-subscriptions.component';
import { QuestionAndWordDescriptionDto } from 'apiDefinitions';
import { ChangeGridService } from '../change-grid.service';

@Component({
  selector: 'appc-list-questions',
  templateUrl: './list-questions.component.html',
  styleUrls: ['./list-questions.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListQuestionsComponent extends BaseSubscriptionsComponent implements OnInit {

  public rows: Array<QuestionAndWordDescriptionDto> = [];

  public columns: Array<any>;
  public page = 1;
  public itemsPerPage = 10;
  public maxSize = 5;
  public numPages = 1;
  public length = 0;

  public config: any;

  public questions: QuestionAndWordDescriptionDto[] = [];


  constructor(
    private _ref: ChangeDetectorRef,
    private _appService: AppService,
    private _changeGridService: ChangeGridService
  ) {
    super();

    const questionName = this._appService.appData.content['quest'];
    const questionFilterName = this._appService.appData.content['fltr_qst'];
    const answerName = this._appService.appData.content['answ'];
    const answerFilterName = this._appService.appData.content['fltr_answ'];
    const directionName = this._appService.appData.content['dirct'];
    const horizontalName = this._appService.appData.content['horiz'];
    const verticalName = this._appService.appData.content['vert'];

    this.columns = [
      { title: questionName, name: 'question', filtering: { filterString: '', placeholder: questionFilterName } },
      { title: answerName, name: 'answer', filtering: { filterString: '', placeholder: answerFilterName } },
      { title: directionName, name: 'direction' },
      { title: horizontalName, name: 'startPointX' },
      { title: verticalName, name: 'startPointY' },
    ];

    this.config = {
      paging: true,
      sorting: { columns: this.columns },
      filtering: { filterString: '' },
      className: ['table-striped', 'table-bordered']
    };

    this._subscriptions.add(
      this._changeGridService.saveQuestionAndAnswer$.subscribe(this._handleWordAdd.bind(this))
    );
  }

  ngOnInit() {
  }


  public changePage(page: any, data: Array<any> = this.questions): Array<any> {
    const start = (page.page - 1) * page.itemsPerPage;
    const end = page.itemsPerPage > -1 ? (start + page.itemsPerPage) : data.length;
    return data.slice(start, end);
  }

  public changeSort(data: any, config: any): any {
    if (!config.sorting) {
      return data;
    }

    const columns = this.config.sorting.columns || [];
    let columnName: string = void 0;
    let sort: string = void 0;

    for (let i = 0; i < columns.length; i++) {
      if (columns[i].sort !== '' && columns[i].sort !== false) {
        columnName = columns[i].name;
        sort = columns[i].sort;
      }
    }

    if (!columnName) {
      return data;
    }

    // simple sorting
    return data.sort((previous: any, current: any) => {
      if (previous[columnName] > current[columnName]) {
        return sort === 'desc' ? -1 : 1;
      } else if (previous[columnName] < current[columnName]) {
        return sort === 'asc' ? -1 : 1;
      }
      return 0;
    });
  }

  public changeFilter(data: any, config: any): any {
    let filteredData: Array<any> = data;
    this.columns.forEach((column: any) => {
      if (column.filtering) {
        filteredData = filteredData.filter((item: any) => {
          return item[column.name].match(column.filtering.filterString);
        });
      }
    });

    if (!config.filtering) {
      return filteredData;
    }

    if (config.filtering.columnName) {
      return filteredData.filter((item: any) =>
        item[config.filtering.columnName].match(this.config.filtering.filterString));
    }

    const tempArray: Array<any> = [];
    filteredData.forEach((item: any) => {
      let flag = false;
      this.columns.forEach((column: any) => {
        if (item[column.name].toString().match(this.config.filtering.filterString)) {
          flag = true;
        }
      });
      if (flag) {
        tempArray.push(item);
      }
    });
    filteredData = tempArray;

    return filteredData;
  }

  public onChangeTable(config: any, page: any = { page: this.page, itemsPerPage: this.itemsPerPage }): any {
    if (config.filtering) {
      Object.assign(this.config.filtering, config.filtering);
    }

    if (config.sorting) {
      Object.assign(this.config.sorting, config.sorting);
    }

    const filteredData = this.changeFilter(this.questions, this.config);
    const sortedData = this.changeSort(filteredData, this.config);
    this.rows = page && config.paging ? this.changePage(page, sortedData) : sortedData;
    this.length = sortedData.length;
  }

  public onCellClick(tableLine: any) {
  }

  private _handleWordAdd(questionAndWordDescriptionDto: QuestionAndWordDescriptionDto) {
    this.questions.push(questionAndWordDescriptionDto);

    if (this.rows.length === 0) {
      this.rows.push(questionAndWordDescriptionDto);
    }

    this._ref.markForCheck();
  }

}
