using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Listening.Core.Entities.Specialized.Knowledge
{
    [Table("Knowledge_Books")]
    public class Book : IIdenticable<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int FileTypeId { get; set; }

        public int CourseId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Path { get; set; }

        [ForeignKey("FileTypeId")]
        public virtual FileType FileType { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
