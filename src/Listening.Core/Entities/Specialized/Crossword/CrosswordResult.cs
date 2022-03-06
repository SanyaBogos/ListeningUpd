using Listening.Core.Entities.Custom;
using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listening.Core.Entities.Specialized.Crossword
{
    [Table("Crossword_CrosswordResults")]
    public class CrosswordResult : ResultBase, IIdenticable<long>, IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long CrosswordId { get; set; }

        [Required]
        public long WordDescriptionId { get; set; }

        [MaxLength(600)]
        public bool[] ResultsEncodedString { get; set; }


        [ForeignKey("CrosswordId")]
        public virtual Crossword Crossword { get; set; }


        [ForeignKey("WordDescriptionId")]
        public virtual WordDescription WordDescription { get; set; }

        // public virtual ICollection<QuestionDescription> QuestionDescriptions { get; set; }
    }
}
