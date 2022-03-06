using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Listening.Core.Entities.Specialized.Knowledge
{
    [Table("Knowledge_Authors")]
    public class Author : IIdenticable<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CourseId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public virtual ICollection<Course> Books { get; set; }
    }
}
