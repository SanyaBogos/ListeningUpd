using Listening.Core.Entities.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Listening.Core.Entities.Specialized.Chat
{
    [Table("Chat_UsersInGroups")]
    public class ChatUsersInGroups
    {
        public long UserId { get; set; }
        public long ChatGroupId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("ChatGroupId")]
        public virtual ChatGroup Group { get; set; }
    }
}
