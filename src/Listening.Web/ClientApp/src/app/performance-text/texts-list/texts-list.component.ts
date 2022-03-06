// tslint:disable:curly
import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { TextClient, TextDescriptionDto, TextQueryViewModel, PagedDataViewModelOfTextDescriptionDto } from '../../../apiDefinitions';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { FilterSortService } from '@app/shared/services/filter-sort.service';

@Component({
    selector: 'appc-texts-list',
    templateUrl: './texts-list.component.html',
    styleUrls: ['./texts-list.component.scss', '../../shared/styles/common.scss'],
    providers: [TextClient],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TextsListComponent implements OnInit {

    private p = 1;
    private itemPerPage = 10;
    private query: TextQueryViewModel;

    public textDescriptions$: Observable<PagedDataViewModelOfTextDescriptionDto>;
    public currentType = 'j';

    constructor(
        private cdRef: ChangeDetectorRef,
        protected textClient: TextClient,
        protected router: Router,
        protected filterSortService: FilterSortService
    ) {
        filterSortService.query$
            .subscribe(data => {
                Object.assign(this.query, data);
                this.textDescriptions$ = this.textClient.getTexts(this.query);
                this.cdRef.markForCheck();
            });
    }

    ngOnInit() {
        this.query = new TextQueryViewModel();
        this.query.page = this.p;
        this.query.isAscending = true;
        this.query.elementsPerPage = this.itemPerPage;

        this.refreshData();
    }

    choseText(textDescription: TextDescriptionDto /*, currentTextFormType: string*/) {

        const listeningType = textDescription.audioName ? 'a' : 'v';
        const fileName = textDescription.audioName
            ? textDescription.audioName : textDescription.videoName;
        const paramsArray = [/*currentTextFormType,*/ this.currentType, listeningType, textDescription.textId,
        textDescription.title, fileName, textDescription.subTitle].filter(n => n);

        this.router.navigate([`perfTexts`, ...paramsArray]);
    }

    pageChanged(page: number) {
        this.p = page;
        this.query.page = page;
        this.textDescriptions$ = this.textClient.getTexts(this.query);
        this.cdRef.markForCheck();
    }

    refreshData() {
        this.textDescriptions$ = this.textClient.getTexts(this.query);
        this.cdRef.markForCheck();
    }
}
