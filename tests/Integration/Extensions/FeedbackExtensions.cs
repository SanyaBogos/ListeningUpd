using Listening.Core.Entities.Specialized.Feedback;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Helpers;

namespace Integration.Extensions
{
    public static class FeedbackExtensions
    {
        internal static List<Feedback> GenerateFeedbacks(this DatabaseFixture fixture, int shift = 0, int count = 5)
        {
            var results = new List<Feedback>();

            for (int i = 0; i < count; i++)
                results.Add(new Feedback
                {
                    Id = long.MaxValue - i - shift - 1,
                    Topic = string.Format(DatabaseFixture.TestLabelEnhanced, i),
                    Details = RandomHelper.String(0, 100),
                    CreatedTime = DateTime.Now,
                    UserId = fixture.NewUser.Id,
                    IsVisible = i % 2 == 0
                });

            return results;
        }

        internal static async Task InsertFeedbacks(this DatabaseFixture fixture)
        {
            fixture.Feedbacks = fixture.GenerateFeedbacks(count: 10);
            await fixture.FeedbackRepository.Insert(fixture.Feedbacks);
        }
    }
}
