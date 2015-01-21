using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.DigitalGoodsStoreOperations
{
    public class GoodPurchase : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "buyer")]
        public ulong BuyerId { get; set; }
        public string BuyerRs { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliveryDeadlineTimestamp { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "discountNQT")]
        public Amount Discount { get; set; }
        public List<GoodsFeedbackNote> FeedbackNotes { get; set; }
        public GoodsData GoodsData { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "goods")]
        public ulong GoodsId { get; set; }
        public bool GoodsIsText { get; set; }
        public string Name { get; set; }
        public EncryptedDataReply Note { get; set; }
        public bool Pending { get; set; }
        public List<string> PublicFeedbacks { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "purchase")]
        public ulong PurchaseId { get; set; }
        public int Quantity { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "refundNQT")]
        public Amount Refund { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "priceNQT")]
        public Amount Price { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "seller")]
        public ulong SellerId { get; set; }
        public string SellerRs { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}