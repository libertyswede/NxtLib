using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.MonetarySystem
{
    public interface IMonetarySystemService
    {
        Task<CanDeleteCurrencyReply> CanDeleteCurrency(string accountId, ulong currencyId);
        Task<TransactionCreated> CurrencyBuy(ulong currencyId, Amount rate, long units, CreateTransactionParameters parameters);
        Task<TransactionCreated> CurrencyMint(ulong currencyId, long nonce, long units, long counter,
            CreateTransactionParameters parameters);
        Task<TransactionCreated> CurrencyReserveClaim(ulong currencyId, long units,
            CreateTransactionParameters parameters);
        Task<TransactionCreated> CurrencyReserveIncrease(ulong currencyId, Amount amountPerUnitNqt,
            CreateTransactionParameters parameters);
        Task<TransactionCreated> CurrencySell(ulong currencyId, Amount rate, long units, CreateTransactionParameters parameters);
        Task<TransactionCreated> DeleteCurrency(ulong currencyId, CreateTransactionParameters parameters);
        Task<GetAccountCurrenciesReply> GetAccountCurrencies(string accountId, ulong? currencyId = null,
            int? height = null);
        Task<GetAccountCurrencyCountReply> GetAccountCurrencyCount(string accountId, int? height = null);
        Task<GetAccountExchangeRequestsReply> GetAccountExchangeRequests(string accountId, ulong currencyId, int? firstindex = null,
            int? lastindex = null);
        Task<CurrenciesReply> GetAllCurrencies(int? firstindex = null, int? lastindex = null, bool? includeCounts = null);
        Task<ExchangesReply> GetAllExchanges(DateTime? timestamp = null, int? firstindex = null,
            int? lastindex = null, bool? includeCurrencyInfo = null);

        Task<GetOffersReply> GetBuyOffers(CurrencyOrAccountLocator locator,
            bool? availableOnly = null, int? firstindex = null, int? lastindex = null);

        Task<CurrenciesReply> GetCurrencies(List<ulong> currencyIds, bool? includeCounts = null);

        Task<CurrenciesReply> GetCurrenciesByIssuer(List<string> accountIds, int? firstIndex = null,
            int? lastIndex = null, bool? includeCounts = null);

        Task<CurrencyReply> GetCurrency(CurrencyLocator locator, bool? includeCounts = null);

        Task<CountReply> GetCurrencyAccountCount(ulong currencyId, int? height = null);

        Task<CurrencyAccountsReply> GetCurrencyAccounts(ulong currencyId, int? height = null, int? firstIndex = null,
            int? lastIndex = null);

        Task<CurrencyFoundersReply> GetCurrencyFounders(ulong currencyId);

        Task<CurrencyIdsReply> GetCurrencyIds(int? firstIndex = null, int? lastIndex = null);

        Task<CurrencyTransfersReply> GetCurrencyTransfers(CurrencyOrAccountLocator locator, int? firstIndex = null,
            int? lastIndex = null, bool? includeCurrencyInfo = null);

        Task<ExchangesReply> GetExchanges(CurrencyOrAccountLocator locator, int? firstIndex = null,
            int? lastIndex = null, bool? includeCurrencyInfo = null);

        Task<ExchangesReply> GetExchangesByExchangeRequest(ulong transactionId, bool? includeCurrencyInfo = null);

        Task<ExchangesReply> GetExchangesByOffer(ulong offerId, int? firstindex = null,
            int? lastindex = null, bool? includeCurrencyInfo = null);

        Task<GetMintingTargetReply> GetMintingTarget(ulong currencyId, string accountId, long units);

        Task<GetOfferReply> GetOffer(ulong offerId);

        Task<GetOffersReply> GetSellOffers(CurrencyOrAccountLocator locator,
            bool? availableOnly = null, int? firstindex = null, int? lastindex = null);

        Task<TransactionCreated> IssueCurrency(IssueCurrencyParameters issueCurrencyParameters, CreateTransactionParameters parameters);

        Task<TransactionCreated> PublishExchangeOffer(PublishExchangeOfferParameters exchangeOfferParameters, CreateTransactionParameters parameters);

        Task<CurrenciesReply> SearchCurrencies(string query, int? firstIndex = null, int? lastIndex = null, bool? includeCounts = null);

        Task<TransactionCreated> TransferCurrency(string recipientId, ulong currencyId, long units, CreateTransactionParameters parameters);
    }

    public class MonetarySystemService : BaseService, IMonetarySystemService
    {
        public MonetarySystemService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public MonetarySystemService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<CanDeleteCurrencyReply> CanDeleteCurrency(string accountId, ulong currencyId)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"account", accountId},
                {"currency", currencyId.ToString()}
            };
            return await Get<CanDeleteCurrencyReply>("canDeleteCurrency", queryParameters);
        }

        public async Task<TransactionCreated> CurrencyBuy(ulong currencyId, Amount rate, long units, CreateTransactionParameters parameters)
        {
            return await CurrencyTrade(currencyId, rate, units, parameters, "currencyBuy");
        }

        public async Task<TransactionCreated> CurrencyMint(ulong currencyId, long nonce, long units, long counter,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {"nonce", nonce.ToString()},
                {"units", units.ToString()},
                {"counter", counter.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("currencyMint", queryParameters);
        }

        public async Task<TransactionCreated> CurrencyReserveClaim(ulong currencyId, long units,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {"units", units.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("currencyReserveClaim", queryParameters);
        }

        public async Task<TransactionCreated> CurrencyReserveIncrease(ulong currencyId, Amount amountPerUnitNqt,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {"amountPerUnitNQT", amountPerUnitNqt.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("currencyReserveIncrease", queryParameters);
        }

        public async Task<TransactionCreated> CurrencySell(ulong currencyId, Amount rate, long units, CreateTransactionParameters parameters)
        {
            return await CurrencyTrade(currencyId, rate, units, parameters, "currencySell");
        }

        public async Task<TransactionCreated> DeleteCurrency(ulong currencyId, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> {{"currency", currencyId.ToString()}};
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("deleteCurrency", queryParameters);
        }

        public async Task<GetAccountCurrenciesReply> GetAccountCurrencies(string accountId, ulong? currencyId = null,
            int? height = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", accountId}};
            AddToParametersIfHasValue("currency", currencyId, queryParameters);
            AddToParametersIfHasValue("height", height, queryParameters);
            return await Get<GetAccountCurrenciesReply>("getAccountCurrencies", queryParameters); // TODO: Check if issuerAccount is included (and update wiki)
        }

        public async Task<GetAccountCurrencyCountReply> GetAccountCurrencyCount(string accountId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("height", height, queryParameters);
            return await Get<GetAccountCurrencyCountReply>("getAccountCurrencyCount", queryParameters);
        }

        public async Task<GetAccountExchangeRequestsReply> GetAccountExchangeRequests(string accountId, ulong currencyId, int? firstindex = null,
            int? lastindex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"account", accountId},
                {"currency", currencyId.ToString()}
            };
            AddToParametersIfHasValue("firstIndex", firstindex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastindex, queryParameters);
            return await Get<GetAccountExchangeRequestsReply>("getAccountExchangeRequests", queryParameters);
        }

        public async Task<CurrenciesReply> GetAllCurrencies(int? firstindex = null, int? lastindex = null, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstindex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastindex, queryParameters);
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<CurrenciesReply>("getAllCurrencies", queryParameters);
        }

        public async Task<ExchangesReply> GetAllExchanges(DateTime? timestamp = null, int? firstindex = null,
            int? lastindex = null, bool? includeCurrencyInfo = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("timestamp", timestamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstindex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastindex, queryParameters);
            AddToParametersIfHasValue("includeCurrencyInfo", includeCurrencyInfo, queryParameters);
            return await Get<ExchangesReply>("getAllExchanges", queryParameters);
        }

        public async Task<GetOffersReply> GetBuyOffers(CurrencyOrAccountLocator locator,
            bool? availableOnly = null, int? firstindex = null, int? lastindex = null)
        {
            var queryParameters = locator.QueryParameters;
            AddToParametersIfHasValue("availableOnly", availableOnly, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstindex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastindex, queryParameters);
            return await Get<GetOffersReply>("getBuyOffers", queryParameters);
        }

        public async Task<CurrenciesReply> GetCurrencies(List<ulong> currencyIds, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"assets", currencyIds.Select(id => id.ToString()).ToList()}
            };
            if (includeCounts.HasValue)
            {
                queryParameters.Add("includeCounts", new List<string> { includeCounts.Value.ToString() });
            }
            return await Get<CurrenciesReply>("getCurrencies", queryParameters);
        }

        public async Task<CurrenciesReply> GetCurrenciesByIssuer(List<string> accountIds, int? firstIndex = null,
            int? lastIndex = null, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"account", accountIds}
            };
            if (firstIndex.HasValue)
            {
                queryParameters.Add("firstIndex", new List<string>(firstIndex.Value));
            }
            if (lastIndex.HasValue)
            {
                queryParameters.Add("lastIndex", new List<string>(lastIndex.Value));
            }
            if (includeCounts.HasValue)
            {
                queryParameters.Add("includeCounts", new List<string> {includeCounts.Value.ToString()});
            }
            return await Get<CurrenciesReply>("getCurrencies", queryParameters);
        }

        public async Task<CurrencyReply> GetCurrency(CurrencyLocator locator, bool? includeCounts = null)
        {
            var queryParameters = locator.QueryParameters;
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<CurrencyReply>("getCurrency", queryParameters);
        }

        public async Task<CountReply> GetCurrencyAccountCount(ulong currencyId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> {{"currency", currencyId.ToString()}};
            AddToParametersIfHasValue("height", height, queryParameters);
            return await Get<CountReply>("getCurrencyAccountCount", queryParameters);
        }

        public async Task<CurrencyAccountsReply> GetCurrencyAccounts(ulong currencyId, int? height = null, int? firstIndex = null,
            int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> {{"currency", currencyId.ToString()}};
            AddToParametersIfHasValue("height", height, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<CurrencyAccountsReply>("getCurrencyAccounts", queryParameters);
        }

        public async Task<CurrencyFoundersReply> GetCurrencyFounders(ulong currencyId)
        {
            var queryParameters = new Dictionary<string, string> {{"currency", currencyId.ToString()}};
            return await Get<CurrencyFoundersReply>("getCurrencyFounders", queryParameters);
        }

        public async Task<CurrencyIdsReply> GetCurrencyIds(int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<CurrencyIdsReply>("getCurrencyIds", queryParameters);
        }

        public async Task<CurrencyTransfersReply> GetCurrencyTransfers(CurrencyOrAccountLocator locator, int? firstIndex = null,
            int? lastIndex = null, bool? includeCurrencyInfo = null)
        {
            var queryParameters = locator.QueryParameters;
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeCurrencyInfo", includeCurrencyInfo, queryParameters);
            return await Get<CurrencyTransfersReply>("getCurrencyTransfers", queryParameters);
        }

        public async Task<ExchangesReply> GetExchanges(CurrencyOrAccountLocator locator, int? firstIndex = null,
            int? lastIndex = null, bool? includeCurrencyInfo = null)
        {
            var queryParameters = locator.QueryParameters;
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeCurrencyInfo", includeCurrencyInfo, queryParameters);
            return await Get<ExchangesReply>("getExchanges", queryParameters);
        }

        public async Task<ExchangesReply> GetExchangesByExchangeRequest(ulong transactionId, bool? includeCurrencyInfo = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            AddToParametersIfHasValue("includeCurrencyInfo", includeCurrencyInfo, queryParameters);
            return await Get<ExchangesReply>("getExchangesByExchangeRequest", queryParameters);
        }

        public async Task<ExchangesReply> GetExchangesByOffer(ulong offerId, int? firstindex = null,
            int? lastindex = null, bool? includeCurrencyInfo = null)
        {
            var queryParameters = new Dictionary<string, string> {{"offer", offerId.ToString()}};
            AddToParametersIfHasValue("firstIndex", firstindex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastindex, queryParameters);
            AddToParametersIfHasValue("includeCurrencyInfo", includeCurrencyInfo, queryParameters);
            return await Get<ExchangesReply>("getExchangesByOffer", queryParameters);
        }

        public async Task<GetMintingTargetReply> GetMintingTarget(ulong currencyId, string accountId, long units)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {"account", accountId},
                {"units", units.ToString()}
            };
            return await Get<GetMintingTargetReply>("getMintingTarget", queryParameters);
        }

        public async Task<GetOfferReply> GetOffer(ulong offerId)
        {
            var queryParameters = new Dictionary<string, string> {{"offer", offerId.ToString()}};
            return await Get<GetOfferReply>("getOffer", queryParameters);
        }

        public async Task<GetOffersReply> GetSellOffers(CurrencyOrAccountLocator locator,
            bool? availableOnly = null, int? firstindex = null, int? lastindex = null)
        {
            var queryParameters = locator.QueryParameters;
            AddToParametersIfHasValue("availableOnly", availableOnly, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstindex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastindex, queryParameters);
            return await Get<GetOffersReply>("getSellOffers", queryParameters);
        }

        public async Task<TransactionCreated> IssueCurrency(IssueCurrencyParameters issueCurrencyParameters, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>();
            parameters.AppendToQueryParameters(queryParameters);
            issueCurrencyParameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("issueCurrency", queryParameters);
        }

        public async Task<TransactionCreated> PublishExchangeOffer(PublishExchangeOfferParameters exchangeOfferParameters, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>();
            parameters.AppendToQueryParameters(queryParameters);
            exchangeOfferParameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("publishExchangeOffer", queryParameters);
        }

        public async Task<CurrenciesReply> SearchCurrencies(string query, int? firstIndex = null, int? lastIndex = null, bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string> {{"query", query}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeCounts", includeCounts, queryParameters);
            return await Get<CurrenciesReply>("searchCurrencies", queryParameters);
        }

        public async Task<TransactionCreated> TransferCurrency(string recipientId, ulong currencyId, long units, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"recipient", recipientId},
                {"currency", currencyId.ToString()},
                {"units", units.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("transferCurrency", queryParameters);
        }

        private async Task<TransactionCreated> CurrencyTrade(ulong currencyId, Amount rate, long units, CreateTransactionParameters parameters, string tradeType)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {"rateNQT", rate.Nqt.ToString()},
                {"units", units.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>(tradeType, queryParameters);
        }
    }
}
