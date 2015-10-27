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
        [JsonProperty(PropertyName = Parameters.Asset)]
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
        public int Height { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.PriceNqt)]
        public Amount Price { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long QuantityQnt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Seller { get; set; }
        public string SellerRs { get; set; }
        public string TradeType { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}