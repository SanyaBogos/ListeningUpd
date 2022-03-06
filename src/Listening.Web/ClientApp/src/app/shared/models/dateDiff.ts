import { DateMeasure } from './dateMeasure';

export class DateDiff {
    public days: number;
    public hours: number;
    public minutes: number;
    public seconds: number;
    public date1: Date;
    public date2: Date;


    constructor(date1: Date, date2: Date) {
        this.days = null;
        this.hours = null;
        this.minutes = null;
        this.seconds = null;
        this.date1 = date1;
        this.date2 = date2;

        this.init();
    }

    private init() {
        const data = new DateMeasure(+this.date1 - +this.date2);
        this.days = data.days;
        this.hours = data.hours;
        this.minutes = data.minutes;
        this.seconds = data.seconds;
    }

    public toString(): string {
        const days = `${this.days ? this.days + ' days' : ''}`;
        const hours = `${this.hours ? this.hours : '00'}`;
        const minutes = `${this.minutes ? this.minutes : '00'}`;
        const seconds = `${this.seconds ? this.seconds : '00'}`;

        return `${days} ${hours}:${minutes}:${seconds}`;
    }
}
