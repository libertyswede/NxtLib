using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NxtLib.ServerInfo;

namespace NxtLib.Internal
{
    internal class TransactionSubTypesConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var reply = new Dictionary<string, TransactionType>();
                var jObject = JObject.Load(reader);
                foreach (var transactionType in jObject.Children<JProperty>())
                {
                    var jtoken = transactionType.Children().First();
                    var subtype = new TransactionType
                    {
                        CanHaveRecipient = Convert.ToBoolean(((JValue)jtoken.SelectToken("canHaveRecipient")).Value),
                        IsPhasingSafe = Convert.ToBoolean(((JValue)jtoken.SelectToken("isPhasingSafe")).Value),
                        MustHaveRecipient = Convert.ToBoolean(((JValue)jtoken.SelectToken("mustHaveRecipient")).Value),
                        Name = ((JValue)jtoken.SelectToken(Parameters.Name)).Value.ToString(),
                        SubType = (int)(long)((JValue)jtoken.SelectToken(Parameters.SubType)).Value,
                        Type = (int)(long)((JValue)jtoken.SelectToken(Parameters.Type)).Value
                    };
                    reply.Add(transactionType.Name, subtype);
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
