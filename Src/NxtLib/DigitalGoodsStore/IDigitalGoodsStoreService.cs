using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.DigitalGoodsStore
{
    public interface IDigitalGoodsStoreService
    {
        Task<TransactionCreatedReply> Delisting(ulong goodsId, CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> Delivery(ulong purchaseId, CreateTransactionParameters parameters,
            Amount discount = null, string goodsToEncrypt = null, bool? goodsIsText = null, string goodsData = null,
            IEnumerable<byte> goodsNonce = null);

        Task<TransactionCreatedReply> Feedback(ulong purchaseId, CreateTransactionParameters parameters);

        Task<GoodReply> GetGood(ulong goodsId, bool? includeCounts = null);

        Task<GoodsReply> GetGoods(ulong? sellerId = null, int? firstIndex = null, int? lastIndex = null,
            bool? inStockOnly = null, bool? hideDelisted = null, bool? includeCounts = null);

        Task<GoodsCountReply> GetGoodsCount(ulong? sellerId = null, bool? inStockOnly = null);

        Task<PuchaseCountReply> GetGoodsPurchaseCount(ulong goodsId, bool? withPublicFeedbacksOnly = null,
            bool? completed = null);

        Task<PurchasesReply> GetGoodsPurchases(ulong goodsId, int? firstIndex = null, int? lastIndex = null,
            bool? withPublickKeedbacksOnly = null, bool? completed = null);

        Task<PurchasesReply> GetPendingPurchases(string sellerId, int? firstIndex = null,
            int? lastIndex = null);

        Task<PurchaseReply> GetPurchase(ulong purchaseId);

        Task<PuchaseCountReply> GetPurchaseCount(ulong? sellerId = null, ulong? buyerId = null,
            bool? withPublicFeedbacksOnly = null, bool? completed = null);

        Task<PurchasesReply> GetPurchases(ulong? sellerId = null, ulong? buyerId = null,
            int? firstIndex = null, int? lastIndex = null, bool? withPublicFeedbacksOnly = null, bool? completed = null);

        Task<TagCountReply> GetTagCount(bool? inStockOnly = null);
        Task<TagsReply> GetTags(bool? inStockOnly = null, int? firstIndex = null, int? lastIndex = null);

        Task<TransactionCreatedReply> Listing(string name, string description, int quantity, Amount price,
            CreateTransactionParameters parameters, string tags = null);

        Task<TransactionCreatedReply> PriceChange(ulong goodsId, Amount price,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> Purchase(ulong goodsId, Amount price, int quantity,
            DateTime deliveryDeadlineTimestamp, CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> QuantityChange(ulong goodsId, int deltaQuantity,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> Refund(ulong purchaseId, Amount refund,
            CreateTransactionParameters parameters);

        Task<GoodsReply> SearchGoods(string query = null, string tag = null, ulong? sellerId = null,
            int? firstIndex = null, int? lastIndex = null, bool? inStockOnly = null, bool? hideDelisted = null,
            bool? includeCounts = null);
    }
}