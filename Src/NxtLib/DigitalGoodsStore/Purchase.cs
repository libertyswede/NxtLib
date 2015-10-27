using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.DigitalGoodsStore
{
    public class Purchase
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Buyer)]
        public ulong BuyerId { get; set; }
        public string BuyerRs { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliveryDeadlineTimestamp { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.DiscountNqt)]
        public Amount Discount { get; set; }
        public List<EncryptedDataReply> FeedbackNotes { get; set; }
        public EncryptedDataReply GoodsData { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Goods)]
        public ulong GoodsId { get; set; }
        public bool GoodsIsText { get; set; }
        public string Name { get; set; }
        public EncryptedDataReply Note { get; set; }
        public bool Pending { get; set; }
        public List<string> PublicFeedbacks { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Purchase)]
        public ulong PurchaseId { get; set; }
        public int Quantity { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.RefundNqt)]
        public Amount Refund { get; set; }
        public EncryptedDataReply RefundNote { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.PriceNqt)]
        public Amount Price { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Seller)]
        public ulong SellerId { get; set; }
        public string SellerRs { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}