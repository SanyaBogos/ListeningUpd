using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Listening.Core.Entities.Specialized.MiniBlog
{
    [Table("Blog_PostTopics")]
    public class PostTopic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public long PostId { get; set; }
        public Post Post { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
