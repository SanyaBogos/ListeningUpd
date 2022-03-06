import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { ResultClient, ResultDto, UserViewModel } from '../../../apiDefinitions';
import { ResultDtoStringified } from './models/ResultDtoStringified';
import { BuildWordService } from '../../shared/services/build-word.service';
import { MergeTextObject } from '../../performance-text/models/mergeTextObject';
import { Paragraph } from '../../performance-text/models/locators';
import { AccountService } from '@app/core/services';
import { AppService } from '@app/app.service';

@Component({
  selector: 'appc-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.scss',
    '../../shared/styles/common.scss',
    '../../shared/styles/common-btns.scss',
    '../../performance-text/abstract-text/abstract-text.component.scss',
  ],
  providers: [ResultClient],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ResultsComponent implements OnInit {

  public isdetailedResultsVisible = true;
  public selectedTextName: string;
  public paragraphs: Paragraph[] = [];
  public errors: string;
  public rows: Array<any> = [];
  public users: UserViewModel[];
  public selectedUserId: number;
  // public columns: Array<any> = [
  //   { title: 'Name', name: 'name', filtering: { filterString: '', placeholder: 'Filter by name' } },
  //   {
  //     title: 'Position',
  //     name: 'position',
  //     sort: false,
  //     filtering: { filterString: '', placeholder: 'Filter by position' }
  //   },
  //   { title: 'Office', className: ['office-header', 'text-success'], name: 'office', sort: 'asc' },
  //   { title: 'Extn.', name: 'ext', sort: '', filtering: { filterString: '', placeholder: 'Filter by extn.' } },
  //   { title: 'Start date', className: 'text-warning', name: 'startDate' },
  //   { title: 'Salary ($)', name: 'salary' }
  // ];

  public columns: Array<any>;
  public page = 1;
  public itemsPerPage = 10;
  public maxSize = 5;
  public numPages = 1;
  public length = 0;
  public isAdmin = false;

  public config: any;
  public calculatedResultsStringified: ResultDtoStringified[];

  public constructor(
    private _appService: AppService,
    private _ref: ChangeDetectorRef,
    private _resultClient: ResultClient,
    private _buildWordService: BuildWordService,
    private _accountService: AccountService
  ) {

    const titleName = this._appService.appData.content['title_name'];
    const countryName = this._appService.appData.content['country_name'];
    const modeName = this._appService.appData.content['mode_name'];
    const durationName = this._appService.appData.content['duration_name'];
    const guessedName = this._appService.appData.content['guessed_symbols_name'];
    const hintedName = this._appService.appData.content['hinted_symbols_name'];
    const fullyGuessed = this._appService.appData.content['fully_guessed_words'];
    const partitiallyGueesed = this._appService.appData.content['partitially_gueesed_words'];
    const totallyHinted = this._appService.appData.content['fully_hinted_words'];
    const errorsCount = this._appService.appData.content['errors_count'];

    this.columns = [
      { title: '', name: 'isCompleted' },
      { title: titleName, name: 'title', filtering: { filterString: '', placeholder: 'Filter by title' } },
      {
        title: countryName,
        name: 'country',
        className: 'country-flag-in-grid'
      },
      { title: modeName, name: 'mode', sort: '', className: 'text-danger' },
      { title: durationName, name: 'duration' },
      { title: guessedName, name: 'guessedSymbolsCount' },
      { title: hintedName, name: 'hintedSymbolsCount' },
      { title: fullyGuessed, name: 'fullyGuessedWordsCount' },
      { title: partitiallyGueesed, name: 'partitionallyGuessedWordsCount' },
      { title: totallyHinted, name: 'totallyHintedWordsCount' },
      { title: errorsCount, name: 'errorsCount' }
    ];

    this.config = {
      paging: true,
      sorting: { columns: this.columns },
      filtering: { filterString: '' },
      className: ['table-striped', 'table-bordered']
    };

    this.isAdmin = this._accountService.isAdmin() || this._accountService.isSuper();
    this.selectedUserId = parseInt(this._accountService.user.sub, null);
    this._getResults();
  }

  public ngOnInit() { }

  changePage(page: any, data: Array<any> = this.calculatedResultsStringified): Array<any> {
    const start = (page.page - 1) * page.itemsPerPage;
    const end = page.itemsPerPage > -1 ? (start + page.itemsPerPage) : data.length;
    return data.slice(start, end);
  }

  changeSort(data: any, config: any): any {
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

  changeFilter(data: any, config: any): any {
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

  onChangeTable(config: any, page: any = { page: this.page, itemsPerPage: this.itemsPerPage }): any {
    if (config.filtering) {
      Object.assign(this.config.filtering, config.filtering);
    }

    if (config.sorting) {
      Object.assign(this.config.sorting, config.sorting);
    }

    const filteredData = this.changeFilter(this.calculatedResultsStringified, this.config);
    const sortedData = this.changeSort(filteredData, this.config);
    this.rows = page && config.paging ? this.changePage(page, sortedData) : sortedData;
    this.length = sortedData.length;
  }

  onCellClick(tableLine: any) {
    const row = tableLine.row as ResultDtoStringified;
    const self = this;
    this.selectedTextName = row.title;

    this._resultClient.getDetailedResult(row.id)
      .subscribe(data => {
        const mergeTextObject = new MergeTextObject(
          data.mergedText, data.resultsEncodedString
        );
        self.paragraphs = self._buildWordService.getMergedWords(mergeTextObject);

        let errors: string[] = [];

        if (data.errorsForJoined && data.errorsForJoined.length > 0)
          errors = data.errorsForJoined;
        else if (data.errorsForSeparated && data.errorsForSeparated.length > 0)
          data.errorsForSeparated.forEach(x => x.errors && errors.length > 0 && errors.push(...x.errors));

        self.errors = errors.join(' ');

        self._ref.markForCheck();
      });
  }

  changeDetailedResultsVisibility() {
    this.isdetailedResultsVisible = !this.isdetailedResultsVisible;
  }

  userSelected() {
    this._getResults();
    this.selectedTextName = null;
    this.paragraphs = [];
    this.errors = null;
  }

  private _getResults() {
    this._resultClient.getAllUserTextResults(this.selectedUserId)
      .subscribe((data) => {
        this.calculatedResultsStringified = this._formatCalculatedData(data.results);
        this.users = data.users;
        this.length = this.calculatedResultsStringified.length;
        this.onChangeTable(this.config);
        this._ref.markForCheck();
      });
  }

  private _formatCalculatedData(resultDtos: ResultDto[]): ResultDtoStringified[] {
    const resultDtosStringified: ResultDtoStringified[] = [];
    resultDtos.forEach(result => {
      const resultStringified = new ResultDtoStringified(result);
      resultDtosStringified.push(resultStringified);
    });

    return resultDtosStringified;
  }

}
