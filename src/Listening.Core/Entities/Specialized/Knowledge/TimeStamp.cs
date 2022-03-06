using Listening.Core.Entities.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Listening.Core.Entities.Specialized.Knowledge
{
    [Table("Knowledge_TimeStamps")]
    public class TimeStamp
    {
        [Key]
        public long VideoId { get; set; }
        public long UserId { get; set; }
        public int Seconds { get; set; }

        [MaxLength(2000)]
        public string Comment { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("VideoId")]
        public virtual Video Video { get; set; }
    }
}
