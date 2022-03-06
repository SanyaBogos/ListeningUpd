using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listening.Core.Entities.Specialized
{
    [Table("System_UniqueAppIdGenerator")]
    public class UniqueAppIdGenerator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CurrentId { get; set; }
    }
}
