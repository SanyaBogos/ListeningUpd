import { DateDiff } from '../../../shared/models/dateDiff';
import { ResultDto } from '../../../../apiDefinitions';

export class ResultDtoStringified {
    id: number;
    title: string;
    country: string;
    calculatedResult: string;
    mode: string;
    duration: string;
    guessedSymbolsCount: string;
    hintedSymbolsCount: string;
    fullyGuessedWordsCount: string;
    partitionallyGuessedWordsCount: string;
    totallyHintedWordsCount: string;
    isCompleted: string;
    errorsCount: number;

    constructor(resultDto: ResultDto) {
        this.id = resultDto.id;
        this.title = resultDto.title ? resultDto.title : '';
        this.country = `<span class="country-flag-in-grid flag flag-${resultDto.country.toLocaleLowerCase()}"></span>`;
        this.mode = resultDto.mode === 'j' ? 'Joined' : 'Separated';

        let diff: string;

        if (resultDto.timeSpentMiliSeconds)
            diff = this._toTime(resultDto.timeSpentMiliSeconds, false); // new Date(resultDto.timeSpentMiliSeconds).toString();
        else
            diff = new DateDiff(resultDto.finished, resultDto.started).toString();

        this.duration = `<label class="tooltips">${diff}
                           <span>${resultDto.started.toLocaleString()} - ${resultDto.finished.toLocaleString()}</span>
                         </label>`;

        this.errorsCount = resultDto.errorsCount;
        this.isCompleted = resultDto.isCompleted
            ? `<i class="fa fa-check-circle fa-lg text-success"></i>`
            : `<i class="fa fa-times-circle fa-lg text-danger"></i>`;

        const { guessedSymbolsCount, hintedSymbolsCount, fullyGuessedWordsCount,
            partitionallyGuessedWordsCount, totallyHintedWordsCount } = resultDto.calculatedResult;

        const symbolsSum = resultDto.calculatedResult.symbolsCountWithoutSign;
        const guessedPerCent = Math.round(guessedSymbolsCount / symbolsSum * 100);
        const hintedPerCent = resultDto.isCompleted ? (100 - guessedPerCent) : Math.round(hintedSymbolsCount / symbolsSum * 100);

        const wordsSum = resultDto.calculatedResult.wordsCountWithoutSign;
        const fullyGuessedWordsPerCent = Math.round(fullyGuessedWordsCount / wordsSum * 100);
        const partitionallyGuessedWordsPerCent = Math.round(partitionallyGuessedWordsCount / wordsSum * 100);
        const totallyHintedWordsPerCent = Math.round(totallyHintedWordsCount / wordsSum * 100);

        this.guessedSymbolsCount = `${guessedSymbolsCount} (${guessedPerCent} %)`;
        this.hintedSymbolsCount = `${hintedSymbolsCount} (${hintedPerCent} %)`;
        this.fullyGuessedWordsCount = `${fullyGuessedWordsCount} (${fullyGuessedWordsPerCent} %)`;
        this.partitionallyGuessedWordsCount = `${partitionallyGuessedWordsCount} (${partitionallyGuessedWordsPerCent} %)`;
        this.totallyHintedWordsCount = `${totallyHintedWordsCount} (${totallyHintedWordsPerCent} %)`;
    }

    private _toTime (mili: number, isSec: boolean) {
        let ms = isSec ? mili * 1e3 : mili,
            lm = Number(~(4 * (isSec ? 1: 0))),  /* limit fraction */
            fmt = new Date(ms).toISOString().slice(11, lm);

        if (ms >= 8.64e7) {  /* >= 24 hours */
            let parts: any[] = fmt.split(/:(?=\d{2}:)/);
            parts[0] -= -24 * (ms / 8.64e7 | 0);
            return parts.join(':');
        }

        return fmt;
    }
}
