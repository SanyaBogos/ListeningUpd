import { Subscription } from "rxjs";

export class MySubscriptions {

    private _subscriptions: Subscription;

    constructor() {
        this._subscriptions = new Subscription();
    }

    add(...subscriptions: Subscription[]): void {
        subscriptions.forEach(sub => {
            this._subscriptions.add(sub);
        });
    }

    remove(): void {
        this._subscriptions.unsubscribe();
    }
}