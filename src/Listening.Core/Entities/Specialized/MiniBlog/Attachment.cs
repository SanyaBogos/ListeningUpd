using Listening.Server.Entities.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Listening.Core.Entities.Specialized.MiniBlog
{
    [Table("Blog_Attachments")]
    public class Attachment : IIdenticable<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Path { get; set; }

        public int MediatypeId { get; set; }

        public long PostId { get; set; }


        public MediaType Mediatype { get; set; }

        public Post Post { get; set; }
    }
}
