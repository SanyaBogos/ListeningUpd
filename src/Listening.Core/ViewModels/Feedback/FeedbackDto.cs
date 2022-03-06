using System;

namespace Listening.Core.ViewModels.Feedback
{
    public class FeedbackDto
    {
        public long Id { get; set; }
        public string Topic { get; set; }
        public string Details { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsVisible { get; set; }
        public string Email { get; set; }
    }
}
