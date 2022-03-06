using FluentAssertions;
using Infrastructure.Helpers;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Repositories.Postgres;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Repositoreis
{
    [Collection("Database collection")]
    public class ErrorForJoinedRepositorySpec : BaseIntegrationTest<ErrorForJoinedRepository>
    {
        public ErrorForJoinedRepositorySpec(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _sut = new ErrorForJoinedRepository(_fixture.Configuration);
        }

        [Fact]
        public async Task CheckGetErrorsForJoined()
        {
            var result = _fixture.Results.First();

            var errors = new ErrorForJoined[]
            {
                new ErrorForJoined { ResultId = result.Id, ErrorValue = RandomHelper.String(5) },
                new ErrorForJoined { ResultId = result.Id, ErrorValue = RandomHelper.String(5) },
                new ErrorForJoined { ResultId = result.Id, ErrorValue = RandomHelper.String(5) },
            };

            await _sut.Insert(errors);
            var insertedErrors = await _sut.GetErrors(result.Id);

            insertedErrors.Should().BeEquivalentTo(
                errors.Select(x => x.ErrorValue).ToArray());
        }
    }
}
