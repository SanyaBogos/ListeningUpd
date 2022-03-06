using FluentAssertions;
using Infrastructure.Helpers;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Repositories.Postgres;
using Listening.Core.ViewModels.Text;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Repositoreis
{
    [Collection("Database collection")]
    public class ErrorForSeparatedRepositorySpec : BaseIntegrationTest<ErrorForSeparatedRepository>
    {
        public ErrorForSeparatedRepositorySpec(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _sut = new ErrorForSeparatedRepository(_fixture.Configuration);
        }

        [Fact]
        public async Task CheckGetErrorsForJoined()
        {
            var result = _fixture.Results.First();

            var firstParagraph = RandomHelper.Quantity();
            var firstWord = RandomHelper.Quantity();
            var secondParagraph = RandomHelper.Quantity();
            var secondWord = RandomHelper.Quantity();
            var thirdParagraph = RandomHelper.Quantity();
            var thirdWord = RandomHelper.Quantity();

            var firstGroup = new ErrorForSeparated[]
            {
                new ErrorForSeparated { ResultId = result.Id, ErrorValue = RandomHelper.String(5),
                    ParagraphIndex = firstParagraph, WordIndex = firstWord },
                new ErrorForSeparated { ResultId = result.Id, ErrorValue = RandomHelper.String(5),
                    ParagraphIndex = firstParagraph, WordIndex = firstWord },
                new ErrorForSeparated { ResultId = result.Id, ErrorValue = RandomHelper.String(5),
                    ParagraphIndex = firstParagraph, WordIndex = firstWord },
            };

            var secondGroup = new ErrorForSeparated[]
            {
                new ErrorForSeparated { ResultId = result.Id, ErrorValue = RandomHelper.String(5),
                    ParagraphIndex = secondParagraph, WordIndex = secondWord },
                new ErrorForSeparated { ResultId = result.Id, ErrorValue = RandomHelper.String(5),
                    ParagraphIndex = secondParagraph, WordIndex = secondWord },
                new ErrorForSeparated { ResultId = result.Id, ErrorValue = RandomHelper.String(5),
                    ParagraphIndex = secondParagraph, WordIndex = secondWord },
            };

            var thirdGroup = new ErrorForSeparated[]
            {
                new ErrorForSeparated { ResultId = result.Id, ErrorValue = RandomHelper.String(5),
                    ParagraphIndex = thirdParagraph, WordIndex = thirdWord },
                new ErrorForSeparated { ResultId = result.Id, ErrorValue = RandomHelper.String(5),
                    ParagraphIndex = thirdParagraph, WordIndex = thirdWord },
                new ErrorForSeparated { ResultId = result.Id, ErrorValue = RandomHelper.String(5),
                    ParagraphIndex = thirdParagraph, WordIndex = thirdWord },
            };

            var expectedResult = new ErrorForSeparatedDto[] {
                new ErrorForSeparatedDto {
                    WordAddress = new WordAddress {
                        ParagraphIndex = firstParagraph,
                        WordIndex = firstWord
                    },
                    Errors = firstGroup.Select(x => x.ErrorValue).ToArray()
                },
                new ErrorForSeparatedDto {
                    WordAddress = new WordAddress {
                        ParagraphIndex = secondParagraph,
                        WordIndex = secondWord
                    },
                    Errors = secondGroup.Select(x => x.ErrorValue).ToArray()
                },
                new ErrorForSeparatedDto {
                    WordAddress = new WordAddress {
                        ParagraphIndex = thirdParagraph,
                        WordIndex = thirdWord
                    },
                    Errors = thirdGroup.Select(x => x.ErrorValue).ToArray()
                },
            };

            var errors = firstGroup.Concat(secondGroup).Concat(thirdGroup).ToArray();

            await _sut.Insert(errors);
            var insertedErrors = await _sut.GetErrors(result.Id);

            insertedErrors.Should().BeEquivalentTo(expectedResult);
        }
    }
}
