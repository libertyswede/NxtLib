using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.Networking
{
    public class GetPeersReply : BaseReply
    {
        [JsonProperty(PropertyName = "peers")]
        public List<string> PeerList { get; set; }
    }
}