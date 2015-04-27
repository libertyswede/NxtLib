using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.Internal
{
    internal class DictionaryConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var reply = new Dictionary<string, sbyte>();
                reader.Read();
                while (reader.TokenType != JsonToken.EndObject)
                {
                    var propertyName = (string)reader.Value;
                    reader.Read();
                    var propertyValue = (sbyte)(long)reader.Value;
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
