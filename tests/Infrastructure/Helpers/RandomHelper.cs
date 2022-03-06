using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helpers;

namespace Infrastructure.Helpers
{
    public static class RandomHelper
    {
        private const int DefaultGuidCount = 25;

        private static Random _random;

        static RandomHelper()
        {
            _random = new Random(DateTime.Now.Millisecond);
        }

        public static string String(int from = 0, int count = DefaultGuidCount)
        {
            if (count == 0)
                return ".";

            var newGuidStr = Guid.NewGuid().ToString();

            if (count > DefaultGuidCount)
            {
                var sb = new StringBuilder(count);
                sb.Append(newGuidStr);
                var iterationsCount = count / DefaultGuidCount;

                for (int i = 0; i < iterationsCount; i++)
                    sb.Append(Guid.NewGuid().ToString());

                newGuidStr = sb.ToString();
            }

            return newGuidStr.Replace("-", string.Empty)
                .Substring(from, count);
        }

        public static bool[] GenerateBoolArray(int length)
        {
            var boolArray = new List<bool>();
            for (int i = 0; i < length; i++)
                boolArray.Add(_random.Next() % 2 == 0);
            return boolArray.ToArray();
        }

        public static char RandomCharForMode()
        {
            return _random.NextDouble() > 0.5 ? 's' : 'j';
        }

        public static int Quantity(int? max = null)
        {
            return max.HasValue ? _random.Next(max.Value) : _random.Next();
        }

        public static string[][] StringsArrays(int count)
        {
            var newStrings = new string[count];

            newStrings[0] = String(0, 3);
            for (int i = 1; i < count; i++)
                newStrings[i] = String(0, _random.Next() % 30);

            return new string[][] { newStrings };
        }

        public static string CountryCode()
        {
            var countryCodes = CountriesHelper.AllCountries().Keys;
            return countryCodes.ElementAt(_random.Next() % countryCodes.Count);
        }
    }
}
