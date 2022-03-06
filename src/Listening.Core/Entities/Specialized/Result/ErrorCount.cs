using System.ComponentModel.DataAnnotations.Schema;

namespace Listening.Server.Entities.Specialized.Result
{
    [Table("Listening_ErrorCounts")]
    public class ErrorCount
    {
        public long ResultId { get; set; }
        public int Count { get; set; }
    }
}
