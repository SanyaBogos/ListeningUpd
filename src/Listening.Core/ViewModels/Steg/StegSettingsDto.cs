using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Steg
{
    public class StegSettingsDto
    {
        public string FileName { get; set; }
        public string Message { get; set; }
        public int[] Colors { get; set; }
        public char Mode { get; set; }

        public int StartIndex { get; set; }
        public int Step { get; set; }

        public SimpleDto Simple { get; set; }
        public FuncDto Func { get; set; }
    }
}
