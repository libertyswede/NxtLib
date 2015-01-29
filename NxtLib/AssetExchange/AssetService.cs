using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NxtLib.Internal;

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
        Task<AssetAskOrders> GetAskOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null);
        Task<AssetAskOrderIds> GetAskOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null);
        Task<Asset> GetAsset(ulong assetId, bool? includeCounts = null);
        Task<AssetAccountCount> GetAssetAccountCount(ulong assetId, int? height = null);

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

    public class AssetExchangeService : BaseService, IAssetExchangeService
    {
        public AssetExchangeService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public AssetExchangeService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<TransactionCreated> CancelAskOrder(ulong orderId, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> { { "order", orderId.ToString() } };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("cancelAskOrder", queryParameters);
        }

        public async Task<TransactionCreated> CancelBidOrder(ulong orderId, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> {{"order", orderId.ToString()}};
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("cancelBidOrder", queryParameters);
        }

        public async Task<TransactionCreated> DividendPayment(ulong assetId, int height, Amount amountPerQnt)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()},
                {"height", height.ToString()},
                {"amountNQTPerQNT", amountPerQnt.Nqt.ToString()}
            };
            return await Post<TransactionCreated>("dividendPayment", queryParameters);
        }

        public async Task<AccountAssetCount> GetAccountAssetCount(string accountId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("height", height, queryParameters);
            return await Get<AccountAssetCount>("getAccountAssetCount", queryParameters);
        }

        public async Task<AccountAsset> GetAccountAsset(string accountId, ulong assetId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("height", height, queryParameters);
            AddToParametersIfHasValue("asset", assetId, queryParameters);
            return await Get<AccountAsset>("getAccountAssets", queryParameters);
        }

        public async Task<AccountAssets> GetAccountAssets(string accountId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("height", height, queryParameters);
            return await Get<AccountAssets>("getAccountAssets", queryParameters);
        }

        public async Task<AssetAskOrderIds> GetAccountCurrentAskOrderIds(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = BuildQueryParameters(accountId, assetId, firstIndex, lastIndex);
            return await Get<AssetAskOrderIds>("getAccountCurrentAskOrderIds", queryParameters);
        }

        public async Task<AssetAskOrders> GetAccountCurrentAskOrders(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = BuildQueryParameters(accountId, assetId, firstIndex, lastIndex);
            return await Get<AssetAskOrders>("getAccountCurrentAskOrders", queryParameters);
        }

        public async Task<AssetBidOrderIds> GetAccountCurrentBidOrderIds(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = BuildQueryParameters(accountId, assetId, firstIndex, lastIndex);
            return await Get<AssetBidOrderIds>("getAccountCurrentBidOrderIds", queryParameters);
        }

        public async Task<AssetBidOrders> GetAccountCurrentBidOrders(string accountId, ulong? assetId = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = BuildQueryParameters(accountId, assetId, firstIndex, lastIndex);
            return await Get<AssetBidOrders>("getAccountCurrentBidOrders", queryParameters);
        }

        public async Task<Assets> GetAllAssets(int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<Assets>("getAllAssets", queryParameters);
        }

        public async Task<AssetOrders> GetAllOpenAskOrders(int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AssetOrders>("getAllOpenAskOrders", queryParameters);
        }

        public async Task<AssetOrders> GetAllOpenBidOrders(int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AssetOrders>("getAllOpenBidOrders", queryParameters);
        }

        public async Task<AssetTrades> GetAllTrades(DateTime? timestamp = null, int? firstIndex = null, int? lastIndex = null,
            bool? includeAssetInfo = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue(timestamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeAssetInfo", includeAssetInfo, queryParameters);
            return await Get<AssetTrades>("getAllTrades", queryParameters);
        }

        public async Task<AssetOrder> GetAskOrder(ulong orderId)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"order", orderId.ToString()}
            };
            return await Get<AssetOrder>("getAskOrder", queryParameters);
        }

        public async Task<AssetAskOrders> GetAskOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AssetAskOrders>("getAskOrders", queryParameters);
        }

        public async Task<AssetAskOrderIds> GetAskOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AssetAskOrderIds>("getAskOrderIds", queryParameters);
        }

        public async Task<Asset> GetAsset(ulong assetId, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<Asset>("getAsset", queryParameters);
        }

        public async Task<AssetAccountCount> GetAssetAccountCount(ulong assetId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("height", height, queryParameters);
            return await Get<AssetAccountCount>("getAssetAccountCount", queryParameters);
        }

        public async Task<AssetAccounts> GetAssetAccounts(ulong assetId, int? height = null,
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("height", height, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AssetAccounts>("getAssetAccounts", queryParameters);
        }

        public async Task<AssetIds> GetAssetIds(int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AssetIds>("getAssetIds", queryParameters);
        }

        public async Task<Assets> GetAssets(IEnumerable<ulong> assetIds, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"assets", assetIds.Select(id => id.ToString()).ToList()}
            };
            if (includeCounts.HasValue)
            {
                queryParameters.Add("includeCounts", new List<string> { includeCounts.Value.ToString() });
            }
            return await Get<Assets>("getAssets", queryParameters);
        }

        public async Task<AssetsByIssuer> GetAssetsByIssuer(IEnumerable<ulong> accountIds, int? firstIndex = null,
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
            return await Get<AssetsByIssuer>("getAssetsByIssuer", queryParameters);
        }

        public async Task<AssetTransfers> GetAssetTransfers(AssetIdOrAccountId assetIdOrAccountId,
            int? firstIndex = null, int? lastIndex = null, bool? includeAssetInfo = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("asset", assetIdOrAccountId.AssetId, queryParameters);
            AddToParametersIfHasValue("account", assetIdOrAccountId.AccountId, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeAssetInfo", includeAssetInfo, queryParameters);
            return await Get<AssetTransfers>("getAssetTransfers", queryParameters);
        }

        public async Task<AssetOrder> GetBidOrder(ulong orderId)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"order", orderId.ToString()}
            };
            return await Get<AssetOrder>("getBidOrder", queryParameters);
        }

        public async Task<AssetBidOrderIds> GetBidOrderIds(ulong assetId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AssetBidOrderIds>("getBidOrderIds", queryParameters);
        }

        public async Task<AssetBidOrders> GetBidOrders(ulong assetId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()}
            };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AssetBidOrders>("getBidOrders", queryParameters);
        }

        public async Task<AssetTrades> GetTrades(AssetIdOrAccountId assetIdOrAccountId, int? firstIndex = null, int? lastIndex = null,
            bool? includeAssetInfo = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("asset", assetIdOrAccountId.AssetId, queryParameters);
            AddToParametersIfHasValue("account", assetIdOrAccountId.AccountId, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeAssetInfo", includeAssetInfo, queryParameters);
            return await Get<AssetTrades>("getTrades", queryParameters);
        }

        public async Task<TransactionCreated> IssueAsset(string name, string description, long quantityQnt,
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
            return await Post<TransactionCreated>("issueAsset", queryParameters);
        }

        public async Task<TransactionCreated> PlaceAskOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()},
                {"quantityQNT", quantityQnt.ToString()},
                {"priceNQT", price.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("placeAskOrder", queryParameters);
        }

        public async Task<TransactionCreated> PlaceBidOrder(ulong assetId, long quantityQnt, Amount price,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()},
                {"quantityQNT", quantityQnt.ToString()},
                {"priceNQT", price.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("placeBidOrder", queryParameters);
        }

        public async Task<Assets> SearchAssets(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string> { { "query", query } };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<Assets>("searchAssets", queryParameters);
        }

        public async Task<TransactionCreated> TransferAsset(string recipientId, ulong assetId, long quantityQnt,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"asset", assetId.ToString()},
                {"quantityQNT", quantityQnt.ToString()},
                {"recipient", recipientId}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("transferAsset", queryParameters);
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
