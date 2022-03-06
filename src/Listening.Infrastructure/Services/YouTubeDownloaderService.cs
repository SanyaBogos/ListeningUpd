using Listening.Core.ViewModels.YouTube;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace Listening.Infrastructure.Services
{
    public class YouTubeDownloaderService : IYouTubeDownloaderService
    {
        private static Dictionary<string, MediaStreamInfoSet> _videoCache = new Dictionary<string, MediaStreamInfoSet>();

        public YouTubeDownloaderService()
        {

        }

        public async Task<Stream> Download(string link)
        {
            var client = new YoutubeClient();
            Stream streamResult = new MemoryStream();
            MediaStreamInfoSet streamInfoSet;

            if (_videoCache.ContainsKey(link))
                streamInfoSet = _videoCache[link];
            else
                streamInfoSet = await GetMediaStreamInfoSet(link);

            var bestQualityVideo = streamInfoSet.Video.WithHighestVideoQuality();
            //bestQualityVideo.
            //client.GetVideoMediaStreamInfosAsync
            await client.DownloadMediaStreamAsync(bestQualityVideo, streamResult);
            return streamResult;
        }

        public async Task<VideoStreamInfoViewModel> GetVideoInfo(string link)
        {
            if (_videoCache.ContainsKey(link))
                return GetInfoVM(_videoCache[link]);

            var streamInfoSet = await GetMediaStreamInfoSet(link);

            _videoCache.Add(link, streamInfoSet);

            return GetInfoVM(streamInfoSet);

            //var videoQualities = streamInfoSet.GetAllVideoQualities();
            //foreach (var videoQuality in videoQualities)
            //{
            //    var result1 = new VideoStreamInfoViewModel
            //    {
            //        Resolution = videoQuality.
            //    };
            //}
        }

        private static async Task<MediaStreamInfoSet> GetMediaStreamInfoSet(string link)
        {
            string linkString;
            var client = new YoutubeClient();

            if (!link.Contains("youtu.be"))
            {
                var match = Regex.Match(link, @"v=(\S+)");
                linkString = match.Groups[1].Value;
            }
            else
            {
                var index = link.LastIndexOf('/');
                linkString = link.Substring(index >= 0 ? index : link.LastIndexOf("%2F"));
            }

            try
            {
                var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(linkString);
                return streamInfoSet;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        private VideoStreamInfoViewModel GetInfoVM(MediaStreamInfoSet streamInfoSet)
        {
            var bestQualityVideo = streamInfoSet.Video.WithHighestVideoQuality();

            var result = new VideoStreamInfoViewModel
            {
                Resolution = bestQualityVideo.Resolution.ToString(),
                VideoEncoding = bestQualityVideo.VideoEncoding.ToString(),
                Bitrate = bestQualityVideo.Bitrate,
                Framerate = bestQualityVideo.Framerate,
                VideoQuality = bestQualityVideo.VideoQuality.ToString(),
                VideoQualityLabel = bestQualityVideo.VideoQualityLabel
            };

            return result;
        }
    }
}
