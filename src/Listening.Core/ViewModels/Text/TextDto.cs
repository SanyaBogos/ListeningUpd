using Listening.Server.Entities.Specialized;

namespace Listening.Core.ViewModels.Text
{
    public class TextDto : LogInfo, IDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Text { get; set; }
        public string AudioName { get; set; }
        public string VideoName { get; set; }
        public string Country { get; set; }
        public long Assignee { get; set; }
        public int Complexity { get; set; }
    }
}
