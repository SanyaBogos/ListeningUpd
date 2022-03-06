using Listening.Core.Entities.Custom;
using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listening.Core.Entities.Specialized.Crossword
{
    // !!! here is one more dependency inside Change_Direction_Type migration (don't rename untill change table name in all places)
    [Table("Crossword_WordDescriptions")]
    public class WordDescription : IIdenticable<long>, IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public int StartPointX { get; set; }

        [Required]
        public int StartPointY { get; set; }

        [Required]
        // 0 - right
        // 1 - down
        // 2 - left
        // 3 - up
        public char Direction { get; set; }


        [Required]
        public long QuestionDescriptionId { get; set; }


        [ForeignKey("QuestionDescriptionId")]
        public virtual QuestionDescription QuestionDescription { get; set; }
        
        public virtual ICollection<Crossword> Crosswords { get; set; }
        public virtual ICollection<CrosswordResult> CrosswordResults { get; set; }
    }
}
