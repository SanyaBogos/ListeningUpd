using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Server.Utilities
{
    public static class TextTransform
    {
        public static string[] SpecialSymbols { get; } =
            new string[] { ",", ".", "?", ":", ";", "-", "!", "\"" };

        public static char[] SpecialCharacters { get; } =
            SpecialSymbols.Select(char.Parse).ToArray();

        public static char[] MoneySymbols { get; } =
            new char[] { '$', '£', '€', '₴', '₽' };

        public static bool IsSpecialSymbolWord(string word)
        {
            return word.All(x => SpecialCharacters.Contains(x) || MoneySymbols.Contains(x));
        }

        public static string[][] GetWordCounts(string[][] wordsInParagraphs)
        {
            var wordsCounts = new List<string[]>();

            foreach (var hiddenWordsInParagraphs in wordsInParagraphs)
            {
                var hiddenWordsLengthInText = new List<string>();

                foreach (var word in hiddenWordsInParagraphs)
                    if (IsSpecialSymbolWord(word))
                        hiddenWordsLengthInText.Add(word);
                    else
                        hiddenWordsLengthInText.Add(word.Length.ToString());

                wordsCounts.Add(hiddenWordsLengthInText.ToArray());
            }

            return wordsCounts.ToArray();
        }

        public static StringBuilder GetTextFromArray(string[][] wordsInParagraphs)
        {
            var sb = new StringBuilder();
            foreach (var paragraph in wordsInParagraphs)
            {
                foreach (var wordOrSymbol in paragraph)
                    if (SpecialSymbols.Except(new string[] { "-" }).Contains(wordOrSymbol))
                        sb.Append($"{wordOrSymbol}");
                    else
                        sb.Append($" {wordOrSymbol}");

                sb.AppendLine();
            }

            return sb;
        }

        public static List<string[]> GetWordInParagraphsByText(string text)
        {
            var formattedText = text.Replace("’", "'").Replace("--", "-").Replace("—", "-")
                        .Replace("\"", " \" ").Replace("  ", " ");

            foreach (var symbol in SpecialSymbols)
                formattedText = formattedText.Replace($" {symbol}", $"{symbol}");

            var paragraphs = formattedText
                .Split(new string[] { "\r\n", "\n", "\n " }, StringSplitOptions.RemoveEmptyEntries);
            var wordsInParagraphs = new List<string[]>();

            foreach (var paragraph in paragraphs)
            {
                var words = new List<string>();
                foreach (var word in paragraph.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                    DevideWordAndSpecialSymbols(words, word);
                wordsInParagraphs.Add(words.ToArray());
            }

            return wordsInParagraphs;
        }

        public static int GetSymbolCount(string[][] wordsCounts)
        {
            int count = 0;

            foreach (var paragrapth in wordsCounts)
                foreach (var word in paragrapth)
                    if (int.TryParse(word, out int x))
                        count += x;
                    else
                        count++;

            return count;
        }

        public static void DevideWordAndSpecialSymbols(List<string> words, string word)
        {
            var specialSymbols = SpecialSymbols.Select(x => Convert.ToChar(x));
            var endIndexer = word.Length;
            var startIndexer = 0;

            if (MoneySymbols.Contains(word.First()))
            {
                words.Add(word.First().ToString());
                startIndexer++;
            }

            while (specialSymbols.Contains(word[endIndexer - 1]))
                endIndexer--;

            if (endIndexer != startIndexer)
                words.Add(word.Substring(startIndexer, endIndexer - startIndexer));

            if (word.Length != endIndexer)
                words.Add(word.Substring(endIndexer, word.Length - endIndexer));
        }

        public static int[] GetParagrphsSymbolsCounts(string[][] wordsInParagraphs)
        {
            return wordsInParagraphs
                .Select(x => x.Select(y => !TextTransform.IsSpecialSymbolWord(y) ? y.Length : 1)
                .ToArray().Sum())
                .ToArray();
        }
    }
}
