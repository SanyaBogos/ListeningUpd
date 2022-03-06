using Listening.Core.Entities.Custom;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Entities.Specialized.ServiceModels.ListeningResult;
using Listening.Server.Services.Contracts;
using Listening.Core.ViewModels.Text;
using Listening.Web.Controllers.api.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize]
    public class WordController : BaseController
    {
        private readonly ITextService _textService;
        private readonly IResultService _resultService;

        public WordController(
            UserManager<ApplicationUser> userManager,
            ITextService textService,
            IResultService resultService
            ) : base(userManager)
        {
            _textService = textService;
            _resultService = resultService;
        }

        [HttpGet("wordsInParagraphs/{id}")]
        [Authorize(Roles = "Super,Admin")]
        public async Task<TextDto> GetWordsInParagraphs(string id)
        {
            return await _textService.GetTextDtoById(id);
        }

        [HttpGet("{id}/{mode}")]
        public async Task<TextForGuessingDto> GetWordsCountInParagraphs(string id, char mode)
        {
            var wordsCounts = await _textService.GetWordCounts(id);
            var result = new Result
            {
                UserId = (await GetCurrentUserAsync()).Id,
                TextId = id,
                Mode = mode
            };

            var newResult = await _resultService.GetTextGuessingResult(result, wordsCounts);

            if (!newResult.IsStarted)
            {
                var textForGuessingDto = new TextForGuessingDto
                {
                    WordsCounts = wordsCounts,
                    IsStarted = false,
                };
                await _resultService.BuildErrors(textForGuessingDto, newResult.Id, mode);
                return textForGuessingDto;
            }

            return await _resultService.GetMergedEncodingWithText(newResult);
        }

        [HttpGet("letter/{id}/{paragraphIndex}/{wordIndex}/{symbolIndex}/{mode}")]
        public async Task<char> GetLetter(string id,
            int paragraphIndex, int wordIndex, int symbolIndex, char mode)
        {
            var wordsInParagraphs = await _textService.GetWordsInParagraphs(id);

            var result = new Result
            {
                UserId = (await GetCurrentUserAsync()).Id,
                TextId = id,
                Mode = mode
            };

            var letterAddress = new LetterAddress
            {
                ParagraphIndex = paragraphIndex,
                WordIndex = wordIndex,
                SymbolIndex = symbolIndex
            };

#if RELEASE
            // We don't need to wait until result will save
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            _resultService.HintLetter(result, letterAddress);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#else
            // However, for testing purpose we should wait
            await _resultService.HintLetter(result, letterAddress);
#endif

            return wordsInParagraphs[paragraphIndex][wordIndex][symbolIndex];
        }

        [HttpGet("wordCorrectness/{id}/{paragraphIndex}/{wordIndex}/{value}")]
        public async Task<bool> GetWordCorrectness(
            string id, int paragraphIndex, int wordIndex, string value)
        {
            value = value.Replace("`", "'");
            string[][] wordsInParagraphs = await _textService.GetWordsInParagraphs(id);
            string word = wordsInParagraphs[paragraphIndex][wordIndex];
            var isCorrectWord = value.Equals(word);

            var result = new Result
            {
                UserId = (await GetCurrentUserAsync()).Id,
                TextId = id,
                Mode = 's'
            };

            var wordAddress = new WordAddress
            {
                ParagraphIndex = paragraphIndex,
                WordIndex = wordIndex
            };

            ResultData resultData;

            if (isCorrectWord)
                resultData = new ResultData
                {
                    Result = result,
                    WordAddresses = new WordAddress[] { wordAddress },
                    WordsLengths = new int[] { word.Length },
                    JoinedErrors = new string[0]
                };
            else
                resultData = new ResultData
                {
                    Result = result,
                    SeparatedError = new SeparatedErrorDescription
                    {
                        WordAddress = wordAddress,
                        Error = value
                    }
                };

#if RELEASE
            _resultService.UpdateResultWithErrors(resultData);
#else
            await _resultService.UpdateResultWithErrors(resultData);
#endif

            return isCorrectWord;
        }

        [HttpPost("wordsForCheck/{id}")]
        public async Task<List<CorrectWordLocatorsDto>> PostCheckWords(string id, [FromBody]string[] words)
        {
            var formattedWords = words.Select(x => x.Replace("`", "'")).ToArray();
            var wordsInParagraphs = await _textService.GetWordsInParagraphs(id);
            var correctWordLocators = _textService.GetCorrectWordLocators(formattedWords, wordsInParagraphs);

            var wordAddressesList = new List<WordAddress>();
            var wordLengths = new List<int>();
            var result = new Result
            {
                UserId = (await GetCurrentUserAsync()).Id,
                TextId = id,
                Mode = 'j'
            };

            foreach (var correctWordLocator in correctWordLocators)
            {
                wordAddressesList.AddRange(correctWordLocator.Locators);
                foreach (var locator in correctWordLocator.Locators)
                    wordLengths.Add(
                        wordsInParagraphs[locator.ParagraphIndex][locator.WordIndex].Length);
            }

            var errorsForJoined =
                 formattedWords.Except(correctWordLocators.Select(x => x.Word));

            var resultData = new ResultData
            {
                Result = result,
                WordAddresses = wordAddressesList.ToArray(),
                WordsLengths = wordLengths.ToArray(),
                JoinedErrors = errorsForJoined.ToArray()
            };

#if RELEASE
            _resultService.UpdateResultWithErrors(resultData);
#else
            await _resultService.UpdateResultWithErrors(resultData);
#endif

            return correctWordLocators;
        }

        [HttpPost("finishGuessing/{id}/{mode}")]
        public async Task FinishGuessing(string id, char mode)
        {
            var result = new Result
            {
                UserId = (await GetCurrentUserAsync()).Id,
                TextId = id,
                Mode = mode
            };

            await _resultService.FinishGuessing(result);
        }
    }
}
