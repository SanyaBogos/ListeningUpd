using Listening.Core.Entities.Custom;
using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Listening.Core.Entities.Specialized.MiniBlog
{
    [Table("Blog_Posts")]
    public class Post : LogInfo, IIdenticable<long>, IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(350)]
        public string Header { get; set; }

        [MaxLength(3500)]
        public string Description { get; set; }

        public long UserId { get; set; }

        public int PriorityId { get; set; }

        public string Message { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("PriorityId")]
        public virtual Priority Priority { get; set; }

        public IList<Attachment> Attachments { get; set; }

        public IList<PostTopic> PostTopics { get; set; }
    }
}
