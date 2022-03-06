using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Core.ViewModels.ListeningResult
{
    public class WordsCalculatedResultDto
    {
        public int SymbolsCountWithoutSign { get; set; }
        public int GuessedSymbolsCount { get; set; }
        public int HintedSymbolsCount { get; set; }

        public int WordsCountWithoutSign { get; set; }
        public int FullyGuessedWordsCount { get; set; }
        public int PartitionallyGuessedWordsCount { get; set; }
        public int TotallyHintedWordsCount { get; set; }
    }
}
