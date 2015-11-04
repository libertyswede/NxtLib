using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.AssetExchange
{
    public class AssetExchangeService : BaseService, IAssetExchangeService
    {
        public AssetExchangeService(string baseAddress = Constants.DefaultNxtUrl)
            : base(baseAddress)
        {
        }

        public async Task<TransactionCreatedReply> CancelAskOrder(ulong orderId, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Order, orderId.ToString()}};
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("cancelAskOrder", queryParameters);
        }

        public async Task<TransactionCreatedReply> CancelBidOrder(ulong orderId, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Order, orderId.ToString()}};
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("cancelBidOrder", queryParameters);
        }

        public async Task<TransactionCreatedReply> DeleteAssetShares(ulong assetId, long quantityQnt,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Asset, assetId.ToString()},
                {Parameters.QuantityQnt, quantityQnt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("deleteAssetShares", queryParameters);
        }

        public async Task<TransactionCreatedReply> DividendPayment(ulong assetId, int height, Amount amountPerQnt,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Asset, assetId.ToString()},
                {Parameters.Height, height.ToString()},
                {Parameters.AmountNqtPerQnt, amountPerQnt.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("dividendPayment", queryParameters);
        }

        public async Task<AccountAssetCountReply> GetAccountAssetCount(Account account, int? height = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Height, height);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AccountAssetCountReply>("getAccountAssetCount", queryParameters);
        }

        public async Task<AccountAssetReply> GetAccountAsset(Account account, ulong assetId, bool? includeAssetInfo = null, 
            int? height = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.IncludeAssetInfo, includeAssetInfo);
            queryParameters.AddIfHasValue(Parameters.Height, height);
            queryParameters.AddIfHasValue(Parameters.Asset, assetId);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AccountAssetReply>("getAccountAssets", queryParameters);
        }

        public async Task<AccountAssetsReply> GetAccountAssets(Account account, bool? includeAssetInfo = null, 
            int? height = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.IncludeAssetInfo, includeAssetInfo);
            queryParameters.AddIfHasValue(Parameters.Height, height);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AccountAssetsReply>("getAccountAssets", queryParameters);
        }

        public async Task<AskOrderIdsReply> GetAccountCurrentAskOrderIds(Account account, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = BuildQueryParameters(account, assetId, firstIndex, lastIndex);
            return await Get<AskOrderIdsReply>("getAccountCurrentAskOrderIds", queryParameters);
        }

        public async Task<AskOrdersReply> GetAccountCurrentAskOrders(Account account, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = BuildQueryParameters(account, assetId, firstIndex, lastIndex, requireBlock, requireLastBlock);
            return await Get<AskOrdersReply>("getAccountCurrentAskOrders", queryParameters);
        }

        public async Task<BidOrderIdsReply> GetAccountCurrentBidOrderIds(Account account, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = BuildQueryParameters(account, assetId, firstIndex, lastIndex, requireBlock, requireLastBlock);
            return await Get<BidOrderIdsReply>("getAccountCurrentBidOrderIds", queryParameters);
        }

        public async Task<BidOrdersReply> GetAccountCurrentBidOrders(Account account, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = BuildQueryParameters(account, assetId, firstIndex, lastIndex, requireBlock, requireLastBlock);
            return await Get<BidOrdersReply>("getAccountCurrentBidOrders", queryParameters);
        }

        public async Task<AssetsReply> GetAllAssets(int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeCounts, includeCounts);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AssetsReply>("getAllAssets", queryParameters);
        }

        public async Task<OpenOrdersReply> GetAllOpenAskOrders(int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<OpenOrdersReply>("getAllOpenAskOrders", queryParameters);
        }

        public async Task<OpenOrdersReply> GetAllOpenBidOrders(int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<OpenOrdersReply>("getAllOpenBidOrders", queryParameters);
        }

        public async Task<TradesReply> GetAllTrades(DateTime? timeStamp = null, int? firstIndex = null,
            int? lastIndex = null, bool? includeAssetInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Timestamp, timeStamp);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeAssetInfo, includeAssetInfo);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TradesReply>("getAllTrades", queryParameters);
        }

        public async Task<OrderReply> GetAskOrder(ulong orderId, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Order, orderId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<OrderReply>("getAskOrder", queryParameters);
        }

        public async Task<AskOrderIdsReply> GetAskOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Asset, assetId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AskOrderIdsReply>("getAskOrderIds", queryParameters);
        }

        public async Task<AskOrdersReply> GetAskOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null,
            bool? showExpectedCancellations = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Asset, assetId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.ShowExpectedCancellations, showExpectedCancellations);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AskOrdersReply>("getAskOrders", queryParameters);
        }

        public async Task<AssetReply> GetAsset(ulong assetId, bool? includeCounts = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Asset, assetId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.IncludeCounts, includeCounts);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AssetReply>("getAsset", queryParameters);
        }

        public async Task<CountReply> GetAssetAccountCount(ulong assetId, int? height = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Asset, assetId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Height, height);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<CountReply>("getAssetAccountCount", queryParameters);
        }

        public async Task<AssetAccountsReply> GetAssetAccounts(ulong assetId, int? height = null, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Asset, assetId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Height, height);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AssetAccountsReply>("getAssetAccounts", queryParameters);
        }

        public async Task<AssetIdsReply> GetAssetIds(int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AssetIdsReply>("getAssetIds", queryParameters);
        }

        public async Task<AssetsReply> GetAssets(IEnumerable<ulong> assetIds, bool? includeCounts = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {Parameters.Assets, assetIds.Select(id => id.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(Parameters.IncludeCounts, includeCounts);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AssetsReply>("getAssets", queryParameters);
        }

        public async Task<AssetsByIssuerReply> GetAssetsByIssuer(IEnumerable<Account> accounts, int? firstIndex = null,
            int? lastIndex = null, bool? includeCounts = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {Parameters.Account, accounts.Select(account => account.AccountId.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeCounts, includeCounts);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AssetsByIssuerReply>("getAssetsByIssuer", queryParameters);
        }

        public async Task<AssetTransfersReply> GetAssetTransfers(AssetIdOrAccountId assetIdOrAccountId,
            int? firstIndex = null, int? lastIndex = null, DateTime? timestamp = null, bool? includeAssetInfo = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Asset, assetIdOrAccountId.AssetId);
            queryParameters.AddIfHasValue(Parameters.Account, assetIdOrAccountId.AccountId);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.Timestamp, timestamp);
            queryParameters.AddIfHasValue(Parameters.IncludeAssetInfo, includeAssetInfo);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AssetTransfersReply>("getAssetTransfers", queryParameters);
        }

        public async Task<OrderReply> GetBidOrder(ulong orderId, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Order, orderId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<OrderReply>("getBidOrder", queryParameters);
        }

        public async Task<BidOrderIdsReply> GetBidOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Asset, assetId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<BidOrderIdsReply>("getBidOrderIds", queryParameters);
        }

        public async Task<BidOrdersReply> GetBidOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null,
            bool? showExpectedCancellations = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Asset, assetId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.ShowExpectedCancellations, showExpectedCancellations);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<BidOrdersReply>("getBidOrders", queryParameters);
        }

        public async Task<ExpectedAskOrdersReply> GetExpectedAskOrders(ulong? assetId = null, bool? sortByPrice = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Asset, assetId);
            queryParameters.AddIfHasValue(Parameters.SortByPrice, sortByPrice);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ExpectedAskOrdersReply>("getExpectedAskOrders", queryParameters);
        }

        public async Task<ExpectedBidOrdersReply> GetExpectedBidOrders(ulong? assetId = null, bool? sortByPrice = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Asset, assetId);
            queryParameters.AddIfHasValue(Parameters.SortByPrice, sortByPrice);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ExpectedBidOrdersReply>("getExpectedBidOrders", queryParameters);
        }

        public async Task<ExpectedAssetTransfersReply> GetExpectedAssetTransfers(ulong? assetId = null,
            Account account = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Asset, assetId);
            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ExpectedAssetTransfersReply>("getExpectedAssetTransfers", queryParameters);
        }

        public async Task<ExpectedOrderCancellationReply> GetExpectedOrderCancellations(ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ExpectedOrderCancellationReply>("getExpectedOrderCancellations");
        }

        public async Task<LastTradesReply> GetLastTrades(IEnumerable<ulong> assetIds, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {Parameters.Assets, assetIds.Select(id => id.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<LastTradesReply>("getLastTrades", queryParameters);
        }

        public async Task<TradesReply> GetOrderTrades(OrderIdLocator orderIdLocator, int? firstIndex = null,
            int? lastIndex = null, bool? includeAssetInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = orderIdLocator.QueryParameters;
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeAssetInfo, includeAssetInfo);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TradesReply>("getOrderTrades", queryParameters);
        }

        public async Task<TradesReply> GetTrades(AssetIdOrAccountId assetIdOrAccountId, int? firstIndex = null,
            int? lastIndex = null, DateTime? timestamp = null, bool? includeAssetInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Asset, assetIdOrAccountId.AssetId);
            queryParameters.AddIfHasValue(Parameters.Account, assetIdOrAccountId.AccountId);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeAssetInfo, includeAssetInfo);
            queryParameters.AddIfHasValue(Parameters.Timestamp, timestamp);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TradesReply>("getTrades", queryParameters);
        }

        public async Task<TransactionCreatedReply> IssueAsset(string name, string description, long quantityQnt,
            CreateTransactionParameters parameters, int? decimals = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Name, name},
                {Parameters.Description, description},
                {Parameters.QuantityQnt, quantityQnt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.AddIfHasValue(Parameters.Decimals, decimals);
            return await Post<TransactionCreatedReply>("issueAsset", queryParameters);
        }

        public async Task<TransactionCreatedReply> PlaceAskOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Asset, assetId.ToString()},
                {Parameters.QuantityQnt, quantityQnt.ToString()},
                {Parameters.PriceNqt, price.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("placeAskOrder", queryParameters);
        }

        public async Task<TransactionCreatedReply> PlaceBidOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Asset, assetId.ToString()},
                {Parameters.QuantityQnt, quantityQnt.ToString()},
                {Parameters.PriceNqt, price.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("placeBidOrder", queryParameters);
        }

        public async Task<AssetsReply> SearchAssets(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Query, query}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeCounts, includeCounts);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AssetsReply>("searchAssets", queryParameters);
        }

        public async Task<TransactionCreatedReply> TransferAsset(Account recipient, ulong assetId, long quantityQnt,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Asset, assetId.ToString()},
                {Parameters.QuantityQnt, quantityQnt.ToString()},
                {Parameters.Recipient, recipient.AccountId.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("transferAsset", queryParameters);
        }

        private static Dictionary<string, string> BuildQueryParameters(Account account, ulong? assetId, int? firstIndex,
            int? lastIndex, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Asset, assetId);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return queryParameters;
        }
    }
}