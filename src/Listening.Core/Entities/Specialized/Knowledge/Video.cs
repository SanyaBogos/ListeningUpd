using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Listening.Core.Entities.Specialized.Knowledge
{
    [Table("Knowledge_Videos")]
    public class Video: IIdenticable<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int FolderId { get; set; }

        //public int TypeId { get; set; }
        public int VideoTypeId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Path { get; set; }

        [MaxLength(5500)]
        public string Description { get; set; }

        public int Repeat { get; set; } = 0;


        [ForeignKey("FolderId")]
        public virtual Folder Folder { get; set; }

        //[ForeignKey("TypeId")]
        //public virtual Type Type { get; set; }

        [ForeignKey("VideoTypeId")]
        public virtual VideoType VideoType { get; set; }

        public virtual ICollection<TimeStamp> TimeStamps { get; set; }

    }
}
