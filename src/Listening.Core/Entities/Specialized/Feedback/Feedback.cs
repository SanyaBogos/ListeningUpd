using Listening.Core.Entities.Custom;
using Listening.Server.Entities.Specialized;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listening.Core.Entities.Specialized.Feedback
{
    [Table("Listening_Feedbacks")]
    public class Feedback : IIdenticable<long>, IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(100)]
        public string Topic { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Details { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        [Required]
        public bool IsVisible { get; set; }

        public long UserId { get; set; }


        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
