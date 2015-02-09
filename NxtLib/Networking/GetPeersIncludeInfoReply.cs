using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.Networking
{
    public class GetPeersIncludeInfoReply : BaseReply
    {
        [JsonProperty(PropertyName = "peers")]
        public List<PeerInfo> PeerList { get; set; }
    }
}