using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.Internal
{
    internal class StringToIntegralTypeConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                if (objectType == typeof (List<ulong>))
                {
                    var list = new List<ulong>();
                    reader.Read();
                    while (reader.TokenType != JsonToken.EndArray)
                    {
                        list.Add(Convert.ToUInt64(reader.Value.ToString()));
                        reader.Read();
                    }
                    return list;
                }
            }
            if (reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer)
            {
                var value = reader.Value.ToString();
                if (string.IsNullOrEmpty(value) &&
                    (objectType == typeof (byte?) || objectType == typeof (short?) || objectType == typeof (int?) ||
                     objectType == typeof (long?) || objectType == typeof (ulong?)))
                {
                    return null;
                }
                if (objectType == typeof(byte) || objectType == typeof(byte?))
                {
                    return Convert.ToByte(value);
                }
                if (objectType == typeof(short) || objectType == typeof(short?))
                {
                    return Convert.ToInt16(value);
                }
                if (objectType == typeof(int) || objectType == typeof(int?))
                {
                    return Convert.ToInt32(value);
                }
                if (objectType == typeof(long) || objectType == typeof(long?))
                {
                    return Convert.ToInt64(value);
                }
                if (objectType == typeof(ulong) || objectType == typeof(ulong?))
                {
                    return Convert.ToUInt64(value);
                }
            }

            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }
}