using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NxtLib.ServerInfo;

namespace NxtLib.Internal
{
    internal class ApiTagsConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var reply = new Dictionary<string, ApiTag>();
                var jObject = JObject.Load(reader);
                foreach (var jPropery in jObject.Children<JProperty>())
                {
                    var jtoken = jPropery.Children().First();
                    var apiTag = new ApiTag
                    {
                        Enabled = Convert.ToBoolean(((JValue)jtoken.SelectToken(Parameters.Enabled)).Value),
                        Name = ((JValue)jtoken.SelectToken(Parameters.Name)).Value.ToString()
                    };
                    reply.Add(jPropery.Name, apiTag);
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