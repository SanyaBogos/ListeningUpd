using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels.File
{
    public class VideoFileViewModel
    {
        public string FileName { get; set; }
        /// <summary>
        /// Time to live in seconds
        /// </summary>
        public int TTL { get; set; }

        public VideoFileViewModel() { }

        public VideoFileViewModel(string fileName) : this(fileName, 0) { }

        public VideoFileViewModel(string fileName, int secondsTTL)
        {
            FileName = fileName;
            TTL = secondsTTL;
        }
    }
}
