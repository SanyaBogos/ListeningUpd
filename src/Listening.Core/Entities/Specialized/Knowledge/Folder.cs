using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Listening.Core.Entities.Specialized.Knowledge
{
    [Table("Knowledge_Folders")]
    public class Folder : IIdenticable<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CourseId { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Name { get; set; }

        //[Required]
        [MaxLength(500)]
        public string Path { get; set; }

        // [MaxLength(2000)]
        // public string AdditionalFolders { get; set; }


        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
    }
}
