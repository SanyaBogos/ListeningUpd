using Listening.Core.ViewModels.YouTube;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IYouTubeDownloaderService
    {
        Task<VideoStreamInfoViewModel> GetVideoInfo(string link);
        Task<Stream> Download(string link);
    }
}
