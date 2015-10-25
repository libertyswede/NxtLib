using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace NxtLib.Internal.LocalSign
{
    internal class ReedSolomon
    {
        private static readonly int[] InitialCodeword = { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private static readonly int[] Gexp = { 1, 2, 4, 8, 16, 5, 10, 20, 13, 26, 17, 7, 14, 28, 29, 31, 27, 19, 3, 6, 12, 24, 21, 15, 30, 25, 23, 11, 22, 9, 18, 1 };
        private static readonly int[] Glog = { 0, 0, 1, 18, 2, 5, 19, 11, 3, 29, 6, 27, 20, 8, 12, 23, 4, 10, 30, 17, 7, 22, 28, 26, 21, 25, 9, 16, 13, 14, 24, 15 };
        private static readonly int[] CodewordMap = { 3, 2, 1, 0, 7, 6, 5, 4, 13, 14, 15, 16, 12, 8, 9, 10, 11 };
        private const string Alphabet = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";

        private const int Base32Length = 13;
        private const int Base10Length = 20;

        public static ulong Decode(string addressRs)
        {
            var cypherString = RemovePrefix(addressRs);
            var codeword = new int[InitialCodeword.Length];
            Array.Copy(InitialCodeword, 0, codeword, 0, InitialCodeword.Length);

            var codewordLength = 0;

            foreach (var t in cypherString)
            {
                var positionInAlphabet = Alphabet.IndexOf(t);

                if (positionInAlphabet <= -1 || positionInAlphabet > Alphabet.Length)
                    continue;

                if (codewordLength > 16)
                    throw new InvalidReedSolomonAddressException(addressRs, nameof(addressRs));

                var codeworkIndex = CodewordMap[codewordLength];
                codeword[codeworkIndex] = positionInAlphabet;
                codewordLength += 1;
            }

            if (codewordLength == 17 && !IsCodewordValid(codeword) || codewordLength != 17)
            {
                throw new InvalidReedSolomonAddressException(addressRs, nameof(addressRs));
            }

            var length = Base32Length;
            var cypherString32 = new int[length];
            for (var i = 0; i < length; i++)
            {
                cypherString32[i] = codeword[length - i - 1];
            }

            var plainStringBuilder = new StringBuilder();
            do
            { // base 32 to base 10 conversion
                var newLength = 0;
                var digit10 = 0;

                for (var i = 0; i < length; i++)
                {
                    digit10 = digit10 * 32 + cypherString32[i];

                    if (digit10 >= 10)
                    {
                        cypherString32[newLength] = digit10 / 10;
                        digit10 %= 10;
                        newLength += 1;
                    }
                    else if (newLength > 0)
                    {
                        cypherString32[newLength] = 0;
                        newLength += 1;
                    }
                }
                length = newLength;
                plainStringBuilder.Append((char)(digit10 + '0'));
            } while (length > 0);

            var bigInt = BigInteger.Parse(Reverse(plainStringBuilder.ToString()));
            return (ulong)bigInt;
        }

        private static string RemovePrefix(string cypherString)
        {
            if (cypherString.StartsWith("NXT-"))
            {
                cypherString = cypherString.Substring(4);
            }
            return cypherString;
        }

        public static string Reverse(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length == 1)
                return str;

            var chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        private static bool IsCodewordValid(IReadOnlyList<int> codeword)
        {
            var sum = 0;

            for (var i = 1; i < 5; i++)
            {
                var t = 0;

                for (var j = 0; j < 31; j++)
                {
                    if (j > 12 && j < 27)
                        continue;

                    var pos = j;
                    if (j > 26)
                        pos -= 14;

                    t ^= Gmult(codeword[pos], Gexp[(i * j) % 31]);
                }
                sum |= t;
            }
            return sum == 0;
        }

        public static string Encode(ulong id)
        {
            var plainString10 = new int[Base10Length];
            var stringId = (id).ToString(CultureInfo.InvariantCulture);
            var length = stringId.Length;
            for (var i = 0; i < length; i++)
            {
                plainString10[i] = stringId[i] - '0';
            }

            var codewordLength = 0;
            var codeword = new int[InitialCodeword.Length];

            do
            {
                var newLength = 0;
                var digit32 = 0;
                for (var i = 0; i < length; i++)
                {
                    digit32 = digit32 * 10 + plainString10[i];
                    if (digit32 >= 32)
                    {
                        plainString10[newLength] = digit32 >> 5;
                        digit32 &= 31;
                        newLength += 1;
                    }
                    else if (newLength > 0)
                    {
                        plainString10[newLength] = 0;
                        newLength += 1;
                    }
                }
                length = newLength;
                codeword[codewordLength] = digit32;
                codewordLength += 1;
            } while (length > 0);

            int[] p = { 0, 0, 0, 0 };

            for (var i = Base32Length - 1; i >= 0; i--)
            {
                var fb = codeword[i] ^ p[3];
                p[3] = p[2] ^ Gmult(30, fb);
                p[2] = p[1] ^ Gmult(6, fb);
                p[1] = p[0] ^ Gmult(9, fb);
                p[0] = Gmult(17, fb);
            }

            Array.Copy(p, 0, codeword, Base32Length, InitialCodeword.Length - Base32Length);

            var cypherStringBuilder = new StringBuilder("NXT-");
            for (var i = 0; i < 17; i++)
            {
                var codeworkIndex = CodewordMap[i];
                var alphabetIndex = codeword[codeworkIndex];
                cypherStringBuilder.Append(Alphabet[alphabetIndex]);

                if ((i & 3) == 3 && i < 13)
                {
                    cypherStringBuilder.Append('-');
                }
            }
            return cypherStringBuilder.ToString();
        }

        private static int Gmult(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                return 0;
            }

            var idx = (Glog[a] + Glog[b]) % 31;

            return Gexp[idx];
        }
    }
}
