using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Entities.Specialized.ServiceModels.ListeningResult
{
    public class ResultData
    {
        public Result.Result Result { get; set; }
        public WordAddress[] WordAddresses { get; set; }
        public int[] WordsLengths { get; set; }
        public string[] JoinedErrors { get; set; }
        public SeparatedErrorDescription SeparatedError { get; set; }
    }
}
