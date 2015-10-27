using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Networking
{
    public class GetPeersReply : BaseReply
    {
        [JsonProperty(PropertyName = Parameters.Peers)]
        public List<string> PeerList { get; set; }
    }
}