using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.YouTube
{
    public class VideoStreamInfoViewModel
    {
        public long Bitrate { get; set; }
        public string VideoEncoding { get; set; }
        public string VideoQualityLabel { get; set; }
        public string VideoQuality { get; set; }
        public string Resolution { get; set; }
        public int Framerate { get; set; }
    }
}
