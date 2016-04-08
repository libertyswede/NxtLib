using System;
using System.Threading.Tasks;

namespace NxtLib.DigitalGoodsStore
{
    public interface IDigitalGoodsStoreService
    {
        Task<TransactionCreatedReply> Delisting(ulong goodsId, CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> Delivery(ulong purchaseId, CreateTransactionParameters parameters,
            Amount discount = null, string goodsToEncrypt = null, bool? goodsIsText = null, string goodsData = null,
            BinaryHexString goodsNonce = null);

        Task<TransactionCreatedReply> Feedback(ulong purchaseId, CreateTransactionParameters parameters);

        Task<PurchasesReply> GetExpiredPurchases(Account seller, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GoodReply> GetGood(ulong goodsId, bool? includeCounts = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GoodsReply> GetGoods(Account seller = null, int? firstIndex = null, int? lastIndex = null,
            bool? inStockOnly = null, bool? hideDelisted = null, bool? includeCounts = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GoodsCountReply> GetGoodsCount(Account seller = null, bool? inStockOnly = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<PuchaseCountReply> GetGoodsPurchaseCount(ulong goodsId, bool? withPublicFeedbacksOnly = null,
            bool? completed = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<PurchasesReply> GetGoodsPurchases(ulong goodsId, Account buyer = null, int? firstIndex = null,
            int? lastIndex = null, bool? withPublicFeedbacksOnly = null, bool? completed = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<PurchasesReply> GetPendingPurchases(Account seller, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<PurchaseReply> GetPurchase(ulong purchaseId, string secretPhrase = null,
            BinaryHexString sharedKey = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<PuchaseCountReply> GetPurchaseCount(Account seller = null, Account buyer = null,
            bool? withPublicFeedbacksOnly = null, bool? completed = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<PurchasesReply> GetPurchases(Account seller = null, Account buyer = null, int? firstIndex = null,
            int? lastIndex = null, bool? withPublicFeedbacksOnly = null, bool? completed = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TagCountReply> GetTagCount(bool? inStockOnly = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TagsReply> GetTags(bool? inStockOnly = null, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TagsReply> GetTagsLike(string tagPrefix, bool? inStockOnly = null, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

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

        Task<GoodsReply> SearchGoods(string query = null, string tag = null, Account seller = null,
            int? firstIndex = null, int? lastIndex = null, bool? inStockOnly = null, bool? hideDelisted = null,
            bool? includeCounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null);
    }
}