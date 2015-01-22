using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NxtLib.Internal
{
    internal class ByteToHexStringConverter : JsonConverter
    {
        internal static string ToHexString(IEnumerable<byte> bytes)
        {
            return BitConverter.ToString(bytes.ToArray()).Replace("-", "").ToLowerInvariant();
        }

        internal static IEnumerable<byte> ToBytes(string hexString)
        {
            var numberChars = hexString.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return bytes;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String && objectType == typeof(IEnumerable<byte>))
            {
                return ToBytes(reader.Value.ToString());
            }
            throw new NotSupportedException(string.Format("objectType {0} and TokenType {1} is not supported", objectType, reader.TokenType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
