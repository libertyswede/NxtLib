using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                if (objectType == typeof(CurrencyType) || objectType == typeof (CurrencyType?))
                {
                    return (CurrencyType) (long) reader.Value;
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
            foreach (CurrencyType currencyType in Enum.GetValues(typeof(CurrencyType)))
            {
                var type = currencyType.GetType();
                var name = Enum.GetName(type, currencyType);
                var displayAttribute = type.GetField(name)
                    .GetCustomAttributes(false)
                    .OfType<NxtApiAttribute>()
                    .SingleOrDefault();

                if (displayAttribute != null && string.Equals(displayAttribute.Name, description))
                {
                    return currencyType;
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