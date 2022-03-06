using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels.Text
{
    public class TextDescriptionDto
    {
        public string TextId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string AudioName { get; set; }
        public string VideoName { get; set; }
        public string Country { get; set; }
        public int? Complexity { get; set; }
    }
}
