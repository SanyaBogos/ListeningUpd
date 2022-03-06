import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class UtilityService {
    private lastTriedUrlArray: string[] = [];

    constructor(private _router: Router) { }

    public convertDateTime(date: Date) {
        const _formattedDate = new Date(date.toString());
        return _formattedDate.toDateString();
    }

    public navigate(path: string) {
        this._router.navigate([path]);
    }

    public navigateToSignIn() {
        // this.navigate('/login');
        this._router.navigate(['/login']);
    }

    public navigateToLastUrl() {
        const lastUrl = this.lastTriedUrlArray.length > 0
            ? this.lastTriedUrlArray : [''];
        this._router.navigate(lastUrl);
        this.lastTriedUrlArray.length = 0;
    }

    public saveLastUrl() {
        const lastTriedUrl =
            this._router.url.split('%20').join(' ')
                .split('%3A').join(':')
                .split('%2C').join(',');

        const lastTriedUrlArray =
            lastTriedUrl.split('/').filter(n => n);

        lastTriedUrlArray
            .forEach((e, i, arr) => arr[i] = e.split('%2F').join('/'));

        lastTriedUrlArray[0] = `/${lastTriedUrlArray[0]}/${lastTriedUrlArray[1]}`;
        lastTriedUrlArray.splice(1, 1);

        this.lastTriedUrlArray = lastTriedUrlArray;
    }
}
