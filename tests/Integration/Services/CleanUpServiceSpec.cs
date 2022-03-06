using FluentAssertions;
using Listening.Server.Entities.Specialized.Text;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Repositories.Duplicates;
using Listening.Server.Services;
using Listening.Server.Services.Contracts;
using Listening.Server.Services.Duplicates;
using Integration.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Integration;

namespace Integration.Services
{
    [Collection("Database collection")]
    public class CleanUpServiceSpec : BaseIntegrationTest<CleanupService>
    {
        public CleanUpServiceSpec(DatabaseFixture fixture)
        {
            _fixture = fixture;
            var fileService = (IFileServiceDuplicate)_fixture.Server.Host.Services
                .GetService(typeof(IFileServiceDuplicate));
            var textRepository = (ITextMongoRepoDuplicate)_fixture.Server.Host.Services
                .GetService(typeof(ITextMongoRepoDuplicate));
            _sut = new CleanupService(fileService, textRepository);
            _fixture.PrepareUnexistedMediaFiles();
        }

        [Fact]
        public async Task ShouldRemoveAudiosForUnexistedTextIfAny()
        {
            // check preparation correctness
            var unexistedFiles = Directory.GetFiles(_fixture.AudioPath, $"{DatabaseFixture.UnexistedAudioNameBase}*.*");
            unexistedFiles.Should().NotBeNullOrEmpty();

            // act 
            await _sut.RemoveFilesForUnexistedTexts(Listening.Core.FileContentType.Audio);

            // check if removed
            unexistedFiles = Directory.GetFiles(_fixture.AudioPath, $"{DatabaseFixture.UnexistedAudioNameBase}*.*");
            unexistedFiles.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldRemoveVideosForUnexistedTextIfAny()
        {
            // check preparation correctness
            var unexistedFiles = Directory.GetFiles(_fixture.VideoPath, $"{DatabaseFixture.UnexistedVideoNameBase}*.*");
            unexistedFiles.Should().NotBeNullOrEmpty();

            // act 
            await _sut.RemoveFilesForUnexistedTexts(Listening.Core.FileContentType.Video);

            // check if removed
            unexistedFiles = Directory.GetFiles(_fixture.VideoPath, $"{DatabaseFixture.UnexistedVideoNameBase}*.*");
            unexistedFiles.Should().BeNullOrEmpty();
        }
    }
}
