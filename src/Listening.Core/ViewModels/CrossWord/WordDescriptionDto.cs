using System;

namespace Listening.Core.ViewModels.CrossWord
{
    public class WordDescriptionDto
    {
        public long Id { get; set; }
        public long QuestionDescriptionId { get; set; }
        public int StartPointX { get; set; }
        public int StartPointY { get; set; }
        public char Direction { get; set; }
    }
}
