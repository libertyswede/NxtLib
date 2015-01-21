using System;
using System.Collections.Generic;
using System.Linq;

namespace NxtLib.Internal
{
    internal static class ByteToHexStringConverter
    {
        internal static string ToHexString(IEnumerable<byte> bytes)
        {
            return BitConverter.ToString(bytes.ToArray()).Replace("-", "").ToLowerInvariant();
        }

        internal static IEnumerable<byte> ToByteArray(string hexString)
        {
            var numberChars = hexString.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
