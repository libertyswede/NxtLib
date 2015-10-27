using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib
{
    public class BlocksReply<T> : BaseReply
    {
        [JsonProperty(PropertyName = Parameters.Blocks)]
        public List<Block<T>> BlockList { get; set; }
    }
}