using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.DigitalGoodsStoreOperations
{
    public interface IDigitalGoodsService
    {
        Task<TransactionCreated> Delisting(ulong goodsId, CreateTransactionParameters parameters);

        Task<TransactionCreated> Delivery(ulong purchaseId, CreateTransactionParameters parameters,
            Amount discount = null, string goodsToEncrypt = null, bool? goodsIsText = null, string goodsData = null,
            string goodsNonce = null);

        Task<TransactionCreated> Feedback(ulong purchaseId, string message,
            CreateTransactionParameters parameters);

        Task<Good> GetDgsGood(ulong goodsId, bool? includeCounts = null);

        Task<Goods> GetDgsGoods(ulong? sellerId = null, int? firstIndex = null, int? lastIndex = null,
            bool? inStockOnly = null, bool? hideDelisted = null, bool? includeCounts = null);

        Task<GoodsCount> GetDgsGoodsCount(ulong? sellerId = null, bool? inStockOnly = null);

        Task<GoodsPurchases> GetDgsGoodsPurchases(ulong goodsId, int? firstIndex = null, int? lastIndex = null,
            bool? withPublickKeedbacksOnly = null, bool? completed = null);

        Task<GoodsPurchases> GetDgsPendingPurchases(ulong sellerId, int? firstIndex = null,
            int? lastIndex = null);

        Task<GoodPurchase> GetDgsPurchase(ulong purchaseId);

        Task<GoodsPuchaseCount> GetDgsPurchaseCount(ulong? sellerId = null, ulong? buyerId = null,
            bool? withPublicFeedbacksOnly = null, bool? completed = null);

        Task<GoodsPurchases> GetDgsPurchases(ulong? sellerId = null, ulong? buyerId = null,
            int? firstIndex = null, int? lastIndex = null, bool? withPublicFeedbacksOnly = null, bool? completed = null);

        Task<GoodsTagCount> GetDgsTagCount(bool? inStockOnly = null);
        Task<GoodsTags> GetDgsTags(bool? inStockOnly = null, int? firstIndex = null, int? lastIndex = null);

        Task<TransactionCreated> Listing(string name, string description, int quantity, Amount price,
            CreateTransactionParameters parameters, string tags = null);

        Task<TransactionCreated> PriceChange(ulong goodsId, Amount price,
            CreateTransactionParameters parameters);

        Task<TransactionCreated> Purchase(ulong goodsId, Amount price, int quantity,
            DateTime deliveryDeadlineTimestamp, CreateTransactionParameters parameters);

        Task<TransactionCreated> QuantityChange(ulong goodsId, int deltaQuantity,
            CreateTransactionParameters parameters);

        Task<TransactionCreated> Refund(ulong purchaseId, Amount refund,
            CreateTransactionParameters parameters);

        Task<Goods> SearchDgsGoods(string query = null, string tag = null, ulong? sellerId = null,
            int? firstIndex = null, int? lastIndex = null, bool? inStockOnly = null, bool? hideDelisted = null,
            bool? includeCounts = null);
    }

    public class DigitalGoodsService : BaseService, IDigitalGoodsService
    {
        public DigitalGoodsService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public DigitalGoodsService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<TransactionCreated> Delisting(ulong goodsId, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"goods", goodsId.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("dgsDelisting", queryParameters);
        }

        public async Task<TransactionCreated> Delivery(ulong purchaseId, CreateTransactionParameters parameters,
            Amount discount = null, string goodsToEncrypt = null, bool? goodsIsText = null, string goodsData = null,
            string goodsNonce = null)
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
            AddToParametersIfHasValue("goodsNonce", goodsNonce, queryParameters);
            return await Post<TransactionCreated>("dgsDelivery", queryParameters);
        }

        public async Task<TransactionCreated> Feedback(ulong purchaseId, string message,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"purchase", purchaseId.ToString()},
                {"message", message}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("dgsFeedback", queryParameters);
        }

        public async Task<Good> GetDgsGood(ulong goodsId, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"goods", goodsId.ToString()}
            };
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<Good>("getDGSGood", queryParameters);
        }

        public async Task<Goods> GetDgsGoods(ulong? sellerId = null, int? firstIndex = null, int? lastIndex = null,
            bool? inStockOnly = null, bool? hideDelisted = null, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("seller", sellerId, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("inStockOnly", inStockOnly, queryParameters);
            AddToParametersIfHasValue("hideDelisted", hideDelisted, queryParameters);
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<Goods>("getDGSGoods", queryParameters);
        }

        public async Task<GoodsCount> GetDgsGoodsCount(ulong? sellerId = null, bool? inStockOnly = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("seller", sellerId, queryParameters);
            AddToParametersIfHasValue("inStockOnly", inStockOnly, queryParameters);
            return await Get<GoodsCount>("getDGSGoodsCount", queryParameters);
        }

        public async Task<GoodsPurchases> GetDgsGoodsPurchases(ulong goodsId, int? firstIndex = null, int? lastIndex = null,
            bool? withPublickKeedbacksOnly = null, bool? completed = null)
        {
            var queryParameters = new Dictionary<string, string> { { "goods", goodsId.ToString() } };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("withPublickKeedbacksOnly", withPublickKeedbacksOnly, queryParameters);
            AddToParametersIfHasValue("completed", completed, queryParameters);
            return await Get<GoodsPurchases>("getDGSGoodsPurchases", queryParameters);
        }

        public async Task<GoodsPurchases> GetDgsPendingPurchases(ulong sellerId, int? firstIndex = null,
            int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> { { "seller", sellerId.ToString() } };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<GoodsPurchases>("getDGSPendingPurchases", queryParameters);
        }

        public async Task<GoodPurchase> GetDgsPurchase(ulong purchaseId)
        {
            var queryParameters = new Dictionary<string, string> { { "purchase", purchaseId.ToString() } };
            return await Get<GoodPurchase>("getDGSPurchase", queryParameters);
        }

        public async Task<GoodsPuchaseCount> GetDgsPurchaseCount(ulong? sellerId = null, ulong? buyerId = null,
            bool? withPublicFeedbacksOnly = null, bool? completed = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("seller", sellerId, queryParameters);
            AddToParametersIfHasValue("buyer", buyerId, queryParameters);
            AddToParametersIfHasValue("withPublicFeedbacksOnly", withPublicFeedbacksOnly, queryParameters);
            AddToParametersIfHasValue("completed", completed, queryParameters);
            return await Get<GoodsPuchaseCount>("getDGSPurchaseCount", queryParameters);
        }

        public async Task<GoodsPurchases> GetDgsPurchases(ulong? sellerId = null, ulong? buyerId = null,
            int? firstIndex = null, int? lastIndex = null, bool? withPublicFeedbacksOnly = null, bool? completed = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("seller", sellerId, queryParameters);
            AddToParametersIfHasValue("buyer", buyerId, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("withPublicFeedbacksOnly", withPublicFeedbacksOnly, queryParameters);
            AddToParametersIfHasValue("completed", completed, queryParameters);
            return await Get<GoodsPurchases>("getDGSPurchases", queryParameters);
        }

        public async Task<GoodsTagCount> GetDgsTagCount(bool? inStockOnly = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("inStockOnly", inStockOnly, queryParameters);
            return await Get<GoodsTagCount>("getDGSTagCount", queryParameters);
        }

        public async Task<GoodsTags> GetDgsTags(bool? inStockOnly = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("inStockOnly", inStockOnly, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<GoodsTags>("getDGSTags", queryParameters);
        }

        public async Task<TransactionCreated> Listing(string name, string description, int quantity, Amount price,
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
            return await Post<TransactionCreated>("dgsListing", queryParameters);
        }

        public async Task<TransactionCreated> PriceChange(ulong goodsId, Amount price,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"goods", goodsId.ToString()},
                {"priceNQT", price.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("dgsPriceChange", queryParameters);
        }

        public async Task<TransactionCreated> Purchase(ulong goodsId, Amount price, int quantity,
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
            return await Post<TransactionCreated>("dgsPurchase", queryParameters);
        }

        public async Task<TransactionCreated> QuantityChange(ulong goodsId, int deltaQuantity,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"goods", goodsId.ToString()},
                {"deltaQuantity", deltaQuantity.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("dgsQuantityChange", queryParameters);
        }

        public async Task<TransactionCreated> Refund(ulong purchaseId, Amount refund,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"purchase", purchaseId.ToString()},
                {"refundNQT", refund.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("dgsRefund", queryParameters);
        }

        public async Task<Goods> SearchDgsGoods(string query = null, string tag = null, ulong? sellerId = null,
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
            return await Get<Goods>("searchDGSGoods", queryParameters);
        }
    }
}
