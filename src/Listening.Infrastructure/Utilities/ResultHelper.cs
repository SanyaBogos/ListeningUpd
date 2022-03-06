using Listening.Server.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Utilities
{
    public class ResultHelper
    {
        public static List<string[][]> MergeEncodingWithText(
            bool[] resultsEncodedString,
            string[][] wordsInParagraphs)
        {
            var encodedIndex = 0;
            var mergedText = new List<string[][]>();

            foreach (var paragraph in wordsInParagraphs)
            {
                var newParagraph = new List<string[]>();

                foreach (var word in paragraph)
                {
                    if (TextTransform.IsSpecialSymbolWord(word))
                    {
                        newParagraph.Add(new string[] { word });
                        //encodedIndex += word.Length * 2;
                        encodedIndex += 2;
                        continue;
                    }

                    var wordMerged = new List<string>();

                    for (int k = 0; k < word.Length; k++, encodedIndex += 2)
                        wordMerged.Add(resultsEncodedString[encodedIndex]
                            ? word[k].ToString() : (k + 1).ToString());

                    newParagraph.Add(wordMerged.ToArray());
                }

                mergedText.Add(newParagraph.ToArray());
            }

            return mergedText;
        }
    }
}
