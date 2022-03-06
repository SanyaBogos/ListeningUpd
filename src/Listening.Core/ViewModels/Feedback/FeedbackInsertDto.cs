using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Feedback
{
    public class FeedbackInsertDto
    {
        public string Topic { get; set; }
        public string Details { get; set; }
        public bool IsVisible { get; set; }
    }
}
