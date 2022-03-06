using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Text
{
    public class AdminTextQueryViewModel : TextQueryViewModel
    {
        public bool IncludeAssignee { get; set; }
        public bool IncludeCreateDate { get; set; }
        public bool IncludeUpdateDate { get; set; }
    }
}
