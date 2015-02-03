using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.DigitalGoodsStore
{
    public interface IDigitalGoodsStoreService
    {
        Task<TransactionCreated> Delisting(ulong goodsId, CreateTransactionParameters parameters);

        Task<TransactionCreated> Delivery(ulong purchaseId, CreateTransactionParameters parameters,
            Amount discount = null, string goodsToEncrypt = null, bool? goodsIsText = null, string goodsData = null,
            IEnumerable<byte> goodsNonce = null);

        Task<TransactionCreated> Feedback(ulong purchaseId, string message,
            CreateTransactionParameters parameters);

        Task<Good> GetGood(ulong goodsId, bool? includeCounts = null);

        Task<GoodsReply> GetGoods(ulong? sellerId = null, int? firstIndex = null, int? lastIndex = null,
            bool? inStockOnly = null, bool? hideDelisted = null, bool? includeCounts = null);

        Task<GoodsCountReply> GetGoodsCount(ulong? sellerId = null, bool? inStockOnly = null);

        Task<PuchaseCountReply> GetGoodsPurchaseCount(ulong goodsId, bool? withPublicFeedbacksOnly = null,
            bool? completed = null);

        Task<Purchases> GetGoodsPurchases(ulong goodsId, int? firstIndex = null, int? lastIndex = null,
            bool? withPublickKeedbacksOnly = null, bool? completed = null);

        Task<Purchases> GetPendingPurchases(string sellerId, int? firstIndex = null,
            int? lastIndex = null);

        Task<Purchase> GetPurchase(ulong purchaseId);

        Task<PuchaseCountReply> GetPurchaseCount(ulong? sellerId = null, ulong? buyerId = null,
            bool? withPublicFeedbacksOnly = null, bool? completed = null);

        Task<Purchases> GetPurchases(ulong? sellerId = null, ulong? buyerId = null,
            int? firstIndex = null, int? lastIndex = null, bool? withPublicFeedbacksOnly = null, bool? completed = null);

        Task<TagCountReply> GetTagCount(bool? inStockOnly = null);
        Task<TagsReply> GetTags(bool? inStockOnly = null, int? firstIndex = null, int? lastIndex = null);

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

        Task<GoodsReply> SearchGoods(string query = null, string tag = null, ulong? sellerId = null,
            int? firstIndex = null, int? lastIndex = null, bool? inStockOnly = null, bool? hideDelisted = null,
            bool? includeCounts = null);
    }
}