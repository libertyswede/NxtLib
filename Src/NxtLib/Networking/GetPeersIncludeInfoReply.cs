using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Networking
{
    public class GetPeersIncludeInfoReply : BaseReply
    {
        [JsonProperty(PropertyName = Parameters.Peers)]
        public List<PeerInfo> PeerList { get; set; }
    }
}