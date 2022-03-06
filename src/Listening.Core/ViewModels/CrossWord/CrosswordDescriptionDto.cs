using System;

namespace Listening.Core.ViewModels.CrossWord
{
    public class CrosswordDescriptionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public long AssigneeId { get; set; }
        public string Country { get; set; }        
    }
}
