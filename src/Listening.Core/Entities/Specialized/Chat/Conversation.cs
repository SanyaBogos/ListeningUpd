using Listening.Core.Entities.Custom;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listening.Core.Entities.Specialized.Chat
{
    [Table("Chat_Conversations")]
    public class Conversation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long FromUserId { get; set; }
        public long ToGroupId { get; set; }

        [MaxLength(1000)]
        public string Message { get; set; }
        public DateTime Time { get; set; }

        [ForeignKey("FromUserId")]
        public virtual ApplicationUser FromUser { get; set; }

        [ForeignKey("ToGroupId")]
        public virtual ApplicationUser ToGroup { get; set; }
    }
}
