using Listening.Server.Entities.Specialized.Result;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Helpers;

namespace Integration.Extensions
{
    public static class ResultExtensions
    {
        internal static List<Result> GenerateResults(this DatabaseFixture fixture, int shift = 0,
            int count = 5, char mode = ' ', bool isFinished = true)
        {
            var results = new List<Result>();

            for (int i = 0; i < count; i++)
                results.Add(new Result
                {
                    Id = long.MaxValue - i - shift - 1,
                    UserId = fixture.NewUser.Id,
                    TextId = RandomHelper.String(),
                    Started = new SqlDateTime(DateTime.Now.AddDays(-13)).Value,
                    Finished = isFinished ? new SqlDateTime(DateTime.Now).Value : (DateTime?)null,
                    ResultsEncodedString = RandomHelper.GenerateBoolArray(10),
                    Mode = mode == ' ' ? RandomHelper.RandomCharForMode() : mode,
                    IsStarted = false,
                    IsCompleted = i % 2 == 0
                });

            return results;
        }

        internal static async Task InsertUserResults(this DatabaseFixture fixture)
        {
            fixture.Results = fixture.GenerateResults(20);

            var existedItemsAsync = fixture.Results.Select(async x => await fixture.ResultRepository.GetById(x.Id));
            var existedItems = (await Task.WhenAll(existedItemsAsync))
                .Where(x => x != null);
            await fixture.ResultRepository.Insert(fixture.Results.Except(existedItems));
        }

        internal static async Task DeleteResults(this DatabaseFixture fixture)
        {
            await fixture.ResultRepository.Delete(fixture.Results.Select(x => x.Id));
        }

        internal static async Task DeleteResultsFromId(this DatabaseFixture fixture, long count)
        {
            var startFromId = long.MaxValue - count;
            await fixture.ResultRepository.DeleteResultsAfterId(startFromId - 1);
        }

        internal static async Task DeleteFeedbacksFromId(this DatabaseFixture fixture, long count)
        {
            var startFromId = long.MaxValue - count;
            await fixture.FeedbackRepository.DeleteResultsAfterId(startFromId - 1);
        }
    }
}
