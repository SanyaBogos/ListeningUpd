using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using Listening.Server.Services;
using Listening.Server.Services.Contracts;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Entities.Specialized.Result;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Utilities;
using Unit;
using Infrastructure;
using Listening.Server.Repositories.Abstract;
using Listening.Core.ViewModels.ListeningResult;
using AutoMapper;

namespace Test.UnitTests.Services
{
    public class ResultServiceSpec : BaseUnitTest<ResultService>
    {
        private readonly Mock<IResultRepository> _resultRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITextService> _textServiceMock;
        private readonly Mock<IErrorsForSeparatedRepository> _errorsForSeparatedRepositoryMock;
        private readonly Mock<IErrorsForJoinedRepository> _errorsForJoinedRepositoryMock;
        private readonly Mock<ISimpleTimeSpentCache<ResultIdDto>> _timeCacheMock;
        // private readonly Mock<IMapper> _mapperMock;

        public ResultServiceSpec()
        {
            _resultRepositoryMock = new Mock<IResultRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _textServiceMock = new Mock<ITextService>();
            _errorsForSeparatedRepositoryMock = new Mock<IErrorsForSeparatedRepository>();
            _errorsForJoinedRepositoryMock = new Mock<IErrorsForJoinedRepository>();
            _timeCacheMock = new Mock<ISimpleTimeSpentCache<ResultIdDto>>();
            // _mapperMock = new Mock<IMapper>();

            _sut = new ResultService(_resultRepositoryMock.Object, _textServiceMock.Object,
                _errorsForSeparatedRepositoryMock.Object, _errorsForJoinedRepositoryMock.Object, _userRepositoryMock.Object,
                _timeCacheMock.Object, _mapper);
        }

        class BuildResultStringData : EnumerableDataAbstract
        {
            public BuildResultStringData()
            {
                _data = new List<object[]>
                {
                    new object[] {
                        new string[][] { new string[] { "5", ",", "3", "." } },
                        new int[] { 11, 19 }
                    },
                    new object[] {
                        new string[][] { new string[] { "4", "2", ",", "3", "." } },
                        new int[] { 13, 21 }
                    },
                    new object[] {
                        new string[][] { new string[] { "2", "4", "3", "!!!" } },
                        new int[] { 19 /*, 21, 23*/ }
                    },
                    new object[] {
                        new string[][] {
                            new string[] { "1", "2", "3" },
                            new string[] { "1", "!?", "3" },
                            new string[] { "1", ".", "3" },
                            new string[] { "1", "...", "3" },
                            new string[] { "4", "5", "6" }
                        },
                        new int[] { 15, /*17,*/25, /*27,*/ 35, /*37, 39, 41 */}
                    }
                };
            }
        }

        class MergeEncodingWithTextData : EnumerableDataAbstract
        {
            public MergeEncodingWithTextData()
            {
                _data = new List<object[]> {
                    new object[] {
                        new string[][] { new string[]{ "Nevertheless" } },
                        new byte[] { 0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                        new string[][][] {
                            new string[][] {
                                new string[] {
                                    "1", "e", "3", "e", "5", "6", "7", "8", "9", "10", "11", "12" }
                            }
                        }
                    },
                    new object[] {
                        new string[][] { new string[]{ "There",
                            "are", "a", "few", "moments", "!?..." } },
                        new byte[] {
                            1,0,1,0,1,0,1,1,1,1,//There: 3 - hinted, 2 - guessed
                            0,0,0,0,0,0,//are: all hidden
                            1,0,//a: hinted
                            1,1,1,1,1,1,//few: all guessed
                            0,0,1,0,0,0,1,0,0,0,1,0,0,0,//moments: hidden-hinted
                            0,1 /*,0,1,0,1,0,1,0,1,*/ //signs
                        },
                        new string[][][] {
                            new string[][] {
                                new string[] { "T","h","e","r","e" },
                                new string[] { "1","2","3" },
                                new string[] { "a" },
                                new string[] { "f","e","w" },
                                new string[] { "1","o","3","e","5","t","7" },
                                new string[] { "!?..." }
                            } }
                    }
                };
            }
        }

        class GuessWordData : EnumerableDataAbstract
        {
            public GuessWordData()
            {
                var wordsInParagraphs1 = new string[][] {
                            new string[] { "Hi", ",", "Black", "!" },
                            new string[] { "Hi", ",", "White", "!",
                                "Welcome", ".", "Congrats" }
                        };
                var encodedByteString1 = new byte[] {
                    0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,0,0, 0,1,
                    0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,1,0, 0,1,
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1, 0,1, 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0
                };
                var paragrphsSymbolsCounts = new int[] { 9, 9 };

                _data = new List<object[]>
                {
                    new object[] {
                        wordsInParagraphs1,
                        encodedByteString1,
                        paragrphsSymbolsCounts,
                        new WordAddress[] { // check encoding for fully hidden word
                            new WordAddress { ParagraphIndex = 0, WordIndex = 0 }
                        },
                        new int[] { 2 },
                        new byte[] { 1,1,1,1, 0,1, 0,0,0,0,0,0,0,0,0,0, 0,1,
                            0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,1,0, 0,1,
                            1,1,1,1,1,1,1,1,1,1,1,1,1,1, 0,1, 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0
                        },
                        true
                    },
                    new object[] {
                        wordsInParagraphs1,
                        encodedByteString1,
                        paragrphsSymbolsCounts,
                        new WordAddress[] { // check encoding for some hidden letters
                            new WordAddress { ParagraphIndex = 1, WordIndex = 2 }
                        },
                        new int[] { 5 },
                        new byte[] { 0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,0,0, 0,1,
                            0,0,0,0, 0,1, 1,1,1,1,1,1,1,1,1,0, 0,1,
                            1,1,1,1,1,1,1,1,1,1,1,1,1,1, 0,1, 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0
                        },
                        true
                    },
                    new object[] {
                        wordsInParagraphs1,
                        encodedByteString1,
                        paragrphsSymbolsCounts,
                        new WordAddress[] { // check encoding for guessed word
                            new WordAddress { ParagraphIndex = 1, WordIndex = 4 }
                        },
                        new int[] { 7 },
                        null,
                        true
                    },
                    new object[] {
                        wordsInParagraphs1,
                        encodedByteString1,
                        paragrphsSymbolsCounts,
                        new WordAddress[] { // check encoding for fully hinted word
                            new WordAddress { ParagraphIndex = 1, WordIndex = 6 }
                        },
                        new int[] { 8 },
                        null,
                        true
                    },
                    new object[] {
                        wordsInParagraphs1,
                        encodedByteString1,
                        paragrphsSymbolsCounts,
                        new WordAddress[] { // check encoding for guessing sign
                            new WordAddress { ParagraphIndex = 1, WordIndex = 1 }
                        },
                        new int[] { 1 },
                        null,
                        false
                    },
                    new object[] {
                        wordsInParagraphs1,
                        encodedByteString1,
                        paragrphsSymbolsCounts,
                        new WordAddress[] { // check encoding for guessing sign
                            new WordAddress { ParagraphIndex = 0, WordIndex = 0 },
                            new WordAddress { ParagraphIndex = 1, WordIndex = 2 },
                            new WordAddress { ParagraphIndex = 1, WordIndex = 1 }
                        },
                        new int[] { 2,5,1 },
                        new byte[] { 1,1,1,1, 0,1, 0,0,0,0,0,0,0,0,0,0, 0,1,
                            0,0,0,0, 0,1, 1,1,1,1,1,1,1,1,1,0, 0,1,
                            1,1,1,1,1,1,1,1,1,1,1,1,1,1, 0,1, 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0
                        },
                        true
                    },
                };
            }
        }

        class HintLetterData : EnumerableDataAbstract
        {
            public HintLetterData()
            {
                var wordsInParagraphs1 = new string[][] {
                            new string[] { "Hi", ",", "Black", "!" },
                            new string[] { "Hi", ",", "White", "!",
                                "Welcome", "." }
                        };
                var encodedByteString1 = new byte[] {
                    0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,0,0, 0,1,
                    0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,1,0, 0,1,
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1, 0,1
                };
                var paragrphsSymbolsCounts = new int[] { 9, 9 };

                _data = new List<object[]>
                {
                    new object[] {
                        wordsInParagraphs1,
                        encodedByteString1,
                        paragrphsSymbolsCounts,
                        1,2,3, // check correctness of using paragrphsSymbolsCounts
                        new byte[] { 0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,0,0, 0,1,
                            0,0,0,0, 0,1, 0,0,0,0,0,0,1,0,1,0, 0,1,
                            1,1,1,1,1,1,1,1,1,1,1,1,1,1, 0,1
                        }
                    },
                    new object[] {
                        wordsInParagraphs1,
                        encodedByteString1,
                        paragrphsSymbolsCounts,
                        1,0,1, // check correctness of using paragrphsSymbolsCounts 2
                        new byte[] { 0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,0,0, 0,1,
                            0,0,1,0, 0,1, 0,0,0,0,0,0,0,0,1,0, 0,1,
                            1,1,1,1,1,1,1,1,1,1,1,1,1,1, 0,1
                        }
                    },
                    new object[] {
                        wordsInParagraphs1,
                        encodedByteString1,
                        paragrphsSymbolsCounts,
                        1,1,0, // check possibility to hint sign
                        new byte[] { 0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,0,0, 0,1,
                            0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,1,0, 0,1,
                            1,1,1,1,1,1,1,1,1,1,1,1,1,1, 0,1
                        }
                    },
                    new object[] {
                        wordsInParagraphs1,
                        encodedByteString1,
                        paragrphsSymbolsCounts,
                        1,2,4, // check possibility to hint hinted
                        new byte[] { 0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,0,0, 0,1,
                            0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,1,0, 0,1,
                            1,1,1,1,1,1,1,1,1,1,1,1,1,1, 0,1
                        }
                    },
                    new object[] {
                        wordsInParagraphs1,
                        encodedByteString1,
                        paragrphsSymbolsCounts,
                        1,4,2, // check possibility to hint guessed
                        new byte[] { 0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,0,0, 0,1,
                            0,0,0,0, 0,1, 0,0,0,0,0,0,0,0,1,0, 0,1,
                            1,1,1,1,1,1,1,1,1,1,1,1,1,1, 0,1
                        }
                    },

                    new object[] {
                        new string[][] { new string[]{ "There",
                            "are", "a", "few", "moments", "!?..." } },
                        new byte[] {
                            1,0,1,0,1,0,1,1,1,1,//There: 3 - hinted, 2 - guessed
                            0,0,0,0,0,0,//are: all hidden
                            1,0,//a: hinted
                            1,1,1,1,1,1,//few: all guessed
                            0,0,1,0,0,0,1,0,0,0,1,0,0,0,//moments: hidden-hinted
                            0,1,0,1,0,1,0,1,0,1,//signs
                        },
                        new int[] { 14 },
                        0,1,2,
                        new byte[] {
                            1,0,1,0,1,0,1,1,1,1,//There: 3 - hinted, 2 - guessed
                            0,0,0,0,1,0,//are: one hinted
                            1,0,//a: hinted
                            1,1,1,1,1,1,//few: all guessed
                            0,0,1,0,0,0,1,0,0,0,1,0,0,0,//moments: hidden-hinted
                            0,1,0,1,0,1,0,1,0,1,//signs
                        },
                    }
                };
            }
        }

        [Theory, ClassData(typeof(HintLetterData))]
        public void ShouldHintLetter(string[][] wordsInParagraphs,
            byte[] resultsEncodedByteString, int[] paragrphsSymbolsCounts,
            int paragraphIndex, int wordIndex, int symbolIndex,
            byte[] updateResultsEncodedByteString)
        {
            var resultsEncodedString = resultsEncodedByteString
                .Select(x => Convert.ToBoolean(x)).ToArray();
            var updateResultsEncodedString = updateResultsEncodedByteString
                .Select(x => Convert.ToBoolean(x)).ToArray();
            var existedResult = new Result
            { ResultsEncodedString = resultsEncodedString };

            var newResult = new Result
            {
                UserId = long.MaxValue - 1,
                TextId = "textId",
                Mode = 'j'
            };

            var letterAddress = new LetterAddress
            {
                ParagraphIndex = paragraphIndex,
                WordIndex = wordIndex,
                SymbolIndex = symbolIndex
            };

            _resultRepositoryMock.Setup(x => x.GetNonCompletedResult(It.IsAny<Result>()))
                .ReturnsAsync(existedResult);
            _resultRepositoryMock.Setup(x => x.Update(It.IsAny<Result[]>()))
                .Returns(Task.CompletedTask);
            _textServiceMock.Setup(x => x.GetWordsInParagraphs(It.IsAny<string>()))
                .ReturnsAsync(wordsInParagraphs);
            _textServiceMock.Setup(x => x.GetParagrphsSymbolsCounts(It.IsAny<string>()))
                .ReturnsAsync(paragrphsSymbolsCounts);

            _sut.HintLetter(newResult, letterAddress)
               .GetAwaiter().GetResult();

            _resultRepositoryMock.Verify(x => x.Update(It.Is<Result[]>(
                y => y.All(z => z.ResultsEncodedString.SequenceEqual(updateResultsEncodedString)
                    && z.IsStarted == true))));
        }

        [Fact]
        public void ShouldReturnExisedResult()
        {
            var existedResult = new Result();
            _resultRepositoryMock.Setup(x => x.GetNonCompletedResult(It.IsAny<Result>()))
                .ReturnsAsync(existedResult);
            _timeCacheMock.Setup(x => x.Insert(It.IsAny<ResultIdDto>()));

            var newResult = new Result
            {
                UserId = long.MaxValue - 1,
                TextId = "textId",
                Mode = 'j'
            };

            var result = _sut.GetTextGuessingResult(newResult,
                    (string[][])(new BuildResultStringData().First()[0]))
                .GetAwaiter().GetResult();

            result.Should().Be(existedResult);
        }

        [Theory, ClassData(typeof(MergeEncodingWithTextData))]
        public void ShouldMergeEncodingWithText(string[][] wordsInParagraphs,
            byte[] resultsEncodedByteString, string[][][] resultTextString)
        {
            var resultsEncodedString = resultsEncodedByteString
                .Select(x => Convert.ToBoolean(x)).ToArray();

            _textServiceMock.Setup(x => x.GetWordsInParagraphs(It.IsAny<string>()))
                .ReturnsAsync(wordsInParagraphs);

            var result = new Result
            {
                TextId = "texId",
                ResultsEncodedString = resultsEncodedString
            };

            var resultMergedString = _sut.MergeEncodingWithText(result)
                .GetAwaiter().GetResult();

            resultMergedString.Should().BeEquivalentTo(resultTextString);
        }

        [Theory, ClassData(typeof(BuildResultStringData))]
        public void ShouldBuildResultString(string[][] wordsCounts, int[] result)
        {
            var newResult = new Result
            {
                UserId = long.MaxValue - 1,
                TextId = "textId",
                Mode = 'j'
            };

            var counts = Count(wordsCounts);
            var expectedEncodedString = new bool[2 * counts];
            foreach (var item in result)
                expectedEncodedString[item] = true;

            _textServiceMock.Setup(x => x.GetSymbolsCount(It.IsAny<string>()))
                .ReturnsAsync(counts);
            _resultRepositoryMock.Setup(x => x.GetNonCompletedResult(It.IsAny<Result>()))
                .ReturnsAsync((Result)null);
            _resultRepositoryMock.Setup(x => x.Insert(It.IsAny<Result[]>()))
                .Returns(Task.CompletedTask);

            _sut.GetTextGuessingResult(newResult, wordsCounts).GetAwaiter().GetResult();

            _resultRepositoryMock.Verify(c => c.Insert(
                It.Is<Result[]>(t => t.All(z => z.UserId.Equals(newResult.UserId)
                  && z.TextId.Equals(newResult.TextId)
                  && z.ResultsEncodedString.SequenceEqual(expectedEncodedString)))));
        }


        [Theory, ClassData(typeof(GuessWordData))]
        public async Task ShouldEncodeGuessedWords(
            string[][] wordsInParagraphs,
            byte[] resultsEncodedByteString, int[] paragrphsSymbolsCounts,
            WordAddress[] wordAddresses, int[] wordsLength,
            byte[] updateResultsEncodedByteString, bool shouldReturnResult)
        {
            var resultsEncodedString = resultsEncodedByteString
                .Select(x => Convert.ToBoolean(x)).ToArray();
            //var result = new Result
            //{
            //    UserId = long.MaxValue - 1,
            //    TextId = "textId",
            //    Mode = 'j'
            //};
            var existedResult = new Result
            {
                UserId = long.MaxValue - 1,
                TextId = "textId",
                Mode = 'j',
                ResultsEncodedString = resultsEncodedString
            };


            //_resultRepositoryMock.Setup(x => x.GetNonCompletedResult(It.IsAny<Result>()))
            //    .ReturnsAsync(existedResult);
            _textServiceMock.Setup(x => x.GetWordsInParagraphs(It.IsAny<string>()))
                .ReturnsAsync(wordsInParagraphs);
            _textServiceMock.Setup(x => x.GetParagrphsSymbolsCounts(It.IsAny<string>()))
                .ReturnsAsync(paragrphsSymbolsCounts);

            await _sut.EncodeGuessedWords(existedResult, wordAddresses, wordsLength);

            if (shouldReturnResult)
                existedResult.IsStarted.Should().BeTrue();

            if (updateResultsEncodedByteString != null)
            {
                var updateResultsEncodedString = updateResultsEncodedByteString
                            .Select(x => Convert.ToBoolean(x)).ToArray();
                existedResult.ResultsEncodedString.Should().BeEquivalentTo(updateResultsEncodedString);
            }
        }

        private int Count(string[][] wordsCounts)
        {
            int count = 0;

            foreach (var paragrapth in wordsCounts)
                foreach (var word in paragrapth)
                    if (int.TryParse(word, out int x))
                        count += x;
                    else
                        count++;

            return count;
        }
    }
}
