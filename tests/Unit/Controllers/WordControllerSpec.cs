using FluentAssertions;
using Infrastructure.Helpers;
using Listening.Server.Controllers.api;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Services.Contracts;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Unit.Controllers
{
    public class WordControllerSpec : BaseUnitTest<WordController>
    {
        private readonly Mock<ITextService> _textServiceMock;
        private readonly Mock<IResultService> _resultServiceMock;

        public WordControllerSpec()
        {
            _textServiceMock = new Mock<ITextService>();
            _resultServiceMock = new Mock<IResultService>();

            _sut = new WordController(_userManagerMock.Object, _textServiceMock.Object,
                _resultServiceMock.Object)
            {
                ControllerContext = _context
            };
        }

        //[Fact]
        //public async Task ShouldReturnNoMergedTextIfGuessingJustStarted()
        //{
        //    var wordsCounts = RandomHelper.StringsArrays(10);
        //    var textId = RandomHelper.String();
        //    var mode = RandomHelper.RandomCharForMode();
        //    var newResult = new Result { IsStarted = false };

        //    _textServiceMock.Setup(x => x.GetWordCounts(It.IsAny<string>()))
        //        .Returns(Task.FromResult(wordsCounts));
        //    _resultServiceMock.Setup(x => x.BuildResultString(
        //            It.IsAny<Result>(), It.IsAny<string[][]>()))
        //        .Returns(Task.FromResult(newResult));

        //    var textForGuessingDto = await _sut.GetWordsCountInParagraphs(textId, mode);

        //    textForGuessingDto.WordsCounts.Should().BeEquivalentTo(wordsCounts);
        //    textForGuessingDto.IsStarted.Should().BeFalse();

        //    _resultServiceMock.Verify(c => c.BuildResultString(
        //                It.Is<Result>(y => y.UserId == _userId
        //                    && y.TextId == textId && y.Mode == mode),
        //                It.Is<string[][]>(y => y.SequenceEqual(wordsCounts))));
        //}

        //[Fact]
        //public async Task ShouldGenerateMergedTextAndReturnWithResultsEncodedString()
        //{
        //    var wordsCounts = RandomHelper.StringsArrays(10);
        //    var mergedText = new string[][][] {
        //        RandomHelper.StringsArrays(10)
        //    };
        //    var textId = RandomHelper.String();
        //    var mode = RandomHelper.RandomCharForMode();
        //    var newResult = new Result
        //    {
        //        IsStarted = true,
        //        ResultsEncodedString = RandomHelper.GenerateBoolArray(10)
        //    };

        //    _textServiceMock.Setup(x => x.GetWordCounts(It.IsAny<string>()))
        //        .Returns(Task.FromResult(wordsCounts));
        //    _resultServiceMock.Setup(x => x.BuildResultString(
        //            It.IsAny<Result>(), It.IsAny<string[][]>()))
        //        .Returns(Task.FromResult(newResult));
        //    _resultServiceMock.Setup(x => x.MergeEncodingWithText(
        //            It.IsAny<Result>()))
        //        .Returns(Task.FromResult(mergedText));

        //    var textForGuessingDto = await _sut.GetWordsCountInParagraphs(textId, mode);

        //    textForGuessingDto.MergedText.Should().BeEquivalentTo(mergedText);
        //    textForGuessingDto.ResultsEncodedString.Should().AllBeEquivalentTo(
        //        newResult.ResultsEncodedString);
        //    textForGuessingDto.IsStarted.Should().BeTrue();

        //    _resultServiceMock.Verify(c => c.MergeEncodingWithText(
        //            It.Is<Result>(y => y.Equals(newResult))));
        //}

        //[Fact]
        //public async Task ShouldGetWordCorrectnessReturnTrue()
        //{
        //    string[][] wordsInParagraphs = RandomHelper.StringsArrays(10);
        //    var textId = RandomHelper.String();
        //    var paragraphIndex = RandomHelper.Quantity(wordsInParagraphs.Length);
        //    var wordIndex = RandomHelper.Quantity(wordsInParagraphs[paragraphIndex].Length);
        //    var value = wordsInParagraphs[paragraphIndex][wordIndex];
        //    var existedResult = new Result();
        //    var wordAddressesExpected = new WordAddress[] {
        //        new WordAddress()
        //    };

        //    _textServiceMock.Setup(x => x.GetWordsInParagraphs(It.IsAny<string>()))
        //        .Returns(Task.FromResult(wordsInParagraphs));
        //    _resultRepositoryMock.Setup(x => x.GetNonCompletedResult(It.IsAny<Result>()))
        //        .Returns(Task.FromResult(existedResult));
        //    //_resultServiceMock.Setup(x => x.BuildEncodeGuessedWords(It.IsAny<Result>(),
        //    //    It.IsAny<WordAddress[]>(), It.IsAny<int[]>()))
        //    //    .Returns(Task.CompletedTask);

        //    var isCorrect = await _sut.GetWordCorrectness(
        //        textId, paragraphIndex, wordIndex, value);

        //    isCorrect.Should().BeTrue();

        //    _textServiceMock.Verify(c => c.GetWordsInParagraphs(
        //        It.Is<string>(y => y.Equals(textId))));
        //    _resultRepositoryMock.Verify(c => c.GetNonCompletedResult(
        //                It.Is<Result>(y => y.UserId == _userId
        //                    && y.TextId == textId && y.Mode == 's')));
        //    //_resultServiceMock.Verify(c => c.BuildEncodeGuessedWords(
        //    //    It.Is<Result>(y => y.Equals(existedResult)),
        //    //    It.IsAny<WordAddress[]>(),
        //    //    It.Is<int[]>(y => y.SequenceEqual(new int[] { value.Length }))));
        //}

        //[Fact]
        //public async Task ShouldWriteErrorIfIncorrectWord()
        //{
        //    string[][] wordsInParagraphs = RandomHelper.StringsArrays(10);
        //    var textId = RandomHelper.String();
        //    var paragraphIndex = RandomHelper.Quantity(wordsInParagraphs.Length);
        //    var wordIndex = RandomHelper.Quantity(wordsInParagraphs[paragraphIndex].Length);
        //    var value = "someVal";
        //    var resultId = RandomHelper.Quantity();
        //    var existedResult = new Result { Id = resultId };
        //    var expectedErrorForSeparated = new ErrorForSeparated
        //    {
        //        ResultId = resultId,
        //        ParagraphIndex = paragraphIndex,
        //        WordIndex = wordIndex,
        //        Value = value
        //    };
        //    var wordAddressesExpected = new WordAddress[] {
        //        new WordAddress()
        //    };

        //    _textServiceMock.Setup(x => x.GetWordsInParagraphs(It.IsAny<string>()))
        //        .Returns(Task.FromResult(wordsInParagraphs));
        //    _resultRepositoryMock.Setup(x => x.GetNonCompletedResult(It.IsAny<Result>()))
        //        .Returns(Task.FromResult(existedResult));
        //    //_resultServiceMock.Setup(x => x.BuildEncodeGuessedWords(It.IsAny<Result>(),
        //    //    It.IsAny<WordAddress[]>(), It.IsAny<int[]>()))
        //    //    .Returns(Task.CompletedTask);
        //    _errorsForSeparatedRepositoryMock.Setup(x => x.Insert(
        //        It.IsAny<ErrorForSeparated[]>()))
        //        .Returns(Task.CompletedTask);

        //    var isCorrect = await _sut.GetWordCorrectness(
        //        textId, paragraphIndex, wordIndex, value);

        //    isCorrect.Should().BeFalse();
        //    Assert.True(false);
        //    //_errorsForSeparatedRepositoryMock.Verify(c => c.Insert(
        //    //    It.Is<ErrorForSeparated[]>(y => y.First().IsDeepEqual(expectedErrorForSeparated))));
        //}
    }
}
