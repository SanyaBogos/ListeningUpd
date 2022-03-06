using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Blog
{
    public class PostsDto
    {
        public PostDto[] Posts { get; set; }
        public TopicDto[] AllTopics { get; set; }
        public PriorityDto[] AllPriorities { get; set; }
    }
}
