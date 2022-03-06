using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Listening.Server.Security;

namespace Listening.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string GetFileNameFromPath(this string path)
        {
            return path.Substring(StartIndexFileName(path));
        }

        public static string GetUpdatedFileNameFromPath(this string path, string addition)
        {
            var i = path.LastIndexOf(".");
            var result = $"{path.Substring(0, i)}{addition}{path.Substring(i)}";
            return result;
        }

        public static string GetFileTypeFromPath(this string path)
        {
            return path.Substring(path.LastIndexOf(".") + 1);
        }

        public static string GetFirstPartBeforeSymbol(this string str, char symbol)
        {
            var spaceIndex = str.IndexOf(symbol);
            var result = str.Substring(0, spaceIndex >= 0 ? spaceIndex : str.Length);
            return result;
        }

        public static string GetPureFileName(this string path)
        {
            var fromIndex = StartIndexFileName(path);
            var lastIndex = path.LastIndexOf(".");
            var count = lastIndex - fromIndex;
            return path.Substring(fromIndex, count);
        }

        public static string WithoutLastSymbol(this string str)
        {
            return str.Substring(0, str.Length - 1);
        }

        public static bool ContainsIgnoringCase(this string value, string searchedWork)
        {
            return value.Contains(searchedWork, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || /*c == '.' ||*/ c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static bool IsSign(this string word)
        {
            return !word.Any(x => char.IsLetterOrDigit(x));
            //return word.All(x => char.IsSeparator(x) || char.IsPunctuation(x));
        }

        public static byte[] GetByteArrayFromBitConverter(this string str)
        {
            int length = (str.Length + 1) / 3;
            byte[] resultArray = new byte[length];
            for (int i = 0; i < length; i++)
            {
                char sixteen = str[3 * i];
                if (sixteen > '9') sixteen = (char)(sixteen - 'A' + 10);
                else sixteen -= '0';

                char ones = str[3 * i + 1];
                if (ones > '9') ones = (char)(ones - 'A' + 10);
                else ones -= '0';

                resultArray[i] = (byte)(16 * sixteen + ones);
            }

            return resultArray;
        }

        public static bool IsUri(this string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                return false;

            return Uri.TryCreate(link, UriKind.Absolute, out Uri outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }

        public static int GetNumberAfterSymbol(this string str, string symbol, int digitsCount)
        {
            return Convert.ToInt32(str.Substring(str.IndexOf(symbol) + 1, digitsCount));
        }

        private static int StartIndexFileName(string path)
        {
            return path.LastIndexOfAny(new char[] { '/', '\\' }) + 1;
        }
    }
}
