import { IMyDateModel } from "angular-mydatepicker";

export type DateName = 'app_created' | 'app_updated';
export type DateType = '<' | '>' | '=';

export class DateFilter {
    public static createdName: DateName = 'app_created';
    public static updatedName: DateName = 'app_updated';

    // public static less = '<';
    // public static more = '>';
    // public static eq = '=';

    // public name: 'app_created' | 'app_updated';
    // public type: '<' | '>' | '=';
    // public date: IMyDateModel;

    constructor(
        public name: DateName,
        public type: DateType,
        public date: IMyDateModel
    ) {
        this.name = name;
        this.type = type;
        this.date = date;
    }
}