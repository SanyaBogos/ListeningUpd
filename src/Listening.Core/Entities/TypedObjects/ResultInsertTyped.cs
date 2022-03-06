using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.Entities.TypedObjects
{
    public class ResultInsertTyped
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string TextId { get; set; }
        public bool[] ResultsEncodedString { get; set; }
        public char Mode { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Finished { get; set; }
        public bool IsStarted { get; set; }
        public bool IsCompleted { get; set; }
    }
}
