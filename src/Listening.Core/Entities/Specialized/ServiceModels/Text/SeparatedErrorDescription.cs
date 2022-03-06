using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Entities.Specialized.ServiceModels
{
    public class SeparatedErrorDescription
    {
        public WordAddress WordAddress { get; set; }
        public string Error { get; set; }

    }
}
