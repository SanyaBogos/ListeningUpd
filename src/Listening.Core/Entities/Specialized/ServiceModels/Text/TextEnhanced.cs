using Listening.Core.ViewModels.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Entities.Specialized.ServiceModels
{
    public class TextEnhanced : TextDto, IIdenticable<string>
    {
        public string[][] WordsInParagraphs { get; set; }
        public string[][] CountsInParagraphs { get; set; }
        public int[] ParagrphsSymbolsCounts { get; set; }
        public int SymbolsCount { get; set; }
    }
}
