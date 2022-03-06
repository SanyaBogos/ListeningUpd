export class Bukvitsa {
    // private _borderClassName: string = 'border border-primary rounded bg-light-blue';
    private static _defaultColor: string = '#ffffff';

    viewBox: string;
    transform: string;
    className: string;
    bgcolor: string;

    constructor(
        public id: number,
        public width: number,
        public height: number,
        public color: string,
        public size: number,
        public pathD: string[],
        public description?: string,
        public voice?: string,
        public number?: number,
        public name?: string,
        public scale1: number = 0.1,
        public scale2: number = 0.1,
        public isSelected: boolean = false,

    ) {
        this.bgcolor = Bukvitsa._defaultColor;
        this.viewBox = `0 0 ${width} ${height}`;
        this.transform = `translate(0.000000, ${height}) scale(${scale1}00000,-${scale2}00000)`;
        this.className = `size-${this.size}`;
    }

    resetToDefault() {
        this.className = `size-${this.size}`;
        this.bgcolor = Bukvitsa._defaultColor;
    }
}