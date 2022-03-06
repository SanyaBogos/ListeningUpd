using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Chat
{
    public class ChatAvailableUsersListDto
    {
        public ChatAvailableUserDto[] Active { get; set; }
        public ChatAvailableUserDto[] Inactive { get; set; }
    }
}
