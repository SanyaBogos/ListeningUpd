using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Spec
{
    public class TimeStampDto
    {
        public long VideoId { get; set; }
        public long UserId { get; set; }
        public int Seconds { get; set; }
        public string Comment { get; set; }
    }
}
