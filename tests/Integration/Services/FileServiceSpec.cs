using FluentAssertions;
using Listening.Server.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Services
{
    [Collection("Database collection")]
    public class FileServiceSpec : BaseIntegrationTest<FileService>
    {
        public FileServiceSpec(DatabaseFixture fixture)
        {
            _fixture = fixture;
            var logger = (ILogger<FileService>)_fixture.Server.Host.Services
                .GetService(typeof(ILogger<FileService>));
            var hostingEnv = (IWebHostEnvironment)_fixture.Server.Host.Services
                .GetService(typeof(IWebHostEnvironment));
            _fixture.Configuration["Data:TimeToLive"] = "5000";
            _sut = new FileService(_fixture.Configuration, logger, hostingEnv);
        }

        [Fact]
        public async Task ShouldSaveVideoFileAndCleanUpAfterTTL()
        {
            var link = "https://www.youtube.com/watch?v=EUHcNeg_e9g";
            var videoFile = await _sut.SaveVideoFile(link);
            var pathToAllVideos = $"{_fixture.CurrentDirectory}/wwwroot/video";
            var allVideos = Directory.GetFiles(pathToAllVideos);
            videoFile.Should().NotBeNull();
            videoFile.FileName.Should().NotBeNullOrEmpty();
            videoFile.TTL.Should<int>().BeEquivalentTo(3);
            allVideos.Any(x => x.Contains(videoFile.FileName)).Should().BeTrue();

            Thread.Sleep((videoFile.TTL + 5 + 1) * 1000);
            allVideos = Directory.GetFiles(pathToAllVideos);
            allVideos.Any(x => x.Contains(videoFile.FileName)).Should().BeFalse();
        }
    }
}
