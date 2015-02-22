using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
{
    public class AssetTrade
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong AskOrder { get; set; }
        public int AskOrderHeight { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "asset")]
        public ulong AssetId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong BidOrder { get; set; }
        public int BidOrderHeight { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "block")]
        public ulong BlockId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Buyer { get; set; }
        public string BuyerRs { get; set; }
        public int Decimals { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "priceNQT")]
        public Amount Price { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong QuantityQnt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Seller { get; set; }
        public string SellerRs { get; set; }
        public string TradeType { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}