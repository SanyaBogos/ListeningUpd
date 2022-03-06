using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Blog
{
    public class SinglePostDto: LogInfo
    {
        public long Id { get; set; }

        public string Header { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public int PriorityId { get; set; }
        public string[] Topics { get; set; }
        public string Message { get; set; }
    }
}
