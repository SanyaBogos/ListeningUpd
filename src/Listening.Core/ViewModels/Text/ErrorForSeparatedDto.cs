using Listening.Server.Entities.Specialized.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels.Text
{
    public class ErrorForSeparatedDto
    {
        public WordAddress WordAddress { get; set; }
        public string[] Errors { get; set; }
    }
}
