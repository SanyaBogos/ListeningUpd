
using Listening.Core.ViewModels.YouTube;
using Listening.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Controllers.api
{
    [Route("api/[controller]")]
    public class YouTubeDownloaderController  //: BaseController
    {
        private readonly IYouTubeDownloaderService _youTubeDownloaderService;

        public YouTubeDownloaderController(IYouTubeDownloaderService youTubeDownloaderService)
        {
            _youTubeDownloaderService = youTubeDownloaderService;
        }

        [HttpGet("videoInfo/{link}")]
        public async Task<VideoStreamInfoViewModel> GetVideoInfo(string link)
        {
            var result = await _youTubeDownloaderService.GetVideoInfo(link);
            return result;
        }

        [HttpGet("download/{link}")]
        public async Task<FileStreamResult> Download(string link)
        {
            var result = await _youTubeDownloaderService.Download(link);
            var fsResult = new FileStreamResult(result, new MediaTypeHeaderValue("video/mp4"))
            {
                FileDownloadName = "test.mp4"
            };
            return fsResult;
        }
    }
}
