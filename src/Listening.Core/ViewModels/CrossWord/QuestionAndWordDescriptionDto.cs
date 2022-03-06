using System;

namespace Listening.Core.ViewModels.CrossWord
{
    public class QuestionAndWordDescriptionDto
    {
        public long WordDescriptionId { get; set; }
        public long QuestionDescriptionId { get; set; }
        public int StartPointX { get; set; }
        public int StartPointY { get; set; }
        public byte Direction { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        // public int Length { get; set; }
    }
}
