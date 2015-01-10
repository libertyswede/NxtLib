using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib
{
    public class BlocksReply<T> : BaseReply
    {
        [JsonProperty(PropertyName = "blocks")]
        public List<Block<T>> BlockList { get; set; }
    }
}