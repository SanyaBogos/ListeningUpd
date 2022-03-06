using Listening.Core.ViewModels.ListeningResult;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Entities.Specialized.ServiceModels.ListeningResult;
using Listening.Core.ViewModels.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Services.Contracts
{
    public interface IResultService
    {
        Task<string[][][]> MergeEncodingWithText(Result result);
        Task HintLetter(Result result, LetterAddress letterAddress);
        Task UpdateTimeSpent(ResultUpdateTimeDto result);
        Task<Result> GetTextGuessingResult(Result result, string[][] wordsCounts);
        Task<Result> GetTextGuessedResult(Result result, bool isAdminOrSuper);
        Task EncodeGuessedWords(Result existedResult,
            WordAddress[] wordAddresses, int[] wordLengths);

        Task FinishGuessing(Result result);
        Task UpdateResultWithErrors(ResultData resultData);
        Task<TextForGuessingDto> GetMergedEncodingWithText(Result newResult, bool isResult = false);
        Task BuildErrors(TextForGuessingDto textForGuessingDto, long resultId, char mode);
        Task<ResultEnhancedDto> GetResultsForUser(long user);
        bool[] BuildPreviewResultString(string[][] wordsCounts);
        string[][][] MergeEncodingWithText(bool[] resultsEncodedString,
            string[][] wordsInParagraphs);
    }
}
