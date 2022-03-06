using Listening.Server.Entities.Specialized.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels.Text
{
    public class LetterLocatorDto : WordAddress
    {
        public int SymbolIndex { get; set; }
    }
}
