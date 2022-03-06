// tslint:disable:curly
import { Injectable } from '@angular/core';
import { EncodedType } from '../../performance-text/models/encodedType';
// import { TextForGuessingDto, ErrorForSeparatedDto } from '../apiDefinitions';
import { Letter, Word, Paragraph, WordEnhanced } from '../../performance-text/models/locators';
import { LetterCssTypeName } from '../../performance-text/models/cssClasses';
import { MergeTextObject } from '../../performance-text/models/mergeTextObject';
import { Counts } from '../../performance-text/models/counts';
import { ChartEventsService } from './chart-events.service';
import { Subject } from 'rxjs';
import * as _ from 'underscore';
import { TextForGuessingDto } from '../../../apiDefinitions';

@Injectable()
export class BuildWordService {

  private startOverSource = new Subject();

  startOver$ = this.startOverSource.asObservable();

  failedAttempts: string[];

  constructor(
    private chartEventsService: ChartEventsService
  ) { }

  announceStartOver() {
    this.startOverSource.next();
  }

  getHiddenWords(textForGuessingDto: TextForGuessingDto,
    type: 'j' | 's' | 'p' = 'j'): Paragraph[] {

    let maxIndex = 0;
    let wordsCountWithoutSign = 0;
    const paragraphs: Paragraph[] = [];
    // const self = this;
    const isJoined = type === 'j';
    const counts: Counts = new Counts();

    textForGuessingDto.wordsCounts.forEach((paragraph, index) => {
      const paragraphObject = new Paragraph(maxIndex++);

      paragraph.forEach(symbol => {
        const lettersCount = Number(symbol);
        const isSign = isNaN(lettersCount);
        const word = isJoined
          ? new Word(maxIndex++)
          : new WordEnhanced(maxIndex++);

        if (!isSign) {
          word.letters = _.range(1, lettersCount + 1)
            .map(e => new Letter(maxIndex, e.toString(), LetterCssTypeName.hiddenSymbolClass));
          counts.hiddenCount += lettersCount;
          wordsCountWithoutSign++;
        } else {
          word.letters = [new Letter(maxIndex, symbol, LetterCssTypeName.signSymbolClass)];
          counts.signCount++;
        }

        if (!isJoined)
          (word as WordEnhanced).isSign = isSign;

        paragraphObject.words.push(word);
      });

      paragraphs.push(paragraphObject);
    });

    counts.wordsCount = wordsCountWithoutSign;

    if (isJoined)
      counts.errorCount = textForGuessingDto.errorsForJoined && textForGuessingDto.errorsForJoined.length || 0;
    else
      counts.errorCount = textForGuessingDto.errorsForSeparated && textForGuessingDto.errorsForSeparated.length
        && textForGuessingDto.errorsForSeparated.map(x => x.errors.length)
          .reduce((acc, curr) => acc + curr)
        || 0;

    this.chartEventsService.announceCountsCalculated(counts);

    return paragraphs;
  }

  getMergedWords(mergeTextObject: MergeTextObject): Paragraph[] {
    const { mergedText, resultsEncodedString, errorsForSeparated, type,
      errorsForJoined } = mergeTextObject;

    let maxIndex = 0;
    const paragraphs: Paragraph[] = [];
    const self = this;
    const isJoined = type === 'j';
    const counts: Counts = new Counts();

    mergedText.forEach((paragraph) => {
      const paragraphObject = new Paragraph(maxIndex++);

      paragraph.forEach((word) => {
        const wordObject = isJoined
          ? new Word(maxIndex++)
          : new WordEnhanced(maxIndex++);

        word.forEach((letter) => {
          const code = resultsEncodedString.splice(0, 2);

          if (_.isEqual(code, EncodedType.sign))
            (wordObject as WordEnhanced).isSign = true;

          this.calculateCounts(counts, code);

          wordObject.letters.push(new Letter(maxIndex++, letter,
            self.getClassName(code)));
        });

        if (!isJoined) {
          const isGuessed = !_.any(wordObject.letters, e =>
            e.className === LetterCssTypeName.hiddenSymbolClass
          );
          (wordObject as WordEnhanced).isGuessed = isGuessed;
        }

        paragraphObject.words.push(wordObject);
      });

      paragraphs.push(paragraphObject);
    });

    if (!isJoined)
      errorsForSeparated.forEach(e => {
        const { paragraphIndex, wordIndex } = e.wordAddress;
        const word = (paragraphs[paragraphIndex].words[wordIndex] as WordEnhanced);
        word.failedAttempts = e.errors;
        word.tooltipClassNames.push(`margin-top-${word.failedAttempts.length}`);
        counts.errorCount += e.errors.length;
      });
    else
      counts.errorCount = errorsForJoined.length;

    counts.wordsCount =
      paragraphs.map(x => x.words.filter(y => !(y as WordEnhanced).isSign).length)
        .reduce((a, b) => a + b, 0);

    this.chartEventsService.announceCountsCalculated(counts);

    return paragraphs;
  }

  private calculateCounts(counts: Counts, code: boolean[]) {
    if (_.isEqual(code, EncodedType.hidden))
      counts.hiddenCount++;
    else if (_.isEqual(code, EncodedType.hinted))
      counts.hintedCount++;
    else if (_.isEqual(code, EncodedType.sign))
      counts.signCount++;
    else
      counts.guessedCount++;
  }

  private getClassName(type: boolean[]): string {
    if (_.isEqual(type, EncodedType.hidden)) {
      return LetterCssTypeName.hiddenSymbolClass;
    } else if (_.isEqual(type, EncodedType.sign)) {
      return LetterCssTypeName.signSymbolClass;
    } else if (_.isEqual(type, EncodedType.hinted)) {
      return LetterCssTypeName.hintedSymbolClass;
    } else {
      return LetterCssTypeName.guessedSymbolClass;
    }
  }

}
