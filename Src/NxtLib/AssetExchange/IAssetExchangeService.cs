using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.AssetExchange
{
    public interface IAssetExchangeService
    {
        Task<TransactionCreatedReply> CancelAskOrder(ulong orderId, CreateTransactionParameters parameters);
        Task<TransactionCreatedReply> CancelBidOrder(ulong orderId, CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> DeleteAssetShares(ulong assetId, long quantityQnt,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> DividendPayment(ulong assetId, int height, Amount amountPerQnt,
            CreateTransactionParameters parameters);

        Task<AccountAssetReply> GetAccountAsset(Account account, ulong assetId, int? height = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AccountAssetCountReply> GetAccountAssetCount(Account account, int? height = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AccountAssetsReply> GetAccountAssets(Account account, int? height = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AskOrderIdsReply> GetAccountCurrentAskOrderIds(Account account, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null);

        Task<AskOrdersReply> GetAccountCurrentAskOrders(Account account, ulong? assetId = null, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<BidOrderIdsReply> GetAccountCurrentBidOrderIds(Account account, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<BidOrdersReply> GetAccountCurrentBidOrders(Account account, ulong? assetId = null, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AssetsReply> GetAllAssets(int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<OpenOrdersReply> GetAllOpenAskOrders(int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<OpenOrdersReply> GetAllOpenBidOrders(int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TradesReply> GetAllTrades(DateTime? timestamp = null, int? firstIndex = null, int? lastIndex = null,
            bool? includeAssetInfo = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<OrderReply> GetAskOrder(ulong orderId, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AskOrderIdsReply> GetAskOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AskOrdersReply> GetAskOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null,
            bool? showExpectedCancellations = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AssetReply> GetAsset(ulong assetId, bool? includeCounts = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<CountReply> GetAssetAccountCount(ulong assetId, int? height = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AssetAccountsReply> GetAssetAccounts(ulong assetId, int? height = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AssetIdsReply> GetAssetIds(int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AssetsReply> GetAssets(IEnumerable<ulong> assetIds, bool? includeCounts = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AssetsByIssuerReply> GetAssetsByIssuer(IEnumerable<Account> accounts, int? firstIndex = null,
            int? lastIndex = null, bool? includeCounts = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AssetTransfersReply> GetAssetTransfers(AssetIdOrAccountId assetIdOrAccountId,
            int? firstIndex = null, int? lastIndex = null, DateTime? timestamp = null, bool? includeAssetInfo = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<OrderReply> GetBidOrder(ulong orderId, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<BidOrderIdsReply> GetBidOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<BidOrdersReply> GetBidOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null,
            bool? showExpectedCancellations = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ExpectedAskOrdersReply> GetExpectedAskOrders(ulong? assetId = null, bool? sortByPrice = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ExpectedAssetTransfersReply> GetExpectedAssetTransfers(ulong? assetId = null, Account account = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ExpectedBidOrdersReply> GetExpectedBidOrders(ulong? assetId = null, bool? sortByPrice = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ExpectedOrderCancellationReply> GetExpectedOrderCancellations(ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<LastTradesReply> GetLastTrades(IEnumerable<ulong> assetIds, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TradesReply> GetOrderTrades(OrderIdLocator orderIdLocator, int? firstIndex = null,
            int? lastIndex = null, bool? includeAssetInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TradesReply> GetTrades(AssetIdOrAccountId assetIdOrAccountId, int? firstIndex = null, int? lastIndex = null,
            DateTime? timestamp = null, bool? includeAssetInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TransactionCreatedReply> IssueAsset(string name, string description, long quantityQnt,
            CreateTransactionParameters parameters, int? decimals = null);

        Task<TransactionCreatedReply> PlaceAskOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> PlaceBidOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters);

        Task<AssetsReply> SearchAssets(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TransactionCreatedReply> TransferAsset(Account recipient, ulong assetId, long quantityQnt,
            CreateTransactionParameters parameters);
    }
}