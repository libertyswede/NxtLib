using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.MonetarySystem
{
    public class MonetarySystemService : BaseService, IMonetarySystemService
    {
        public MonetarySystemService(string baseAddress = Constants.DefaultNxtUrl)
            : base(baseAddress)
        {
        }

        public async Task<CanDeleteCurrencyReply> CanDeleteCurrency(Account account, ulong currencyId,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {nameof(account), account.AccountId.ToString()},
                {"currency", currencyId.ToString()}
            };
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<CanDeleteCurrencyReply>("canDeleteCurrency", queryParameters);
        }

        public async Task<TransactionCreatedReply> CurrencyBuy(ulong currencyId, Amount rate, long units,
            CreateTransactionParameters parameters)
        {
            return await CurrencyTrade(currencyId, rate, units, parameters, "currencyBuy");
        }

        public async Task<TransactionCreatedReply> CurrencyMint(ulong currencyId, long nonce, long units, long counter,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {nameof(nonce), nonce.ToString()},
                {nameof(units), units.ToString()},
                {nameof(counter), counter.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("currencyMint", queryParameters);
        }

        public async Task<TransactionCreatedReply> CurrencyReserveClaim(ulong currencyId, long units,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {nameof(units), units.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("currencyReserveClaim", queryParameters);
        }

        public async Task<TransactionCreatedReply> CurrencyReserveIncrease(ulong currencyId, Amount amountPerUnitNqt,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {"amountPerUnitNQT", amountPerUnitNqt.Nqt.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("currencyReserveIncrease", queryParameters);
        }

        public async Task<TransactionCreatedReply> CurrencySell(ulong currencyId, Amount rate, long units,
            CreateTransactionParameters parameters)
        {
            return await CurrencyTrade(currencyId, rate, units, parameters, "currencySell");
        }

        public async Task<TransactionCreatedReply> DeleteCurrency(ulong currencyId,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> {{"currency", currencyId.ToString()}};
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("deleteCurrency", queryParameters);
        }

        public async Task<GetAccountCurrenciesReply> GetAccountCurrencies(Account account, ulong? currencyId = null,
            int? height = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(account), account.AccountId.ToString()}};
            queryParameters.AddIfHasValue("currency", currencyId);
            queryParameters.AddIfHasValue(nameof(height), height);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            
            // TODO: Check if issuerAccount is included (and update wiki)
            return await Get<GetAccountCurrenciesReply>("getAccountCurrencies", queryParameters);
        }

        public async Task<GetAccountCurrencyCountReply> GetAccountCurrencyCount(Account account, int? height = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(account), account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(nameof(height), height);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetAccountCurrencyCountReply>("getAccountCurrencyCount", queryParameters);
        }

        public async Task<GetAccountExchangeRequestsReply> GetAccountExchangeRequests(Account account, ulong currencyId,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {nameof(account), account.AccountId.ToString()},
                {"currency", currencyId.ToString()}
            };
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetAccountExchangeRequestsReply>("getAccountExchangeRequests", queryParameters);
        }

        public async Task<CurrenciesReply> GetAllCurrencies(int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(includeCounts), includeCounts);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<CurrenciesReply>("getAllCurrencies", queryParameters);
        }

        public async Task<ExchangesReply> GetAllExchanges(DateTime? timestamp = null, int? firstIndex = null,
            int? lastIndex = null, bool? includeCurrencyInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(nameof(timestamp), timestamp);
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(includeCurrencyInfo), includeCurrencyInfo);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<ExchangesReply>("getAllExchanges", queryParameters);
        }

        public async Task<GetOffersReply> GetBuyOffers(CurrencyOrAccountLocator locator, bool? availableOnly = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(nameof(availableOnly), availableOnly);
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetOffersReply>("getBuyOffers", queryParameters);
        }

        public async Task<CurrenciesReply> GetCurrencies(IEnumerable<ulong> currencyIds, bool? includeCounts = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"currencies", currencyIds.Select(id => id.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(nameof(includeCounts), includeCounts);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<CurrenciesReply>("getCurrencies", queryParameters);
        }

        public async Task<CurrenciesReply> GetCurrenciesByIssuer(IEnumerable<Account> accounts, int? firstIndex = null,
            int? lastIndex = null, bool? includeCounts = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"account", accounts.Select(a => a.AccountId.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(includeCounts), includeCounts);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<CurrenciesReply>("getCurrencies", queryParameters);
        }

        public async Task<CurrencyReply> GetCurrency(CurrencyLocator locator, bool? includeCounts = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(nameof(includeCounts), includeCounts);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<CurrencyReply>("getCurrency", queryParameters);
        }

        public async Task<CountReply> GetCurrencyAccountCount(ulong currencyId, int? height = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"currency", currencyId.ToString()}};
            queryParameters.AddIfHasValue(nameof(height), height);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<CountReply>("getCurrencyAccountCount", queryParameters);
        }

        public async Task<CurrencyAccountsReply> GetCurrencyAccounts(ulong currencyId, int? height = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"currency", currencyId.ToString()}};
            queryParameters.AddIfHasValue(nameof(height), height);
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<CurrencyAccountsReply>("getCurrencyAccounts", queryParameters);
        }

        public async Task<CurrencyFoundersReply> GetCurrencyFounders(ulong currencyId, Account account = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"currency", currencyId.ToString()}};
            queryParameters.AddIfHasValue(nameof(account), account);
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<CurrencyFoundersReply>("getCurrencyFounders", queryParameters);
        }

        public async Task<CurrencyIdsReply> GetCurrencyIds(int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<CurrencyIdsReply>("getCurrencyIds", queryParameters);
        }

        public async Task<CurrencyTransfersReply> GetCurrencyTransfers(CurrencyOrAccountLocator locator,
            int? firstIndex = null, int? lastIndex = null, DateTime? timestamp = null, bool? includeCurrencyInfo = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(timestamp), timestamp);
            queryParameters.AddIfHasValue(nameof(includeCurrencyInfo), includeCurrencyInfo);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<CurrencyTransfersReply>("getCurrencyTransfers", queryParameters);
        }

        public async Task<ExchangesReply> GetExchanges(CurrencyOrAccountLocator locator, int? firstIndex = null,
            int? lastIndex = null, DateTime? timestamp = null, bool? includeCurrencyInfo = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(includeCurrencyInfo), includeCurrencyInfo);
            queryParameters.AddIfHasValue(nameof(timestamp), timestamp);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<ExchangesReply>("getExchanges", queryParameters);
        }

        public async Task<ExchangesReply> GetExchangesByExchangeRequest(ulong transactionId,
            bool? includeCurrencyInfo = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            queryParameters.AddIfHasValue(nameof(includeCurrencyInfo), includeCurrencyInfo);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<ExchangesReply>("getExchangesByExchangeRequest", queryParameters);
        }

        public async Task<ExchangesReply> GetExchangesByOffer(ulong offerId, int? firstIndex = null,
            int? lastIndex = null, bool? includeCurrencyInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"offer", offerId.ToString()}};
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(includeCurrencyInfo), includeCurrencyInfo);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<ExchangesReply>("getExchangesByOffer", queryParameters);
        }

        public async Task<GetExpectedOffersReply> GetExpectedBuyOffers(ulong? currencyId = null, Account account = null,
            bool? sortByRate = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue("currency", currencyId);
            queryParameters.AddIfHasValue(nameof(account), account);
            queryParameters.AddIfHasValue(nameof(sortByRate), sortByRate);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetExpectedOffersReply>("getExpectedBuyOffers", queryParameters);
        }

        public async Task<ExpectedCurrencyTransfersReply> GetExpectedCurrencyTransfers(ulong? currencyId = null,
            Account account = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue("currency", currencyId);
            queryParameters.AddIfHasValue(nameof(account), account);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<ExpectedCurrencyTransfersReply>("getExpectedCurrencyTransfers", queryParameters);
        }

        public async Task<GetExpectedExchangeRequestsReply> GetExpectedExchangeRequests(Account account = null,
            ulong? currencyId = null, bool? includeCurrencyInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(nameof(account), account);
            queryParameters.AddIfHasValue("currency", currencyId);
            queryParameters.AddIfHasValue(nameof(includeCurrencyInfo), includeCurrencyInfo);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetExpectedExchangeRequestsReply>("getExpectedExchangeRequests", queryParameters);
        }

        public async Task<ExchangesReply> GetLastExchanges(IEnumerable<ulong> currencyIds, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"currencies", currencyIds.Select(id => id.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<ExchangesReply>("getLastExchanges", queryParameters);
        }

        public async Task<GetExpectedOffersReply> GetExpectedSellOffers(ulong? currencyId = null,
            Account account = null, bool? sortByRate = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue("currency", currencyId);
            queryParameters.AddIfHasValue(nameof(account), account);
            queryParameters.AddIfHasValue(nameof(sortByRate), sortByRate);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetExpectedOffersReply>("getExpectedSellOffers", queryParameters);
        }

        public async Task<GetMintingTargetReply> GetMintingTarget(ulong currencyId, Account account, long units,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {nameof(account), account.AccountId.ToString()},
                {nameof(units), units.ToString()}
            };
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetMintingTargetReply>("getMintingTarget", queryParameters);
        }

        public async Task<GetOfferReply> GetOffer(ulong offerId, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"offer", offerId.ToString()}};
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetOfferReply>("getOffer", queryParameters);
        }

        public async Task<GetOffersReply> GetSellOffers(CurrencyOrAccountLocator locator, bool? availableOnly = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(nameof(availableOnly), availableOnly);
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetOffersReply>("getSellOffers", queryParameters);
        }

        public async Task<TransactionCreatedReply> IssueCurrency(IssueCurrencyParameters issueCurrencyParameters,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>();
            parameters.AppendToQueryParameters(queryParameters);
            issueCurrencyParameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("issueCurrency", queryParameters);
        }

        public async Task<TransactionCreatedReply> PublishExchangeOffer(
            PublishExchangeOfferParameters exchangeOfferParameters, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>();
            parameters.AppendToQueryParameters(queryParameters);
            exchangeOfferParameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("publishExchangeOffer", queryParameters);
        }

        public async Task<CurrenciesReply> SearchCurrencies(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(query), query}};
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(includeCounts), includeCounts);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<CurrenciesReply>("searchCurrencies", queryParameters);
        }

        public async Task<TransactionCreatedReply> TransferCurrency(Account recipient, ulong currencyId, long units,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {nameof(recipient), recipient.AccountId.ToString()},
                {"currency", currencyId.ToString()},
                {nameof(units), units.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("transferCurrency", queryParameters);
        }

        private async Task<TransactionCreatedReply> CurrencyTrade(ulong currencyId, Amount rate, long units,
            CreateTransactionParameters parameters, string tradeType)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"currency", currencyId.ToString()},
                {"rateNQT", rate.Nqt.ToString()},
                {nameof(units), units.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>(tradeType, queryParameters);
        }
    }
}