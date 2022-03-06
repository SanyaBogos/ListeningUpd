using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System.Data.SqlTypes;
using Listening.Server.Entities.Specialized.Result;
using Infrastructure.Helpers;
using Integration.Extensions;
using Integration;
using Listening.Server.Repositories.Postgres;

namespace Integration.Repositoreis
{
    [Collection("Database collection")]
    public class ResultsRepositorySpec : BaseIntegrationTest<ResultRepository>
    {
        public ResultsRepositorySpec(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _sut = new ResultRepository(_fixture.Configuration);
        }

        [Fact]
        public async Task CheckPostgresResultsCRUD()
        {
            var results = _fixture.GenerateResults(30, 3);

            await _sut.Insert(results);

            foreach (var result in results)
                (await _sut.GetById(result.Id)).Should().BeEquivalentTo(result);

            results = _fixture.GenerateResults(30, 3);

            await _sut.Update(results);

            foreach (var result in results)
                (await _sut.GetById(result.Id)).Should().BeEquivalentTo(result);

            await _sut.Delete(results.Select(x => x.Id).ToArray());
        }

        [Fact]
        public async Task CheckBuildResult()
        {
            var id = long.MaxValue - 10;
            var result = new Result
            {
                Id = id,
                UserId = _fixture.NewUser.Id,
                TextId = RandomHelper.String(),
                Started = new SqlDateTime(DateTime.Now.AddDays(-13)).Value,
                ResultsEncodedString = RandomHelper.GenerateBoolArray(10),
                Mode = RandomHelper.RandomCharForMode(),
                IsStarted = false,
                IsCompleted = false
            };

            await _sut.Insert(new Result[] { result });

            var nonCompletedResult = await _sut.GetNonCompletedResult(
                new Result
                {
                    UserId = result.UserId,
                    TextId = result.TextId,
                    Mode = result.Mode
                });

            nonCompletedResult.ResultsEncodedString.Should().BeEquivalentTo(result.ResultsEncodedString);
            nonCompletedResult.Finished.Should().BeNull();
            nonCompletedResult.IsCompleted.Should().BeFalse();

            await _sut.Delete(new long[] { id });
        }

        [Fact]
        public async Task ShouldReturnNullIfNoElementWithSuchId()
        {
            (await _sut.GetById(long.MaxValue - 100))
                .Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnNullIfNoElementWithIncorrectTextId()
        {
            (await _sut.GetNonCompletedResult(
                new Result
                {
                    UserId = _fixture.NewUser.Id,
                    TextId = RandomHelper.String(),
                    Mode = RandomHelper.RandomCharForMode()
                }))
                .Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnNullIfNoElementWithIncorrectUserId()
        {
            (await _sut.GetNonCompletedResult(
                new Result
                {
                    UserId = long.MaxValue - 1,
                    TextId = _fixture.Results.First().TextId,
                    Mode = RandomHelper.RandomCharForMode()
                })).Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnNullIfNoElementWithWrongUserAndTextId()
        {
            (await _sut.GetNonCompletedResult(
                new Result
                {
                    //UserId = Guid.NewGuid().ToString(),
                    UserId = long.MaxValue - 1,
                    TextId = RandomHelper.String(),
                    Mode = RandomHelper.RandomCharForMode()
                }))
                .Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnValueFromGetNonCompletedResult()
        {
            var results = _fixture.GenerateResults(40, 2, 'j', false);
            await _sut.Insert(results);

            var resultFromDb = await _sut.GetNonCompletedResult(results.First());
            resultFromDb.Should().NotBeNull();
            resultFromDb.Should().BeEquivalentTo(results.First());

            await _sut.Delete(results.Select(x => x.Id));
        }

        [Fact]
        public async Task ShouldReturnNullFromGetNonCompletedResult()
        {
            var results = _fixture.GenerateResults(50, 2, 'j', false);
            await _sut.Insert(results);

            var resultForSearch = results.First();
            resultForSearch.Mode = 's';
            var resultFromDb = await _sut.GetNonCompletedResult(resultForSearch);
            resultFromDb.Should().BeNull();

            await _sut.Delete(results.Select(x => x.Id));
        }
    }
}
