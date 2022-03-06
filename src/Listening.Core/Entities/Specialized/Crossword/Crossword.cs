using Listening.Core.Entities.Custom;
using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listening.Core.Entities.Specialized.Crossword
{
    [Table("Crossword_Crosswords")]
    public class Crossword : IIdenticable<long>, IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long CrosswordDescriptionId { get; set; }

        [Required]
        public long WordDescriptionId { get; set; }


        [ForeignKey("CrosswordDescriptionId")]
        public virtual CrosswordDescription CrosswordDescription { get; set; }


        [ForeignKey("WordDescriptionId")]
        public virtual WordDescription WordDescription { get; set; }

        public virtual ICollection<CrosswordResult> CrosswordResults { get; set; }
    }
}
