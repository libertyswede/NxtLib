using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;
using NxtLib.MonetarySystem;

namespace NxtLib.AssetExchange
{
    public class AssetExchangeService : BaseService, IAssetExchangeService
    {
        public AssetExchangeService(string baseAddress = Constants.DefaultNxtUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public AssetExchangeService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<TransactionCreatedReply> CancelAskOrder(ulong orderId, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> { { "order", orderId.ToString() } };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("cancelAskOrder", queryParameters);
        }

        public async Task<TransactionCreatedReply> CancelBidOrder(ulong orderId, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> {{"order", orderId.ToString()}};
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("cancelBidOrder", queryParameters);
        }

        public async Task<TransactionCreatedReply> DividendPayment(ulong assetId, int height, Amount amountPerQnt)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()},
                {"height", height.ToString()},
                {"amountNQTPerQNT", amountPerQnt.Nqt.ToString()}
            };
            return await Post<TransactionCreatedReply>("dividendPayment", queryParameters);
        }

        public async Task<AccountAssetCountReply> GetAccountAssetCount(string accountId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("height", height, queryParameters);
            return await Get<AccountAssetCountReply>("getAccountAssetCount", queryParameters);
        }

        public async Task<AccountAssetReply> GetAccountAsset(string accountId, ulong assetId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("height", height, queryParameters);
            AddToParametersIfHasValue("asset", assetId, queryParameters);
            return await Get<AccountAssetReply>("getAccountAssets", queryParameters);
        }

        public async Task<AccountAssetsReply> GetAccountAssets(string accountId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("height", height, queryParameters);
            return await Get<AccountAssetsReply>("getAccountAssets", queryParameters);
        }

        public async Task<AskOrderIdsReply> GetAccountCurrentAskOrderIds(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = BuildQueryParameters(accountId, assetId, firstIndex, lastIndex);
            return await Get<AskOrderIdsReply>("getAccountCurrentAskOrderIds", queryParameters);
        }

        public async Task<AskOrdersReply> GetAccountCurrentAskOrders(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = BuildQueryParameters(accountId, assetId, firstIndex, lastIndex);
            return await Get<AskOrdersReply>("getAccountCurrentAskOrders", queryParameters);
        }

        public async Task<BidOrderIdsReply> GetAccountCurrentBidOrderIds(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = BuildQueryParameters(accountId, assetId, firstIndex, lastIndex);
            return await Get<BidOrderIdsReply>("getAccountCurrentBidOrderIds", queryParameters);
        }

        public async Task<BidOrdersReply> GetAccountCurrentBidOrders(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = BuildQueryParameters(accountId, assetId, firstIndex, lastIndex);
            return await Get<BidOrdersReply>("getAccountCurrentBidOrders", queryParameters);
        }

        public async Task<AssetsReply> GetAllAssets(int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<AssetsReply>("getAllAssets", queryParameters);
        }

        public async Task<OpenOrdersReply> GetAllOpenAskOrders(int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<OpenOrdersReply>("getAllOpenAskOrders", queryParameters);
        }

        public async Task<OpenOrdersReply> GetAllOpenBidOrders(int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<OpenOrdersReply>("getAllOpenBidOrders", queryParameters);
        }

        public async Task<TradesReply> GetAllTrades(DateTime? timestamp = null, int? firstIndex = null, int? lastIndex = null,
            bool? includeAssetInfo = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue(timestamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeAssetInfo", includeAssetInfo, queryParameters);
            return await Get<TradesReply>("getAllTrades", queryParameters);
        }

        public async Task<OrderReply> GetAskOrder(ulong orderId)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"order", orderId.ToString()}
            };
            return await Get<OrderReply>("getAskOrder", queryParameters);
        }

        public async Task<AskOrderIdsReply> GetAskOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AskOrderIdsReply>("getAskOrderIds", queryParameters);
        }

        public async Task<AskOrdersReply> GetAskOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AskOrdersReply>("getAskOrders", queryParameters);
        }

        public async Task<AssetReply> GetAsset(ulong assetId, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<AssetReply>("getAsset", queryParameters);
        }

        public async Task<CountReply> GetAssetAccountCount(ulong assetId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("height", height, queryParameters);
            return await Get<CountReply>("getAssetAccountCount", queryParameters);
        }

        public async Task<AssetAccountsReply> GetAssetAccounts(ulong assetId, int? height = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("height", height, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AssetAccountsReply>("getAssetAccounts", queryParameters);
        }

        public async Task<AssetIdsReply> GetAssetIds(int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AssetIdsReply>("getAssetIds", queryParameters);
        }

        public async Task<AssetsReply> GetAssets(IEnumerable<ulong> assetIds, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"assets", assetIds.Select(id => id.ToString()).ToList()}
            };
            if (includeCounts.HasValue)
            {
                queryParameters.Add("includeCounts", new List<string> { includeCounts.Value.ToString() });
            }
            return await Get<AssetsReply>("getAssets", queryParameters);
        }

        public async Task<AssetsByIssuerReply> GetAssetsByIssuer(IEnumerable<ulong> accountIds, int? firstIndex = null,
            int? lastIndex = null, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"account", accountIds.Select(id => id.ToString()).ToList()}
            };
            if (firstIndex.HasValue)
            {
                queryParameters.Add("firstIndex", new List<string> { firstIndex.Value.ToString() });
            }
            if (lastIndex.HasValue)
            {
                queryParameters.Add("lastIndex", new List<string> { lastIndex.Value.ToString() });
            }
            if (includeCounts.HasValue)
            {
                queryParameters.Add("includeCounts", new List<string> { includeCounts.Value.ToString() });
            }
            return await Get<AssetsByIssuerReply>("getAssetsByIssuer", queryParameters);
        }

        public async Task<AssetTransfersReply> GetAssetTransfers(AssetIdOrAccountId assetIdOrAccountId,
            int? firstIndex = null, int? lastIndex = null, bool? includeAssetInfo = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("asset", assetIdOrAccountId.AssetId, queryParameters);
            AddToParametersIfHasValue("account", assetIdOrAccountId.AccountId, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeAssetInfo", includeAssetInfo, queryParameters);
            return await Get<AssetTransfersReply>("getAssetTransfers", queryParameters);
        }

        public async Task<OrderReply> GetBidOrder(ulong orderId)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"order", orderId.ToString()}
            };
            return await Get<OrderReply>("getBidOrder", queryParameters);
        }

        public async Task<BidOrderIdsReply> GetBidOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<BidOrderIdsReply>("getBidOrderIds", queryParameters);
        }

        public async Task<BidOrdersReply> GetBidOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<BidOrdersReply>("getBidOrders", queryParameters);
        }

        public async Task<ExpectedAskOrdersReply> GetExpectedAskOrders(ulong? assetId = null, bool? sortByPrice = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("assetId", assetId, queryParameters);
            AddToParametersIfHasValue("sortByPrice", sortByPrice, queryParameters);
            return await Get<ExpectedAskOrdersReply>("getExpectedAskOrders", queryParameters);
        }

        public async Task<ExchangesReply> GetLastExchanges(IList<ulong> currencyIds)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"currencies", currencyIds.Select(id => id.ToString()).ToList()}
            };
            return await Get<ExchangesReply>("getLastExchanges", queryParameters);
        }

        public async Task<TradesReply> GetTrades(AssetIdOrAccountId assetIdOrAccountId, int? firstIndex = null, int? lastIndex = null,
            DateTime? timestamp = null, bool? includeAssetInfo = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("asset", assetIdOrAccountId.AssetId, queryParameters);
            AddToParametersIfHasValue("account", assetIdOrAccountId.AccountId, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeAssetInfo", includeAssetInfo, queryParameters);
            AddToParametersIfHasValue("timestamp", timestamp, queryParameters);
            return await Get<TradesReply>("getTrades", queryParameters);
        }

        public async Task<TransactionCreatedReply> IssueAsset(string name, string description, long quantityQnt,
            CreateTransactionParameters parameters, int? decimals = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"name", name},
                {"description", description},
                {"quantityQNT", quantityQnt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            AddToParametersIfHasValue("decimals", decimals, queryParameters);
            return await Post<TransactionCreatedReply>("issueAsset", queryParameters);
        }

        public async Task<TransactionCreatedReply> PlaceAskOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()},
                {"quantityQNT", quantityQnt.ToString()},
                {"priceNQT", price.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("placeAskOrder", queryParameters);
        }

        public async Task<TransactionCreatedReply> PlaceBidOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()},
                {"quantityQNT", quantityQnt.ToString()},
                {"priceNQT", price.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("placeBidOrder", queryParameters);
        }

        public async Task<AssetsReply> SearchAssets(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string> { { "query", query } };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<AssetsReply>("searchAssets", queryParameters);
        }

        public async Task<TransactionCreatedReply> TransferAsset(string recipientId, ulong assetId, long quantityQnt,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()},
                {"quantityQNT", quantityQnt.ToString()},
                {"recipient", recipientId}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("transferAsset", queryParameters);
        }

        private static Dictionary<string, string> BuildQueryParameters(string accountId, ulong? assetId, int? firstIndex,
            int? lastIndex)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("asset", assetId, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return queryParameters;
        }
    }
}
