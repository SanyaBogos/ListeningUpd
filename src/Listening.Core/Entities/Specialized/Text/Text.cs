using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Entities.Specialized.Text
{
    public class Text : MongoBaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string[][] WordsInParagraphs { get; set; }
        public string AudioName { get; set; }
        public string VideoName { get; set; }
        public string Country { get; set; }
        public int SymbolsCount { get; set; }
        public long Assignee { get; set; }
        // from 1 to 5; easy - 1; the hardest - 5. 0 - no value (instead of nullability)
        public int Complexity { get; set; }
        public string Topic { get; set; }
    }
}
