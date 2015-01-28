using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.ServerInfo
{
    public class GetTimeReply : BaseReply
    {
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Time { get; set; }
    }
}