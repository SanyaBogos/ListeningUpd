export class KeyStrCombinations {
    public static tab: string[] = ['Tab'];
    public static shiftTab: string[] = ['Shift', 'Tab'];
    public static ctrlLeft: string[] = ['Control', 'ArrowLeft'];
    public static ctrlRight: string[] = ['Control', 'ArrowRight'];
    public static ctrlSpace: string[] = ['Control', ' '];
    public static altUp: string[] = ['Alt', 'ArrowUp'];
    public static altDown: string[] = ['Alt', 'ArrowDown'];
    public static ctrlAltSpace: string[] = ['Control', 'Alt', ' '];
    public static shiftSpace: string[] = ['Shift', ' '];
}

export class ActionStrEventCombination {
    constructor(public combination: string[],
        public action: () => void) { }
}

export class ActionStrEventCombinationDictionary {
    constructor(public name: string,
        public actionEventCombination: ActionStrEventCombination[]) { }
}
