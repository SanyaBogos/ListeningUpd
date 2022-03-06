using Listening.Core.ViewModels.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.CrossWord
{
    public class CrosswordDescriptionEnhancedDto : CrosswordDescriptionDto
    {
        public string Assignee { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
