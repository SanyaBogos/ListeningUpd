export class Identicable {
    constructor(public readonly id: number) {
        id = id;
    }
}

export class Letter extends Identicable {
    // constructor(id: number, public symbol: string, public isSign: boolean) {
    constructor(id: number, public symbol: string, public className: string) {
        super(id);
        symbol = symbol;
        // isSign = isSign;
        className = className;
    }
}

export class Word extends Identicable {
    letters: Letter[] = [];
}

export class WordEnhanced extends Word {
    isGuessed = false;
    isSign = false;
    failedAttempts: string[] = [];
    tooltipClassNames: string[] = ['tooltip-inner'];
}

export class Paragraph extends Identicable {
    words: Word[] = [];
}

class AbstractDeepClone {
    deepClone() {
        return JSON.parse(JSON.stringify(this));
    }
}

export class Locator extends AbstractDeepClone {
    constructor(public textId: string, public paragraphIndex: number,
        public wordIndex: number, public type: string) {
        super();
    }
}

export class Position extends AbstractDeepClone {
    constructor(public paragraphIndex: number, public wordIndex: number) {
        super();
    }
}

