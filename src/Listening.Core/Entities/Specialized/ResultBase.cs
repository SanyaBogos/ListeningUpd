using System;

namespace Listening.Server.Entities.Specialized
{
    public class ResultBase
    {
        public DateTime Started { get; set; }
        public DateTime? Finished { get; set; }
        public int? TimeSpentMiliSeconds { get; set; }
        public bool IsStarted { get; set; }
        public bool IsCompleted { get; set; }
    }
}
