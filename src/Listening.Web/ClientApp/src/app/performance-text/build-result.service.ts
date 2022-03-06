// tslint:disable:curly
import { Injectable } from '@angular/core';
import { Paragraph } from './models/locators';
import { LetterCssTypeName } from './models/cssClasses';
import { DiagramElement, DiagramElementType } from './models/diagram';
import { TranslateTextService } from './translate-text.service';

@Injectable()
export class BuildResultService {

  constructor(public translationService: TranslateTextService) { }

  buildWordResults(paragraphs: Paragraph[]): DiagramElement[] {
    let fullyGuessedCount = 0;
    let fullyHinted = 0;
    let partitiallyGuessed = 0;

    paragraphs.forEach(paragraph => {
      paragraph.words.forEach(word => {
        if (word.letters[0].className === LetterCssTypeName.signSymbolClass)
          return;

        if (word.letters.every(letter => letter.className === LetterCssTypeName.guessedSymbolClass))
          fullyGuessedCount++;
        else if (word.letters.every(letter => letter.className === LetterCssTypeName.hintedSymbolClass))
          fullyHinted++;
        else
          partitiallyGuessed++;
      });
    });

    return [
      this.translationService.getDiagramElement(DiagramElementType.FullyGuessedWords, fullyGuessedCount),
      this.translationService.getDiagramElement(DiagramElementType.FullyHintedWords, fullyHinted),
      this.translationService.getDiagramElement(DiagramElementType.PartitiallyGueesedWords, partitiallyGuessed)
    ];
  }

}
