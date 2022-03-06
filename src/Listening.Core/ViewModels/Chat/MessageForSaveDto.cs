using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Chat
{
    public class MessageForSaveDto
    {
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
        public string Message { get; set; }
    }
}
