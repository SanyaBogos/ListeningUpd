using FluentAssertions;
using Infrastructure.Helpers;
using Integration;
using Integration.Extensions;
using Listening.Server.Controllers.api;
using Listening.Server.Entities.Specialized.Text;
using Listening.Core.ViewModels.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Controllers
{
    [Collection("Database collection")]
    public class WordControllerSpec : BaseIntegrationTest<WordController>
    {
        public WordControllerSpec(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldGetWordsCountsNotStarted()
        {
            var text = _fixture.Texts.First();
            var response = await _fixture.UserClient.GetAsync($"api/Word/{text.Id}/j");
            var result = await response.Deserialize<TextForGuessingDto>();

            result.Should().NotBeNull();
            result.IsStarted.Should().BeFalse();
            result.WordsCounts.Should().NotBeNullOrEmpty();
            result.MergedText.Should().BeNull();
            result.ResultsEncodedString.Should().BeNull();
            result.ErrorsForJoined.Should().BeNullOrEmpty();
            result.ErrorsForSeparated.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldGetWordsCountsStartedThenGuessWord_ForJoined()
        {
            var text = _fixture.Texts[2];
            var mode = 'j';
            var paragraphIndex = 0;
            var wordIndex = 0;
            var getWordsCountsResponse = await _fixture.UserClient.GetAsync($"api/Word/{text.Id}/{mode}");
            getWordsCountsResponse.EnsureSuccessStatusCode();

            var words = new string[] { text.WordsInParagraphs[paragraphIndex][wordIndex] };
            var postCheckWordsResponse = await _fixture.UserClient.PostAsync(
                $"api/Word/wordsForCheck/{text.Id}", words.Serialize().AsContent());
            postCheckWordsResponse.EnsureSuccessStatusCode();
            var checkResult = await postCheckWordsResponse.Deserialize<List<CorrectWordLocatorsDto>>();
            checkResult.Should().NotBeNullOrEmpty();
            checkResult.First().Word.Should().BeEquivalentTo(words.First());
            checkResult.First().Locators.First().ParagraphIndex
                .Should().Be(paragraphIndex);
            checkResult.First().Locators.First().ParagraphIndex
                .Should().Be(wordIndex);

            getWordsCountsResponse = await _fixture.UserClient.GetAsync($"api/Word/{text.Id}/{mode}");
            getWordsCountsResponse.EnsureSuccessStatusCode();

            var wordsCountsResult = await getWordsCountsResponse.Deserialize<TextForGuessingDto>();

            wordsCountsResult.ResultsEncodedString.Take(words.First().Length * 2)
                .All(x => x == true).Should().BeTrue();
        }

        [Theory]
        [InlineData('j')]
        [InlineData('s')]
        public async Task ShouldGetWordsCountsStartedThenHintLetter(char mode)
        {
            var text = _fixture.Texts[1];

            var paragraphIndex = 0;
            var wordIndex = 0;
            var symbolIndex = RandomHelper.Quantity(
                text.WordsInParagraphs[paragraphIndex][wordIndex].Length - 1);
            var letterLocator = new LetterLocatorDto
            {
                ParagraphIndex = paragraphIndex,
                WordIndex = wordIndex,
                SymbolIndex = symbolIndex
            };

            var expectedLetter = text.WordsInParagraphs[paragraphIndex][wordIndex][symbolIndex];
            var getWordsCountsResponse = await _fixture.UserClient.GetAsync(
                $"api/Word/{text.Id}/{mode}");
            getWordsCountsResponse.EnsureSuccessStatusCode();
            var getLetterResponse = await _fixture.UserClient.GetAsync(
                $"api/Word/letter/{text.Id}/{paragraphIndex}/{wordIndex}/{symbolIndex}/{mode}");
            getLetterResponse.EnsureSuccessStatusCode();

            var actualLetter = await getLetterResponse.Deserialize<char>();

            actualLetter.Should().BeEquivalentTo(expectedLetter);

            getWordsCountsResponse = await _fixture.UserClient.GetAsync(
                $"api/Word/{text.Id}/{mode}");
            var wordsCountsResult = await getWordsCountsResponse.Deserialize<TextForGuessingDto>();

            wordsCountsResult.Should().NotBeNull();
            wordsCountsResult.IsStarted.Should().BeTrue();
            wordsCountsResult.WordsCounts.Should().BeNullOrEmpty();
            wordsCountsResult.MergedText.Should().NotBeNullOrEmpty();
            wordsCountsResult.MergedText[paragraphIndex][wordIndex][symbolIndex]
                .Should().Be(actualLetter.ToString());

            CheckHintResult(text, letterLocator, wordsCountsResult);

            wordsCountsResult.ResultsEncodedString.Should().NotBeNullOrEmpty();
            wordsCountsResult.ErrorsForJoined.Should().BeNullOrEmpty();
            wordsCountsResult.ErrorsForSeparated.Should().BeNullOrEmpty();
        }

        private void CheckHintResult(
            Text text,
            LetterLocatorDto letterLocator,
            TextForGuessingDto wordsCountsResult)
        {
            var hintIndex = letterLocator.SymbolIndex * 2;
            var resultHint = wordsCountsResult.ResultsEncodedString[hintIndex];
            resultHint.Should().BeTrue();
            var wordLength = text.WordsInParagraphs
                                        [letterLocator.ParagraphIndex]
                                        [letterLocator.WordIndex].Length;
            var indexes = Enumerable.Range(0, wordLength).Except(new[] { hintIndex });
            indexes.All(x => !wordsCountsResult.ResultsEncodedString[x]).Should().BeTrue();
        }
    }
}
