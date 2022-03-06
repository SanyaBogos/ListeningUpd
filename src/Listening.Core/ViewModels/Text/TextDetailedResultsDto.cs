using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Text
{
    public class TextDetailedResultsDto
    {
        public string[][][] MergedText { get; set; }
        public string[] Errors { get; set; }
    }
}
