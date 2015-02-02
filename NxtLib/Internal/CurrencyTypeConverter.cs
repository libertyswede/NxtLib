using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
                if (objectType == typeof(HashSet<CurrencyType>))
                {
                    var list = new HashSet<CurrencyType>();
                    reader.Read();
                    while (reader.TokenType != JsonToken.EndArray)
                    {
                        list.Add(GetTypeBasedOnDescription(reader.Value.ToString()));
                        reader.Read();
                    }
                    return list;
                }
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                if (objectType == typeof(HashSet<CurrencyType>))
                {
                    return GetTypesBasedOnValue(Convert.ToInt32(reader.Value));
                }
            }
            return reader.Value;
        }

        private static HashSet<CurrencyType> GetTypesBasedOnValue(int type)
        {
            var types = new HashSet<CurrencyType>();

            foreach (var currencyType in Enum.GetValues(typeof(CurrencyType)).Cast<CurrencyType>())
            {
                if ((type & (int)currencyType) != 0)
                {
                    types.Add(currencyType);
                }
            }

            return types;
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