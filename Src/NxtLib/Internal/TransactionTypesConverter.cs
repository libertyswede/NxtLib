using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NxtLib.ServerInfo;

namespace NxtLib.Internal
{
    internal class TransactionTypesConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var reply = new Dictionary<int, Dictionary<int, TransactionType>>();
                var jObject = JObject.Load(reader);
                foreach (var transactionType in jObject.Children<JProperty>())
                {
                    var transactionDictionary = new Dictionary<int, TransactionType>();
                    reply.Add(Convert.ToInt32(transactionType.Name), transactionDictionary);

                    foreach (var subTypeProperty in transactionType.Value.Children().First().Children().First().Children<JProperty>())
                    {
                        var subTypeObject = subTypeProperty.Value;
                        transactionDictionary.Add(Convert.ToInt32(subTypeProperty.Name), new TransactionType
                        {
                            CanHaveRecipient = Convert.ToBoolean(((JValue) subTypeObject.SelectToken("canHaveRecipient")).Value),
                            IsPhasingSafe = Convert.ToBoolean(((JValue) subTypeObject.SelectToken("isPhasingSafe")).Value),
                            MustHaveRecipient = Convert.ToBoolean(((JValue) subTypeObject.SelectToken("mustHaveRecipient")).Value),
                            Name = ((JValue) subTypeObject.SelectToken("name")).Value.ToString(),
                            SubType = (int)(long) ((JValue) subTypeObject.SelectToken("subtype")).Value,
                            Type = (int)(long)((JValue)subTypeObject.SelectToken("type")).Value
                        });
                    }
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
