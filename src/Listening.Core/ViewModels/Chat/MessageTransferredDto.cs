using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Chat
{
    public class MessageTransferredDto
    {
        public long FromUserId { get; set; }
        public string ToUserSignalRId { get; set; }
        public string FromUserName { get; set; }
        public string Message { get; set; }
    }
}
