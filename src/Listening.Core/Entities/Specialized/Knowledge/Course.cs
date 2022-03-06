using Listening.Core.Entities.Custom;
using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Listening.Core.Entities.Specialized.Knowledge
{
    [Table("Knowledge_Courses")]
    public class Course : IIdenticable<int>
    {
        // public Course()
        // {
        //     ApplicationRoles = new HashSet<ApplicationRole>();
        // }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TypeId { get; set; }

        public int AuthorId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(5055)]
        public string Description { get; set; }

        [MaxLength(3000)]
        public string OriginalSite { get; set; }

        [MaxLength(3000)]
        public string OriginalLink { get; set; }

        [ForeignKey("TypeId")]
        public virtual Type Type { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }


        public virtual ICollection<Folder> Folders { get; set; }
        public virtual ICollection<Book> Books { get; set; }

        public virtual ICollection<Access> Accesses { get; set; }

    }
}
