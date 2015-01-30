using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace NxtLib.Internal
{
    internal class CurrencyTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                if (objectType == typeof (List<CurrencyType>))
                {
                    var list = new List<CurrencyType>();
                    reader.Read();
                    while (reader.TokenType != JsonToken.EndArray)
                    {
                        list.Add(GetTypeBasedOnDescription(reader.Value.ToString()));
                        reader.Read();
                    }
                    return list;
                }
            }
            return reader.Value;
        }

        private static CurrencyType GetTypeBasedOnDescription(string description)
        {
            foreach (var field in typeof(CurrencyType).GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                    {
                        return (CurrencyType) field.GetValue(null);
                    }
                }
            }
            throw new NotSupportedException("Could not find CurrencyType with description: " + description);
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}