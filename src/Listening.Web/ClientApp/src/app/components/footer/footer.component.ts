import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'appc-footer',
    styleUrls: ['./footer.component.scss'],
    templateUrl: './footer.component.html'
})
export class FooterComponent implements OnInit {

    public startYear: number = 2015;
    public currentYear: number;

    ngOnInit(): void {
        const currentDate = new Date();
        this.currentYear = currentDate.getFullYear();
    }
}
