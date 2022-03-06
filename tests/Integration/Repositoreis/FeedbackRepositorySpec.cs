using AutoMapper;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Integration.Extensions;
using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using Listening.Infrastructure.Repositories.Postgres;
using Listening.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Repositoreis
{
    [Collection("Database collection")]
    public class FeedbackRepositorySpec : BaseIntegrationTest<FeedbackRepository>
    {
        private readonly IMapper _mapper;

        public FeedbackRepositorySpec(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _sut = new FeedbackRepository(_fixture.Configuration);
            //_fixture.getse
            _mapper = (IMapper)Listening.Web.Startup.ServiceProvider.GetService(typeof(IMapper));
        }

        [Theory]
        [InlineData(DatabaseFixture.TestLabelSimple, 2, 1, true)]
        [InlineData(DatabaseFixture.TestLabelSimple, 2, 2, true)]
        [InlineData(DatabaseFixture.TestLabelSimple, 2, 3, true)]
        [InlineData(DatabaseFixture.TestLabelSimple, 2, 1, false)]
        [InlineData(DatabaseFixture.TestLabelSimple, 2, 2, false)]
        [InlineData(DatabaseFixture.TestLabelSimple, 2, 3, false)]
        [InlineData(DatabaseFixture.Unexisted, 2, 1, true)]
        [InlineData(DatabaseFixture.Unexisted, 2, 2, true)]
        [InlineData(DatabaseFixture.Unexisted, 2, 3, true)]
        [InlineData(DatabaseFixture.Unexisted, 2, 1, false)]
        [InlineData(DatabaseFixture.Unexisted, 2, 2, false)]
        [InlineData(DatabaseFixture.Unexisted, 2, 3, false)]
        public async Task CheckGetPaged(string filterName, int elementsPerPage, int page, bool isAsc)
        {
            var query = new FeedbackQueryViewModel
            {
                ElementsPerPage = elementsPerPage,
                Page = page,
                IsAscending = isAsc,
                FilteringProperties = new Dictionary<string, string> {
                    { $"{nameof(Feedback.Topic)}", filterName },
                }
            };

            var feedbacksFiltered = _fixture.Feedbacks.Where(x => x.Topic.Contains(filterName));
            var feedbacks = isAsc
                ? feedbacksFiltered.OrderBy(x => x.CreatedTime).ToList()
                : feedbacksFiltered.OrderByDescending(x => x.CreatedTime).ToList();
            var expected = feedbacks.Skip((query.Page - 1) * query.ElementsPerPage).Take(query.ElementsPerPage);
            var expectedCasted = _mapper.Map<FeedbackDto[]>(expected);
            foreach (var feedback in expectedCasted)
                feedback.Email = _fixture.NewUser.Email;

            var actual = await _sut.GetPaged(query);

            actual.Count.Should().Be(feedbacks.Count);
            actual.Data.Should().BeEquivalentTo(expectedCasted, EquivalentOptions);
        }

        [Fact]
        public async Task CheckPostgresResultsCRUD()
        {
            var feedbacks = _fixture.GenerateFeedbacks(11, 5);

            await _sut.Insert(feedbacks);

            foreach (var result in feedbacks)
                (await _sut.GetById(result.Id)).Should().BeEquivalentTo(result, EquivalentOptions);

            feedbacks = _fixture.GenerateFeedbacks(11, 5);

            await _sut.Update(feedbacks);

            foreach (var result in feedbacks)
                (await _sut.GetById(result.Id)).Should().BeEquivalentTo(result, EquivalentOptions);

            await _sut.Delete(feedbacks.Select(x => x.Id).ToArray());
        }

        private EquivalencyAssertionOptions<Feedback> EquivalentOptions(
            EquivalencyAssertionOptions<Feedback> options)
        {
            options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>();
            return options;
        }

        private EquivalencyAssertionOptions<FeedbackDto> EquivalentOptions(
            EquivalencyAssertionOptions<FeedbackDto> options)
        {
            options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>();
            return options;
        }
    }
}
