using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Blog
{
    public class AdditionalDto
    {
        public TopicDto[] Topics { get; set; }
        public PriorityDto[] Priorities { get; set; }
        public string[] Videos { get; set; }
        public string VideoFolderName { get; set; }
    }
}
