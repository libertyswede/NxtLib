using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.DigitalGoodsStoreOperations
{
    public class Good : BaseReply
    {
        public bool Delisted { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "goods")]
        public ulong GoodsId { get; set; }
        public string Name { get; set; }
        public int NumberOfPublicFeedbacks { get; set; }
        public int NumberOfPurchases { get; set; }
        public List<string> ParsedTags { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "priceNQT")]
        public Amount Price { get; set; }
        public int Quantity { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "seller")]
        public ulong SellerId { get; set; }
        public string SellerRs { get; set; }
        public string Tags { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}