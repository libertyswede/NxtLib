using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
{
    public class ExpectedOrderCancellationReply : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "lastBlock")]
        public ulong LastBlockId { get; set; }
        public List<OrderCancellation> OrderCancellations { get; set; }
    }
}