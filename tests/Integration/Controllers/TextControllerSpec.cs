using FluentAssertions;
using Infrastructure.Helpers;
using Integration;
using Integration.Extensions;
using Listening.Server.Controllers.api;
using Listening.Server.Entities.Specialized.Text;
using Listening.Server.Repositories.Mongo;
using Listening.Core.ViewModels;
using Listening.Core.ViewModels.Text;
using MongoDB.Bson;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Controllers
{
    [Collection("Database collection")]
    public class TextControllerSpec : BaseIntegrationTest<TextController>
    {
        private readonly TextsMongoRepository _textRepository;

        public TextControllerSpec(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _textRepository = new TextsMongoRepository(_fixture.Configuration);
        }

        [Fact]
        public async Task ShouldFillLogDataAtPostAndPut()
        {
            var textDto = new TextDto
            {
                Title = $"{DatabaseFixture.OtherTestLabel}sdf",
                AudioName = $"{RandomHelper.String()}.{DatabaseFixture.AudioType}",
                Country = RandomHelper.CountryCode(),
                Text = "one two three"
            };

            Text textInDb = await CheckPostText(textDto);

            textInDb.CreatedBy.Should().NotBeNull();
            textInDb.CreatedDate.Should().NotBeNull();

            textDto.Title = $"{textDto.Title}123";
            textDto.Id = textInDb.Id.ToString();
            Text updatedTextInDb = await CheckPutText(textDto, textInDb);

            updatedTextInDb.CreatedBy.Should().NotBeNull();
            updatedTextInDb.CreatedDate.Should().NotBeNull();
            updatedTextInDb.CreatedBy.HasValue.Should().BeTrue();
            updatedTextInDb.CreatedBy.Value.Should<long>().BeEquivalentTo(textInDb.CreatedBy.Value);
            updatedTextInDb.CreatedDate.HasValue.Should().BeTrue();
            updatedTextInDb.CreatedDate.Value.Should<DateTime>().BeEquivalentTo(textInDb.CreatedDate.Value);
            updatedTextInDb.LastModifiedDate.Should().NotBeNull();
            updatedTextInDb.UpdatedBy.Should().NotBeNull();

            await CleanupText(textDto);
        }

        [Fact]
        public async Task ShouldFillAssigneeAtPostIfZero()
        {
            var textDto = new TextDto
            {
                Title = $"{DatabaseFixture.OtherTestLabel}sdf",
                AudioName = $"{RandomHelper.String()}.{DatabaseFixture.AudioType}",
                Country = RandomHelper.CountryCode(),
                Text = "one two three"
            };

            Text textInDb = await CheckPostText(textDto);

            textInDb.Assignee.Should().NotBe(0);
            textInDb.Assignee.Should().BeGreaterThan(0);

            await CleanupText(textDto);
        }

        [Fact]
        public async Task ShouldFillAssigneeAtPost()
        {
            var textDto = new TextDto
            {
                Title = $"{DatabaseFixture.OtherTestLabel}sdf",
                AudioName = $"{RandomHelper.String()}.{DatabaseFixture.AudioType}",
                Country = RandomHelper.CountryCode(),
                Text = "one two three",
                Assignee = _fixture.NewUser.Id
            };

            Text textInDb = await CheckPostText(textDto);

            textInDb.Assignee.Should().Be(textDto.Assignee);

            await CleanupText(textDto);
        }

        private async Task<Text> CheckPutText(TextDto textDto, Text textInDb)
        {
            var textResponse = await _fixture.AdminClient.PutAsync($"api/Text/updateText",
                textDto.Serialize().AsContent());
            textResponse.EnsureSuccessStatusCode();
            return await _textRepository.GetById(textInDb.Id);
        }

        private async Task<Text> CheckPostText(TextDto textDto)
        {
            var textResponse = await _fixture.AdminClient.PostAsync($"api/Text/addText",
                textDto.Serialize().AsContent());
            textResponse.EnsureSuccessStatusCode();

            var result = await textResponse.Deserialize<StringIdsDto>();
            var id = result.Ids.First();
            return await _textRepository.GetById(ObjectId.Parse(id));
        }

        private async Task CleanupText(TextDto textDto)
        {
            await _fixture.AdminClient.DeleteAsync($"api/Text/{textDto.Id}");
        }
    }
}
