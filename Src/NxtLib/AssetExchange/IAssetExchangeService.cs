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

        Task<TransactionCreatedReply> DividendPayment(ulong assetId, int height, Amount amountPerQnt);

        Task<AccountAssetReply> GetAccountAsset(string accountId, ulong assetId, int? height = null);

        Task<AccountAssetCountReply> GetAccountAssetCount(string accountId, int? height = null);

        Task<AccountAssetsReply> GetAccountAssets(string accountId, int? height = null);

        Task<AskOrderIdsReply> GetAccountCurrentAskOrderIds(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null);

        Task<AskOrdersReply> GetAccountCurrentAskOrders(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null);

        Task<BidOrderIdsReply> GetAccountCurrentBidOrderIds(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null);

        Task<BidOrdersReply> GetAccountCurrentBidOrders(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null);

        Task<AssetsReply> GetAllAssets(int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null);

        Task<OpenOrdersReply> GetAllOpenAskOrders(int? firstIndex = null, int? lastIndex = null);

        Task<OpenOrdersReply> GetAllOpenBidOrders(int? firstIndex = null, int? lastIndex = null);

        Task<TradesReply> GetAllTrades(DateTime? timestamp = null, int? firstIndex = null, int? lastIndex = null,
            bool? includeAssetInfo = null);

        Task<OrderReply> GetAskOrder(ulong orderId);

        Task<AskOrderIdsReply> GetAskOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null);

        Task<AskOrdersReply> GetAskOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null);

        Task<AssetReply> GetAsset(ulong assetId, bool? includeCounts = null);

        Task<CountReply> GetAssetAccountCount(ulong assetId, int? height = null);

        Task<AssetAccountsReply> GetAssetAccounts(ulong assetId, int? height = null,
            int? firstIndex = null, int? lastIndex = null);

        Task<AssetIdsReply> GetAssetIds(int? firstIndex = null, int? lastIndex = null);

        Task<AssetsReply> GetAssets(IEnumerable<ulong> assetIds, bool? includeCounts = null);

        Task<AssetsByIssuerReply> GetAssetsByIssuer(IEnumerable<ulong> accountIds, int? firstIndex = null,
            int? lastIndex = null, bool? includeCounts = null);

        Task<AssetTransfersReply> GetAssetTransfers(AssetIdOrAccountId assetIdOrAccountId,
            int? firstIndex = null, int? lastIndex = null, DateTime? timestamp = null, bool? includeAssetInfo = null);

        Task<OrderReply> GetBidOrder(ulong orderId);

        Task<BidOrderIdsReply> GetBidOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null);

        Task<BidOrdersReply> GetBidOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null);

        Task<ExpectedAskOrdersReply> GetExpectedAskOrders(ulong? assetId = null, bool? sortByPrice = null);

        Task<ExpectedAssetTransfersReply> GetExpectedAssetTransfers(ulong? assetId = null, string account = null);

        Task<ExpectedBidOrdersReply> GetExpectedBidOrders(ulong? assetId = null, bool? sortByPrice = null);

        Task<ExpectedOrderCancellationReply> GetExpectedOrderCancellations();

        Task<LastTradesReply> GetLastTrades(IEnumerable<ulong> assetIds);

        Task<TradesReply> GetOrderTrades(OrderIdLocator orderIdLocator, int? firstIndex = null,
            int? lastIndex = null, bool? includeAssetInfo = null);

        Task<TradesReply> GetTrades(AssetIdOrAccountId assetIdOrAccountId, int? firstIndex = null, int? lastIndex = null,
            DateTime? timestamp = null, bool? includeAssetInfo = null);

        Task<TransactionCreatedReply> IssueAsset(string name, string description, long quantityQnt,
            CreateTransactionParameters parameters, int? decimals = null);

        Task<TransactionCreatedReply> PlaceAskOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> PlaceBidOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters);

        Task<AssetsReply> SearchAssets(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null);

        Task<TransactionCreatedReply> TransferAsset(string recipientId, ulong assetId, long quantityQnt,
            CreateTransactionParameters parameters);
    }
}