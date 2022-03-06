using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listening.Core.ViewModels.DebianFAI;

namespace Listening.Server.Entities.Specialized.ServiceModels.DebianFAI
{
    public class ArhitecturesWithTime
    {
        public Dictionary<ArchitectureType, string> Arhitectures { get; set; }
        public DateTime Time { get; set; }
    }
}
