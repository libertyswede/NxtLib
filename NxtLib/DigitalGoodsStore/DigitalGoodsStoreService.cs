using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.DigitalGoodsStore
{
    public class DigitalGoodsStoreService : BaseService, IDigitalGoodsStoreService
    {
        public DigitalGoodsStoreService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public DigitalGoodsStoreService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<TransactionCreatedReply> Delisting(ulong goodsId, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"goods", goodsId.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("dgsDelisting", queryParameters);
        }

        public async Task<TransactionCreatedReply> Delivery(ulong purchaseId, CreateTransactionParameters parameters,
            Amount discount = null, string goodsToEncrypt = null, bool? goodsIsText = null, string goodsData = null,
            IEnumerable<byte> goodsNonce = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"purchase", purchaseId.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            if (discount != null)
            {
                AddToParametersIfHasValue("discountNQT", discount.Nqt, queryParameters);
            }
            AddToParametersIfHasValue("goodsToEncrypt", goodsToEncrypt, queryParameters);
            AddToParametersIfHasValue("goodsIsText", goodsIsText, queryParameters);
            AddToParametersIfHasValue("goodsData", goodsData, queryParameters);
            if (goodsNonce != null)
            {
                AddToParametersIfHasValue("goodsNonce", ByteToHexStringConverter.ToHexString(goodsNonce), queryParameters);
            }
            return await Post<TransactionCreatedReply>("dgsDelivery", queryParameters);
        }

        public async Task<TransactionCreatedReply> Feedback(ulong purchaseId, string message,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"purchase", purchaseId.ToString()},
                {"message", message}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("dgsFeedback", queryParameters);
        }

        public async Task<GoodReply> GetGood(ulong goodsId, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"goods", goodsId.ToString()}
            };
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<GoodReply>("getDGSGood", queryParameters);
        }

        public async Task<GoodsReply> GetGoods(ulong? sellerId = null, int? firstIndex = null, int? lastIndex = null,
            bool? inStockOnly = null, bool? hideDelisted = null, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("seller", sellerId, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("inStockOnly", inStockOnly, queryParameters);
            AddToParametersIfHasValue("hideDelisted", hideDelisted, queryParameters);
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<GoodsReply>("getDGSGoods", queryParameters);
        }

        public async Task<GoodsCountReply> GetGoodsCount(ulong? sellerId = null, bool? inStockOnly = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("seller", sellerId, queryParameters);
            AddToParametersIfHasValue("inStockOnly", inStockOnly, queryParameters);
            return await Get<GoodsCountReply>("getDGSGoodsCount", queryParameters);
        }

        public async Task<PuchaseCountReply> GetGoodsPurchaseCount(ulong goodsId, bool? withPublicFeedbacksOnly = null, bool? completed = null)
        {
            var queryParameters = new Dictionary<string, string>{{"goods", goodsId.ToString()}};
            AddToParametersIfHasValue("withPublicFeedbacksOnly", withPublicFeedbacksOnly, queryParameters);
            AddToParametersIfHasValue("completed", completed, queryParameters);
            return await Get<PuchaseCountReply>("getDGSGoodsPurchaseCount", queryParameters);
        }

        public async Task<PurchasesReply> GetGoodsPurchases(ulong goodsId, int? firstIndex = null, int? lastIndex = null,
            bool? withPublickKeedbacksOnly = null, bool? completed = null)
        {
            var queryParameters = new Dictionary<string, string> { { "goods", goodsId.ToString() } };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("withPublickKeedbacksOnly", withPublickKeedbacksOnly, queryParameters);
            AddToParametersIfHasValue("completed", completed, queryParameters);
            return await Get<PurchasesReply>("getDGSGoodsPurchases", queryParameters);
        }

        public async Task<PurchasesReply> GetPendingPurchases(string sellerId, int? firstIndex = null,
            int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> {{"seller", sellerId}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<PurchasesReply>("getDGSPendingPurchases", queryParameters);
        }

        public async Task<PurchaseReply> GetPurchase(ulong purchaseId)
        {
            var queryParameters = new Dictionary<string, string> { { "purchase", purchaseId.ToString() } };
            return await Get<PurchaseReply>("getDGSPurchase", queryParameters);
        }

        public async Task<PuchaseCountReply> GetPurchaseCount(ulong? sellerId = null, ulong? buyerId = null,
            bool? withPublicFeedbacksOnly = null, bool? completed = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("seller", sellerId, queryParameters);
            AddToParametersIfHasValue("buyer", buyerId, queryParameters);
            AddToParametersIfHasValue("withPublicFeedbacksOnly", withPublicFeedbacksOnly, queryParameters);
            AddToParametersIfHasValue("completed", completed, queryParameters);
            return await Get<PuchaseCountReply>("getDGSPurchaseCount", queryParameters);
        }

        public async Task<PurchasesReply> GetPurchases(ulong? sellerId = null, ulong? buyerId = null,
            int? firstIndex = null, int? lastIndex = null, bool? withPublicFeedbacksOnly = null, bool? completed = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("seller", sellerId, queryParameters);
            AddToParametersIfHasValue("buyer", buyerId, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("withPublicFeedbacksOnly", withPublicFeedbacksOnly, queryParameters);
            AddToParametersIfHasValue("completed", completed, queryParameters);
            return await Get<PurchasesReply>("getDGSPurchases", queryParameters);
        }

        public async Task<TagCountReply> GetTagCount(bool? inStockOnly = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("inStockOnly", inStockOnly, queryParameters);
            return await Get<TagCountReply>("getDGSTagCount", queryParameters);
        }

        public async Task<TagsReply> GetTags(bool? inStockOnly = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("inStockOnly", inStockOnly, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<TagsReply>("getDGSTags", queryParameters);
        }

        public async Task<TransactionCreatedReply> Listing(string name, string description, int quantity, Amount price,
            CreateTransactionParameters parameters, string tags = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"name", name},
                {"description", description},
                {"quantity", quantity.ToString()},
                {"priceNQT", price.Nqt.ToString()}
            };
            AddToParametersIfHasValue("tags", tags, queryParameters);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("dgsListing", queryParameters);
        }

        public async Task<TransactionCreatedReply> PriceChange(ulong goodsId, Amount price,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"goods", goodsId.ToString()},
                {"priceNQT", price.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("dgsPriceChange", queryParameters);
        }

        public async Task<TransactionCreatedReply> Purchase(ulong goodsId, Amount price, int quantity,
            DateTime deliveryDeadlineTimestamp, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"goods", goodsId.ToString()},
                {"priceNQT", price.Nqt.ToString()},
                {"quantity", quantity.ToString()}
            };
            AddToParametersIfHasValue("deliveryDeadlineTimestamp", deliveryDeadlineTimestamp, queryParameters);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("dgsPurchase", queryParameters);
        }

        public async Task<TransactionCreatedReply> QuantityChange(ulong goodsId, int deltaQuantity,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"goods", goodsId.ToString()},
                {"deltaQuantity", deltaQuantity.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("dgsQuantityChange", queryParameters);
        }

        public async Task<TransactionCreatedReply> Refund(ulong purchaseId, Amount refund,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"purchase", purchaseId.ToString()},
                {"refundNQT", refund.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("dgsRefund", queryParameters);
        }

        public async Task<GoodsReply> SearchGoods(string query = null, string tag = null, ulong? sellerId = null,
            int? firstIndex = null, int? lastIndex = null, bool? inStockOnly = null, bool? hideDelisted = null,
            bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("query", query, queryParameters);
            AddToParametersIfHasValue("tag", tag, queryParameters);
            AddToParametersIfHasValue("seller", sellerId, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("inStockOnly", inStockOnly, queryParameters);
            AddToParametersIfHasValue("hideDelisted", hideDelisted, queryParameters);
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<GoodsReply>("searchDGSGoods", queryParameters);
        }
    }
}
