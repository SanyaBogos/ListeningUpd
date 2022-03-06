using Listening.Infrastructure.Utilities;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Entities.Specialized.ServiceModels.ListeningResult;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Services.Contracts;
using Listening.Server.Utilities;
using Listening.Core.ViewModels.ListeningResult;
using Listening.Core.ViewModels.Text;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptions = Listening.Infrastructure.Exceptions;
using Listening.Server.Repositories.Abstract;
using Listening.Infrastructure.Extensions;
using AutoMapper;

namespace Listening.Server.Services
{
    // Coding symbols system
    // 00 - hidden symbol
    // 01 - sign
    // 10 - hinted
    // 11 - guessed
    // Example (counts): 
    //      Entry string: 1   2     4           1  2     ,  1  3         !?...
    //      Encoded:      00  00|00 00|00|00|00 00 00|00 01 00 00|00|00  01
    // There is no necessarity to save more than 1 symbol for sign
    public class ResultService : IResultService
    {
        private const string digitPattern = @"^\d+$";

        private readonly IResultRepository _resultRepository;
        private readonly ITextService _textService;
        private readonly IErrorsForSeparatedRepository _errorsForSeparatedRepository;
        private readonly IErrorsForJoinedRepository _errorsForJoinedRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISimpleTimeSpentCache<ResultIdDto> _timeCache;
        private readonly IMapper _mapper;

        public ResultService(
            IResultRepository resultRepository,
            ITextService textService,
            IErrorsForSeparatedRepository errorsForSeparatedRepository,
            IErrorsForJoinedRepository errorsForJoinedRepository,
            IUserRepository userRepository,
            ISimpleTimeSpentCache<ResultIdDto> timeCache,
            IMapper mapper)
        {
            _resultRepository = resultRepository;
            _textService = textService;
            _errorsForSeparatedRepository = errorsForSeparatedRepository;
            _errorsForJoinedRepository = errorsForJoinedRepository;
            _userRepository = userRepository;
            _timeCache = timeCache;
            _mapper = mapper;
        }

        public async Task<TextForGuessingDto> GetMergedEncodingWithText(Result newResult, bool isResult = false)
        {
            var mergedText = await MergeEncodingWithText(newResult);

            var textForGuessingDto = new TextForGuessingDto
            {
                IsStarted = true,
                MergedText = mergedText,
                ResultsEncodedString = newResult.ResultsEncodedString
            };

            await BuildErrors(textForGuessingDto, newResult.Id, newResult.Mode);

            return textForGuessingDto;
        }

        public async Task<string[][][]> MergeEncodingWithText(Result result)
        {
            var wordsInParagraphs = await _textService.GetWordsInParagraphs(result.TextId);
            var mergedText = ResultHelper.MergeEncodingWithText(
                                        result.ResultsEncodedString, wordsInParagraphs);

            return mergedText.ToArray();
        }

        public string[][][] MergeEncodingWithText(
            bool[] resultsEncodedString, string[][] wordsInParagraphs)
        {
            var mergedText = ResultHelper.MergeEncodingWithText(
                                        resultsEncodedString, wordsInParagraphs);

            return mergedText.ToArray();
        }


        public async Task HintLetter(Result result, LetterAddress letterAddress)
        {
            var existedResult = await _resultRepository.GetNonCompletedResult(result);
            int resultsIndex = await CalculateResultPositionIndex(result, letterAddress);

            if (!existedResult.ResultsEncodedString[resultsIndex * 2]
                && !existedResult.ResultsEncodedString[resultsIndex * 2 + 1])
                existedResult.ResultsEncodedString[resultsIndex * 2] = true;

            existedResult.IsStarted = true;

            if (IsTextFinished(existedResult.ResultsEncodedString))
            {
                existedResult.Finished = DateTime.Now;
                existedResult.IsCompleted = true;
            }

            await _resultRepository.Update(new Result[] { existedResult });
        }

        public async Task UpdateTimeSpent(ResultUpdateTimeDto result)
        {
            await _resultRepository.UpdateTimeSpentForNonCompletedResult(result);
        }

        public async Task<Result> GetTextGuessedResult(Result result, bool isAdminOrSuper)
        {
            var existedResult = await _resultRepository.GetById(result.Id);

            if (existedResult == null)
                throw new Exceptions.DataException("IDENTIFIER_NOT_FOUND");

            if (!isAdminOrSuper && existedResult.UserId != result.UserId)
                throw new Exceptions.AuthenticationException("ACCESS_DENIED_FOR_RESULT");

            return existedResult;
        }

        public async Task<Result> GetTextGuessingResult(Result result, string[][] wordsCounts)
        {
            var existedResult = await _resultRepository.GetNonCompletedResult(result);
            _timeCache.Insert(_mapper.Map<ResultIdDto>(result));

            if (existedResult != null)
                return existedResult;

            var symbolsCount = await _textService.GetSymbolsCount(result.TextId);
            var resultEncodedString = BuildStartedResultString(symbolsCount, wordsCounts);

            result.ResultsEncodedString = resultEncodedString;
            result.Started = DateTime.Now;
            result.TimeSpentMiliSeconds = 0;

            await _resultRepository.Insert(new Result[] { result });

            return result;
        }

        public async Task EncodeGuessedWords(Result existedResult, WordAddress[] wordAddresses, int[] wordLengths)
        {
            var hasBeenSetGuessedWordArray = new bool[wordAddresses.Length];

            for (int i = 0; i < wordAddresses.Length; i++)
                hasBeenSetGuessedWordArray[i] =
                    await ChangeWordToGuessed(existedResult, wordAddresses[i], wordLengths[i]);

            if (hasBeenSetGuessedWordArray.All(x => x == false))
                return;

            existedResult.IsStarted = true;

            if (IsTextFinished(existedResult.ResultsEncodedString))
            {
                existedResult.Finished = DateTime.Now;
                existedResult.IsCompleted = true;
            }
        }

        public async Task<ResultEnhancedDto> GetResultsForUser(long userId)
        {
            IEnumerable<ErrorCount> errorsJoinedCounts = new ErrorCount[0];
            IEnumerable<ErrorCount> errorsSeparatedCounts = new ErrorCount[0];

            var results = await _resultRepository.GetResults(userId);
            var joinedResultIds = results.Where(x => x.Mode == 'j').Select(x => x.Id).ToArray();
            var separatedResultIds = results.Where(x => x.Mode == 's').Select(x => x.Id).ToArray();

            if (joinedResultIds.Any())
                errorsJoinedCounts = await _errorsForJoinedRepository.ErrorsCount(joinedResultIds);

            if (errorsSeparatedCounts.Any())
                errorsSeparatedCounts = await _errorsForSeparatedRepository.ErrorsCount(separatedResultIds);

            var textIds = results.Select(x => ObjectId.Parse(x.TextId)).ToArray();
            var textModels = _textService.GetTexts(textIds);
            var paragraphs = textModels.Select(x => x.WordsInParagraphs).ToArray();

            var resultDtos = from result in results
                             join text in textModels on result.TextId equals text.Id.ToString()
                             join errorJoinedCount in errorsJoinedCounts
                                on result.Id equals errorJoinedCount.ResultId
                                    into ressWithJoined
                             from res in ressWithJoined.DefaultIfEmpty()
                             join errorSeparatedCount in errorsSeparatedCounts
                                on result.Id equals errorSeparatedCount.ResultId
                                    into ress2WithJoined
                             from res2 in ress2WithJoined.DefaultIfEmpty()
                             orderby result.Id descending
                             select new ResultDto
                             {
                                 Id = result.Id,
                                 Title = text.Title,
                                 Country = text.Country,
                                 CalculatedResult = CalculateResults(text.WordsInParagraphs, result.ResultsEncodedString),
                                 Mode = result.Mode,
                                 Started = result.Started,
                                 Finished = result.Finished.Value,
                                 TimeSpentMiliSeconds = GetTimeSpentMilliSeconds(result.TimeSpentMiliSeconds),
                                 IsCompleted = result.IsCompleted,
                                 ErrorsCount = res?.Count ?? res2?.Count ?? 0
                             };

            var users = await _userRepository.GetUsersAsync(/*userId*/);
            var resultEnhanced = new ResultEnhancedDto { Results = resultDtos.ToArray(), Users = users };

            return resultEnhanced;
        }


        public async Task UpdateResultWithErrors(ResultData resultData)
        {
            var existedResult = await _resultRepository.GetNonCompletedResult(resultData.Result);

            if (resultData.WordAddresses != null && resultData.WordAddresses.Any())
            {
                await EncodeGuessedWords(
                        existedResult, resultData.WordAddresses, resultData.WordsLengths);
                await _resultRepository.Update(new Result[] { existedResult });
            }

            if (resultData.SeparatedError == null && !resultData.JoinedErrors.Any())
                return;

            if (existedResult.Mode == 's')
                await _errorsForSeparatedRepository.Insert(new ErrorForSeparated[] {
                    new ErrorForSeparated
                    {
                        ResultId = existedResult.Id,
                        ParagraphIndex = resultData.SeparatedError.WordAddress.ParagraphIndex,
                        WordIndex = resultData.SeparatedError.WordAddress.WordIndex,
                        ErrorValue = resultData.SeparatedError.Error
                    }});
            else
                await _errorsForJoinedRepository.Insert(resultData.JoinedErrors.Select(
                    x => new ErrorForJoined
                    {
                        ResultId = existedResult.Id,
                        ErrorValue = x
                    }));

            existedResult.IsStarted = true;
            await _resultRepository.Update(new Result[] { existedResult });
        }

        public async Task FinishGuessing(Result result)
        {
            var existedResult = await _resultRepository.GetNonCompletedResult(result);
            existedResult.Finished = DateTime.Now;
            await _resultRepository.Update(new Result[] { existedResult });
        }

        public async Task BuildErrors(TextForGuessingDto textForGuessingDto, long resultId, char mode)
        {
            if (mode == 'j')
                textForGuessingDto.ErrorsForJoined =
                    await _errorsForJoinedRepository.GetErrors(resultId);
            else
                textForGuessingDto.ErrorsForSeparated =
                    await _errorsForSeparatedRepository.GetErrors(resultId);
        }

        public bool[] BuildPreviewResultString(string[][] wordsCounts)
        {
            var symbolsCount = TextTransform.GetSymbolCount(wordsCounts);
            var resultEncodedString = new bool[symbolsCount * 2];
            int currentPosition = 0;

            // run through all paragraphs and mark all signed symbols 
            // (we needn't mark because it`s already marked)
            foreach (var paragraph in wordsCounts)
                foreach (var item in paragraph)
                {
                    if (int.TryParse(item, out int symbolsCountInWord))
                        for (int i = 0; i < symbolsCountInWord; i++, currentPosition += 2)
                            resultEncodedString[currentPosition] = true;
                    else
                    {
                        resultEncodedString[currentPosition + 1] = true;
                        currentPosition += 2;
                    }
                }

            return resultEncodedString;
        }

        private bool IsTextFinished(bool[] resultsEncodedString)
        {
            for (int i = 0; i < resultsEncodedString.Length; i += 2)
                if (!resultsEncodedString[i] && !resultsEncodedString[i + 1])
                    return false;

            return true;
        }

        private async Task<bool> ChangeWordToGuessed(Result result, WordAddress wordAddress, int wordLength/*, Result existedResult*/)
        {
            int resultsIndex = await CalculateResultPositionIndex(result, wordAddress);
            var start = resultsIndex * 2;
            var end = start + wordLength * 2;

            if (result.ResultsEncodedString[start] == false
                && result.ResultsEncodedString[start + 1] == true)
            {
                return false;
            }

            for (int i = start; i < end; i += 2)
            {
                if (result.ResultsEncodedString[i] == false
                    && result.ResultsEncodedString[i + 1] == false)
                {
                    result.ResultsEncodedString[i] = true;
                    result.ResultsEncodedString[i + 1] = true;
                }
            }

            return true;
        }

        private async Task<int> CalculateResultPositionIndex(Result result, WordAddress wordAddress)
        {
            var wordsInParagraphs = await _textService.GetWordsInParagraphs(result.TextId);
            var paragrphsSymbolsCounts = await _textService.GetParagrphsSymbolsCounts(result.TextId);

            var paragraphIndex = paragrphsSymbolsCounts.Take(wordAddress.ParagraphIndex).Sum();
            var wordIndex = wordsInParagraphs[wordAddress.ParagraphIndex]
                .Take(wordAddress.WordIndex)
                // I encode group of signs as one symbol '...' , '!!!', '!!!???'
                .Select(x => !TextTransform.IsSpecialSymbolWord(x) ? x.Length : 1)
                .Sum();

            return paragraphIndex + wordIndex;
        }

        private async Task<int> CalculateResultPositionIndex(Result result, LetterAddress letterAddress)
        {
            return
                await CalculateResultPositionIndex(result, (WordAddress)letterAddress)
                + letterAddress.SymbolIndex;
        }

        private bool[] BuildStartedResultString(int symbolsCount, string[][] wordsCounts)
        {
            var resultEncodedString = new bool[symbolsCount * 2];
            int currentPosition = 0;

            // run through all paragraphs and mark all signed symbols 
            // (we needn't mark because it`s already marked)
            foreach (var paragraph in wordsCounts)
                foreach (var item in paragraph)
                    if (int.TryParse(item, out int symbolsCountInWord))
                        currentPosition += symbolsCountInWord * 2;
                    else
                    {
                        resultEncodedString[currentPosition + 1] = true;
                        currentPosition += 2;
                    }
            return resultEncodedString;
        }

        private int? GetTimeSpentMilliSeconds(int? tsms)
        {
            if (tsms.HasValue)
                return tsms.Value;
            else
                return null;
        }


        private WordsCalculatedResultDto CalculateResults(string[][] paragraphs, bool[] results)
        {
            var index = 0;
            var guessedSymbolsCount = 0;
            var hintedSymbolsCount = 0;
            var hiddenSymbolsCount = 0;

            var wordsWithHiddenSignCount = 0;
            var fullyGuessedWordsCount = 0;
            var partitionallyGuessedWordsCount = 0;
            var totallyHintedWordsCount = 0;

            foreach (var paragraph in paragraphs)
            {
                foreach (var word in paragraph)
                {
                    var previosGuessed = guessedSymbolsCount;
                    var previousHinted = hintedSymbolsCount;
                    var previousHidden = hiddenSymbolsCount;

                    if (word.IsSign())
                    {
                        index += 2;
                        continue;
                    }

                    var toLength = index + word.Length * 2;

                    for (int i = index; i < toLength; i += 2)
                    {
                        if (results[i] == true && results[i + 1] == true)
                            guessedSymbolsCount++;
                        else if (results[i] == true && results[i + 1] == false)
                            hintedSymbolsCount++;
                        else
                            hiddenSymbolsCount++;
                    }

                    if (previousHidden != hiddenSymbolsCount)
                        wordsWithHiddenSignCount++;
                    else if (previosGuessed == guessedSymbolsCount)
                        totallyHintedWordsCount++;
                    else if (previousHinted == hintedSymbolsCount)
                        fullyGuessedWordsCount++;
                    else
                        partitionallyGuessedWordsCount++;

                    index = toLength;
                }
            }

            return new WordsCalculatedResultDto
            {
                SymbolsCountWithoutSign = guessedSymbolsCount + hintedSymbolsCount + hiddenSymbolsCount,
                GuessedSymbolsCount = guessedSymbolsCount,
                HintedSymbolsCount = hintedSymbolsCount,
                FullyGuessedWordsCount = fullyGuessedWordsCount,
                PartitionallyGuessedWordsCount = partitionallyGuessedWordsCount,
                TotallyHintedWordsCount = totallyHintedWordsCount,
                WordsCountWithoutSign = fullyGuessedWordsCount + partitionallyGuessedWordsCount
                                            + totallyHintedWordsCount + wordsWithHiddenSignCount
            };
        }
    }
}
