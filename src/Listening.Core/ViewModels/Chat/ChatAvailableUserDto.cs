using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Chat
{
    public class ChatAvailableUserDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SignalRId { get; set; }
    }
}
