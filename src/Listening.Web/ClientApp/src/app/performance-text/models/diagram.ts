export enum DiagramElementType {
    Hidden = 0,
    Hinted,
    Guessed,
    Sign,

    FullyGuessedWords,
    FullyHintedWords,
    PartitiallyGueesedWords
}

export class DiagramDictionary {
    [key: number]: string;
}

export const dictionary: DiagramDictionary = {
    [DiagramElementType.Hidden]: 'hidden',
    [DiagramElementType.Hinted]: 'hinted',
    [DiagramElementType.Guessed]: 'guessed',
    [DiagramElementType.Sign]: 'sign',

    [DiagramElementType.FullyGuessedWords]: 'fully_guessed_words',
    [DiagramElementType.FullyHintedWords]: 'fully_hinted_words',
    [DiagramElementType.PartitiallyGueesedWords]: 'partitially_gueesed_words',
};

export class DiagramElement {
    constructor(public name: string, public value: number) {
        this.name = name;
        this.value = value;
    }
}
