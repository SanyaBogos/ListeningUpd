using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels.ListeningResult
{
    public class ResultDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }

        public WordsCalculatedResultDto CalculatedResult { get; set; }

        public char Mode { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public int? TimeSpentMiliSeconds { get; set; }
        public bool IsCompleted { get; set; }
        public int ErrorsCount { get; set; }
    }
}
