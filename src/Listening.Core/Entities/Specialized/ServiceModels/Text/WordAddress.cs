using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Entities.Specialized.ServiceModels
{
    public class WordAddress : IEqualityComparer<WordAddress>
    {
        public int ParagraphIndex { get; set; }
        public int WordIndex { get; set; }

        public bool Equals(WordAddress x, WordAddress y)
        {
            if (x == null && y == null)
                return true;
            if (x == null | y == null)
                return false;

            if (x.ParagraphIndex == y.ParagraphIndex
                                && x.WordIndex == y.WordIndex)
                return true;

            return false;
        }

        public int GetHashCode(WordAddress obj)
        {
            return (ParagraphIndex.GetHashCode() ^ WordIndex.GetHashCode()).GetHashCode();
        }
    }
}
