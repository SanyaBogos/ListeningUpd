using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.CrossWord
{
    public class AdminCrosswordQueryViewModel : CrosswordQueryViewModel
    {
        public bool IncludeAssignee { get; set; }
        public bool IncludeCreateDate { get; set; }
        public bool IncludeUpdateDate { get; set; }
    }
}
