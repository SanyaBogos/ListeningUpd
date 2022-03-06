using Listening.Core.Entities.Custom;
using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listening.Core.Entities.Specialized.Crossword
{
    [Table("Crossword_CrosswordDescriptions")]
    public class CrosswordDescription : LogInfo, IIdenticable<long>, IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        public string SubTitle { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public long AssigneeId { get; set; }

        [Required]
        public string Country { get; set; }

        // from 1 to 5; easy - 1; the hardest - 5. 0 - no value (instead of nullability)
        public int Complexity { get; set; }


        [ForeignKey("AssigneeId")]
        public virtual ApplicationUser Assignee { get; set; }

        public virtual ICollection<Crossword> Crosswords { get; set; }
    }
}
