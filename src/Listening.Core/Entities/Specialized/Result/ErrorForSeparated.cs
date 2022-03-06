using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listening.Server.Entities.Specialized.Result
{
    [Table("Listening_ErrorsForSeparated")]
    public class ErrorForSeparated : IIdenticable<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ResultId { get; set; }
        public int ParagraphIndex { get; set; }
        public int WordIndex { get; set; }

        [MaxLength(50)]
        public string ErrorValue { get; set; }

        [ForeignKey("ResultId")]
        public virtual Result Result { get; set; }
    }
}
