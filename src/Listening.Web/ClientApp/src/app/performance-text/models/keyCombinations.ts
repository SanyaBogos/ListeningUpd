export class KeyCombinations {
    public static tab: number[] = [9];
    public static shiftTab: number[] = [16, 9];
    public static ctrlLeft: number[] = [17, 37];
    public static ctrlRight: number[] = [17, 39];
    public static ctrlSpace: number[] = [17, 32];
    public static altUp: number[] = [18, 38];
    public static altDown: number[] = [18, 40];
    public static ctrlAltSpace: number[] = [17, 18, 32];
    public static shiftSpace: number[] = [16, 32];
}

export class ActionEventCombination {
    constructor(public combination: number[],
        public action: () => void) { }
}

export class ActionEventCombinationDictionary {
    constructor(public name: string,
        public actionEventCombination: ActionEventCombination[]) { }
}
