using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels.Text
{
    public class TextForGuessingDto
    {
        public string[][] WordsCounts { get; set; }
        public string[][][] MergedText { get; set; }
        public bool[] ResultsEncodedString { get; set; }
        public string[] ErrorsForJoined { get; set; }
        public ErrorForSeparatedDto[] ErrorsForSeparated { get; set; }
        public bool IsStarted { get; set; }
    }
}
