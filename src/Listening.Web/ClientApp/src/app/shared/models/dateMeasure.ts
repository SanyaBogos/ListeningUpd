export class DateMeasure {
    public days: number;
    public hours: number;
    public minutes: number;
    public seconds: number;

    constructor(ms: number) {
        let d, h, m, s;
        s = Math.floor(ms / 1000);
        m = Math.floor(s / 60);
        s = s % 60;
        h = Math.floor(m / 60);
        m = m % 60;
        d = Math.floor(h / 24);
        h = h % 24;

        this.days = d;
        this.hours = h;
        this.minutes = m;
        this.seconds = s;
    }
}