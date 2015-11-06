using System;

namespace NxtLib.Local
{
    internal class StringConverter
    {
        private static readonly char[] BaseCharacters =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'e', 'g', 'h', 'i', 'j', 'k',
            'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        internal string ToBase32String(long value)
        {
            const int targetBase = 32;
            var i = 32;
            var buffer = new char[i];

            do
            {
                buffer[--i] = BaseCharacters[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            var result = new char[32 - i];
            Array.Copy(buffer, i, result, 0, 32 - i);

            return new string(result);
        }
    }
}
