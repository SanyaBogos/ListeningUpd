using Listening.Core.Entities.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Entities.Specialized.Result
{
    [Table("Listening_Results")]
    public class Result : ResultBase, IIdenticable<long>, IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }

        [MaxLength(25)]
        public string TextId { get; set; }

        [MaxLength(8000)]
        public bool[] ResultsEncodedString { get; set; }

        [MaxLength(1)]
        public char Mode { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ErrorForJoined> ErrorForJoined { get; set; }
        public virtual ICollection<ErrorForSeparated> ErrorsForSeparated { get; set; }
    }
}
