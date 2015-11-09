using System;
using System.Collections.Generic;
using System.Linq;

namespace NxtLib.Internal
{
    internal class StringConverter
    {
        private static readonly char[] Base32Array =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v'
        };
        private static readonly Dictionary<char, int> Base32Dictionary;

        static StringConverter()
        {
            Base32Dictionary = Base32Array
                .Select((c, i) => new { Value = c, Index = i })
                .ToDictionary(x => x.Value, x => x.Index);
        }

        internal string ToBase32String(long value)
        {
            const int targetBase = 32;
            var i = 32;
            var buffer = new char[i];

            do
            {
                buffer[--i] = Base32Array[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            var result = new char[32 - i];
            Array.Copy(buffer, i, result, 0, 32 - i);

            return new string(result);
        }

        internal long FromBase32String(string encodedString)
        {
            var result = 0L;

            for (var currentCharIndex = encodedString.Length - 1; currentCharIndex >= 0; currentCharIndex--)
            {
                var next = encodedString[currentCharIndex];
                int nextCharIndex;

                if (!Base32Dictionary.TryGetValue(next, out nextCharIndex))
                {
                    throw new ArgumentException("Input includes illegal characters.");
                }

                result += (long)Math.Pow(32, encodedString.Length - 1 - currentCharIndex) * nextCharIndex;
            }

            return result;
        }
    }
}
