using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels.File
{
    public class CuttingOptionsViewModel
    {
        public string FileName { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}
