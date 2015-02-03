using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.AssetExchange
{
    public interface IAssetExchangeService
    {
        Task<TransactionCreated> CancelAskOrder(ulong orderId, CreateTransactionParameters parameters);
        Task<TransactionCreated> CancelBidOrder(ulong orderId, CreateTransactionParameters parameters);
        Task<TransactionCreated> DividendPayment(ulong assetId, int height, Amount amountPerQnt);
        Task<AccountAsset> GetAccountAsset(string accountId, ulong assetId, int? height = null);
        Task<AccountAssetCount> GetAccountAssetCount(string accountId, int? height = null);
        Task<AccountAssets> GetAccountAssets(string accountId, int? height = null);

        Task<AssetAskOrderIds> GetAccountCurrentAskOrderIds(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null);

        Task<AssetAskOrders> GetAccountCurrentAskOrders(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null);

        Task<AssetBidOrderIds> GetAccountCurrentBidOrderIds(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null);

        Task<AssetBidOrders> GetAccountCurrentBidOrders(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null);

        Task<Assets> GetAllAssets(int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null);

        Task<AssetOrders> GetAllOpenAskOrders(int? firstIndex = null, int? lastIndex = null);
        Task<AssetOrders> GetAllOpenBidOrders(int? firstIndex = null, int? lastIndex = null);

        Task<AssetTrades> GetAllTrades(DateTime? timestamp = null, int? firstIndex = null, int? lastIndex = null,
            bool? includeAssetInfo = null);

        Task<AssetOrder> GetAskOrder(ulong orderId);
        Task<AssetAskOrderIds> GetAskOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null);
        Task<AssetAskOrders> GetAskOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null);
        Task<Asset> GetAsset(ulong assetId, bool? includeCounts = null);
        Task<CountReply> GetAssetAccountCount(ulong assetId, int? height = null);

        Task<AssetAccounts> GetAssetAccounts(ulong assetId, int? height = null,
            int? firstIndex = null, int? lastIndex = null);

        Task<AssetIds> GetAssetIds(int? firstIndex = null, int? lastIndex = null);
        Task<Assets> GetAssets(IEnumerable<ulong> assetIds, bool? includeCounts = null);

        Task<AssetsByIssuer> GetAssetsByIssuer(IEnumerable<ulong> accountIds, int? firstIndex = null,
            int? lastIndex = null, bool? includeCounts = null);

        Task<AssetTransfers> GetAssetTransfers(AssetIdOrAccountId assetIdOrAccountId,
            int? firstIndex = null, int? lastIndex = null, bool? includeAssetInfo = null);

        Task<AssetOrder> GetBidOrder(ulong orderId);
        Task<AssetBidOrderIds> GetBidOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null);
        Task<AssetBidOrders> GetBidOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null);

        Task<AssetTrades> GetTrades(AssetIdOrAccountId assetIdOrAccountId, int? firstIndex = null, int? lastIndex = null,
            bool? includeAssetInfo = null);

        Task<TransactionCreated> IssueAsset(string name, string description, long quantityQnt,
            CreateTransactionParameters parameters, int? decimals = null);

        Task<TransactionCreated> PlaceAskOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters);

        Task<TransactionCreated> PlaceBidOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters);

        Task<Assets> SearchAssets(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null);

        Task<TransactionCreated> TransferAsset(string recipientId, ulong assetId, long quantityQnt,
            CreateTransactionParameters parameters);
    }
}