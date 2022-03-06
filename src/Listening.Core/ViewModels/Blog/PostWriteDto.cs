using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Blog
{
    public class PostWriteDto : LogInfo
    {
        public long Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public int[] TopicIds { get; set; }
        public int PriorityId { get; set; }
        public string Message { get; set; }
    }
}
