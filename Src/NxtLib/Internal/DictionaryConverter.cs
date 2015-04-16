using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.Internal
{
    internal class DictionaryConverter<TKey, TValue> : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var reply = new Dictionary<TKey, TValue>();
                reader.Read();
                while (reader.TokenType != JsonToken.EndObject)
                {
                    var propertyName = (TKey) reader.Value;
                    reader.Read();
                    var propertyValue = (TValue) reader.Value;
                    reader.Read();
                    reply.Add(propertyName, propertyValue);
                }
                return reply;
            }
            throw new ArgumentException();
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
