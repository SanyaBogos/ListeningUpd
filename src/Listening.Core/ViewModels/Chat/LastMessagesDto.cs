using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Chat
{
    public class UserMessageDto
    {
        public long Id { get; set; }
        public bool IsMine { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
