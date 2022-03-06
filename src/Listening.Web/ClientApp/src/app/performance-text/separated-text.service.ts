// tslint:disable:curly
import { Injectable } from '@angular/core';
import { Paragraph, WordEnhanced, Position } from './models/locators';

@Injectable()
export class SeparatedTextService {

  constructor() { }

  findFirstPosition(paragraphs: Paragraph[]) {
    for (let i = 0; i < paragraphs.length; i++) {
      for (let j = 0; j < paragraphs[i].words.length; j++) {
        const word = (paragraphs[i].words[j] as WordEnhanced);
        if (!word.isGuessed)
          return new Position(i, j);
      }
    }

    return new Position(0, 0);
  }

  findNext(paragraphs: Paragraph[], position: Position): Position {
    ++position.wordIndex;
    let result = this.findFromPosition(paragraphs, position);
    if (!result)
      result = this.findFromPosition(paragraphs, new Position(0, 0));
    return result;
  };

  findPrev(paragraphs: Paragraph[], position: Position): Position {
    --position.wordIndex;
    let result = this.reverseFindFromPosition(paragraphs, position);

    if (!result) {
      const lastParagraphIndex = paragraphs.length - 1;
      const lastWordIndex = paragraphs[lastParagraphIndex].words.length - 1;
      result = this.reverseFindFromPosition(paragraphs,
        new Position(lastParagraphIndex, lastWordIndex));
    }

    return result;
  };

  findFromPosition(paragraphs: Paragraph[], position: Position): Position {
    // tslint:disable-next-line:prefer-const
    let { paragraphIndex, wordIndex } = position;

    for (let i = paragraphIndex; i < paragraphs.length; i++) {
      for (let j = wordIndex; j < paragraphs[i].words.length; j++) {
        const word = paragraphs[i].words[j] as WordEnhanced;
        if (!word.isGuessed && !word.isSign)
          return new Position(i, j);
      }
      wordIndex = 0;
    }

    return null;
  };

  reverseFindFromPosition(paragraphs: Paragraph[], position: Position): Position {
    let iIndex;
    // tslint:disable-next-line:prefer-const
    let { paragraphIndex, wordIndex } = position;

    for (let i = paragraphIndex; i >= 0; i--) {
      for (let j = wordIndex; j >= 0; j--) {
        const word = paragraphs[i].words[j] as WordEnhanced;
        if (!word.isGuessed && !word.isSign)
          return new Position(i, j);
      }

      iIndex = i === 0 ? paragraphs.length : i;
      wordIndex = paragraphs[iIndex - 1].words.length - 1;
    }

    return null;
  };

}
