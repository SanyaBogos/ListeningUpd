using Listening.Core.Entities.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Listening.Core.Entities.Specialized.Chat
{
    [Table("Chat_Dialogues")]
    public class Dialogue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }

        [MaxLength(1000)]
        public string Message { get; set; }
        public DateTime Time { get; set; }

        [ForeignKey("FromUserId")]
        public virtual ApplicationUser FromUser { get; set; }

        [ForeignKey("ToUserId")]
        public virtual ApplicationUser ToUser { get; set; }
    }
}
