using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NxtLib.ServerInfo;

namespace NxtLib.Internal
{
    internal class RequestTypesConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var reply = new List<RequestType>();
                var jObject = JObject.Load(reader);

                foreach (var requestTypeJson in jObject.Children<JProperty>())
                {
                    var requestType = new RequestType {Name = requestTypeJson.Name};
                    reply.Add(requestType);

                    foreach (var jsonProperty in requestTypeJson.Value.Children<JProperty>())
                    {
                        switch (jsonProperty.Name)
                        {
                            case "allowRequiredBlockParameters":
                                requestType.AllowRequiredBlockParameters = (bool) jsonProperty.Value;
                                break;
                            case "requirePassword":
                                requestType.RequirePassword = (bool) jsonProperty.Value;
                                break;
                            case "requireBlockchain":
                                requestType.RequireBlockchain = (bool) jsonProperty.Value;
                                break;
                            case "requirePost":
                                requestType.RequirePost = (bool) jsonProperty.Value;
                                break;
                        }
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