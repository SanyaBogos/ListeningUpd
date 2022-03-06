using Listening.Core.Entities.Custom;
using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listening.Core.Entities.Specialized.Crossword
{
    [Table("Crossword_QuestionDescriptions")]
    public class QuestionDescription : LogInfo, IIdenticable<long>, IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Question { get; set; }

        [Required]
        [MaxLength(300)]
        public string Answer { get; set; }

        // [Required]
        // public int CrosswordDescriptionId { get; set; }


        // [ForeignKey("CrosswordDescriptionId")]
        // public virtual CrosswordDescription CrosswordDescription { get; set; }

        public virtual ICollection<WordDescription> WordDescriptions { get; set; }
    }
}
