using System;
using Newtonsoft.Json;
using NxtLib.Networking;
using NxtLib.ServerInfo;

namespace NxtLib.Internal
{
    internal class PeerStateConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if ((reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer) && objectType == typeof(PeerReply.PeerState))
            {
                var stateInt = Convert.ToInt32(reader.Value.ToString());
                switch (stateInt)
                {
                    case 0:
                        return PeerReply.PeerState.NonConnected;
                    case 1:
                        return PeerReply.PeerState.Connected;
                    case 2:
                        return PeerReply.PeerState.Disconnected;
                }
            }
            throw new NotSupportedException(string.Format("objectType {0} and TokenType {1} is not supported", objectType, reader.TokenType));
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}