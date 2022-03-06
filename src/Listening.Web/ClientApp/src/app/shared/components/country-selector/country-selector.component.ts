// tslint:disable:curly
import {
  Component, OnInit, Output, EventEmitter, Input,
  ChangeDetectionStrategy, ChangeDetectorRef, OnDestroy
} from '@angular/core';
import { Country } from '../../models/country';
import { CountriesListService } from '../../services/countries-list.service';
import { FilterSortService } from '../../services/filter-sort.service';
import { QueryViewModel } from 'apiDefinitions';
import { BaseSubscriptionsComponent } from '../base-subscriptions/base-subscriptions.component';

@Component({
  selector: 'appc-country-selector',
  templateUrl: './country-selector.component.html',
  styleUrls: ['./country-selector.component.scss', '../../styles/common.scss'],
  providers: [CountriesListService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CountrySelectorComponent extends BaseSubscriptionsComponent implements OnInit {

  private allCountries: Country[] = [];
  
  public countries: Country[] = [];
  public isListShowed = false;
  public selectedCountry: string = null;
  public translatedTitle: string;
  public searchWord: string;
  public title: string;

  @Input() selectedValue: string;
  @Output() selectedValueChange: EventEmitter<string> = new EventEmitter<string>();

  constructor(
    private cdRef: ChangeDetectorRef,
    public countriesListService: CountriesListService,
    private filterSortService: FilterSortService
  ) {
    super();
    this.countries = countriesListService.getCountries();
    this.allCountries = this.countries.map(e => e);
  }

  ngOnInit() {
    if (this.selectedValue)
      this.selectedCountry = this.countriesListService.getCountry(this.selectedValue);

    const self = this;

    this._subscriptions.add(
      this.filterSortService.query$.subscribe(this._handleQueryChange.bind(this))
    );

    this.cdRef.markForCheck();
  }

  changeShowingState() {
    this.isListShowed = !this.isListShowed;
    this.cdRef.markForCheck();
  }

  searchCountry(event: any) {
    this.countries = this.allCountries.filter(
      (e) => e.value.toLowerCase().indexOf(event.toLowerCase()) > -1);

    this.cdRef.markForCheck();
  }

  selectCountry(country: Country) {
    this.selectedCountry = country.value;
    this.selectedValue = country.id;
    this.selectedValueChange.emit(this.selectedValue);
    this.changeShowingState();
  }

  private _handleQueryChange(queryVM: QueryViewModel): void {
    const { country } = queryVM.filteringProperties;

    if (country == null || country === '')
      this.selectedCountry = null;
  }
}
