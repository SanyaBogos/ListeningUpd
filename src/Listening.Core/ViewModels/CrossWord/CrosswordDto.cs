using System;

namespace Listening.Core.ViewModels.CrossWord
{
    public class CrosswordDto
    {
        public long Id { get; set; }
        public long CrosswordDescriptionId { get; set; }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public long AssigneeId { get; set; }
        public string Country { get; set; }

        public int Complexity { get; set; }

        public long[] WordDescriptionIds { get; set; }
    }
}
