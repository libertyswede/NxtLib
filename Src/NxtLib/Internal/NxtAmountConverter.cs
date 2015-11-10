using System;
using Newtonsoft.Json;

namespace NxtLib.Internal
{
    internal class NxtAmountConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if ((reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer) && objectType == typeof(Amount))
            {
                var nxtAmount = Convert.ToInt32(reader.Value.ToString());
                return Amount.CreateAmountFromNxt(nxtAmount);
            }
            throw new NotSupportedException($"objectType {objectType} and TokenType {reader.TokenType} is not supported");
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