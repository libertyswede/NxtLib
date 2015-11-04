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
                {Parameters.Account, account.AccountId.ToString()},
                {Parameters.Currency, currencyId.ToString()}
            };
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
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
                {Parameters.Currency, currencyId.ToString()},
                {Parameters.Nonce, nonce.ToString()},
                {Parameters.Units, units.ToString()},
                {Parameters.Counter, counter.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("currencyMint", queryParameters);
        }

        public async Task<TransactionCreatedReply> CurrencyReserveClaim(ulong currencyId, long units,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Currency, currencyId.ToString()},
                {Parameters.Units, units.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("currencyReserveClaim", queryParameters);
        }

        public async Task<TransactionCreatedReply> CurrencyReserveIncrease(ulong currencyId, Amount amountPerUnitNqt,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Currency, currencyId.ToString()},
                {Parameters.AmountPerUnitNqt, amountPerUnitNqt.Nqt.ToString()}
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
            var queryParameters = new Dictionary<string, string> {{Parameters.Currency, currencyId.ToString()}};
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("deleteCurrency", queryParameters);
        }

        public async Task<GetAccountCurrenciesReply> GetAccountCurrencies(Account account, ulong? currencyId = null,
            int? height = null, bool? includeCurrencyInfo = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Currency, currencyId);
            queryParameters.AddIfHasValue(Parameters.Height, height);
            queryParameters.AddIfHasValue(Parameters.IncludeCurrencyInfo, includeCurrencyInfo);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            
            // TODO: Check if issuerAccount is included (and update wiki)
            return await Get<GetAccountCurrenciesReply>("getAccountCurrencies", queryParameters);
        }

        public async Task<GetAccountCurrencyCountReply> GetAccountCurrencyCount(Account account, int? height = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Height, height);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetAccountCurrencyCountReply>("getAccountCurrencyCount", queryParameters);
        }

        public async Task<GetAccountExchangeRequestsReply> GetAccountExchangeRequests(Account account, ulong currencyId,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Account, account.AccountId.ToString()},
                {Parameters.Currency, currencyId.ToString()}
            };
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetAccountExchangeRequestsReply>("getAccountExchangeRequests", queryParameters);
        }

        public async Task<CurrenciesReply> GetAllCurrencies(int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeCounts, includeCounts);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<CurrenciesReply>("getAllCurrencies", queryParameters);
        }

        public async Task<ExchangesReply> GetAllExchanges(DateTime? timestamp = null, int? firstIndex = null,
            int? lastIndex = null, bool? includeCurrencyInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Timestamp, timestamp);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeCurrencyInfo, includeCurrencyInfo);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ExchangesReply>("getAllExchanges", queryParameters);
        }

        public async Task<GetOffersReply> GetBuyOffers(CurrencyOrAccountLocator locator, bool? availableOnly = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(Parameters.AvailableOnly, availableOnly);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetOffersReply>("getBuyOffers", queryParameters);
        }

        public async Task<CurrenciesReply> GetCurrencies(IEnumerable<ulong> currencyIds, bool? includeCounts = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {Parameters.Currencies, currencyIds.Select(id => id.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(Parameters.IncludeCounts, includeCounts);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<CurrenciesReply>("getCurrencies", queryParameters);
        }

        public async Task<CurrenciesReply> GetCurrenciesByIssuer(IEnumerable<Account> accounts, int? firstIndex = null,
            int? lastIndex = null, bool? includeCounts = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {Parameters.Account, accounts.Select(a => a.AccountId.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeCounts, includeCounts);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<CurrenciesReply>("getCurrencies", queryParameters);
        }

        public async Task<CurrencyReply> GetCurrency(CurrencyLocator locator, bool? includeCounts = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(Parameters.IncludeCounts, includeCounts);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<CurrencyReply>("getCurrency", queryParameters);
        }

        public async Task<CountReply> GetCurrencyAccountCount(ulong currencyId, int? height = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Currency, currencyId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Height, height);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<CountReply>("getCurrencyAccountCount", queryParameters);
        }

        public async Task<CurrencyAccountsReply> GetCurrencyAccounts(ulong currencyId, int? height = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Currency, currencyId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Height, height);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<CurrencyAccountsReply>("getCurrencyAccounts", queryParameters);
        }

        public async Task<CurrencyFoundersReply> GetCurrencyFounders(ulong currencyId, Account account = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Currency, currencyId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<CurrencyFoundersReply>("getCurrencyFounders", queryParameters);
        }

        public async Task<CurrencyIdsReply> GetCurrencyIds(int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<CurrencyIdsReply>("getCurrencyIds", queryParameters);
        }

        public async Task<CurrencyTransfersReply> GetCurrencyTransfers(CurrencyOrAccountLocator locator,
            int? firstIndex = null, int? lastIndex = null, DateTime? timestamp = null, bool? includeCurrencyInfo = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.Timestamp, timestamp);
            queryParameters.AddIfHasValue(Parameters.IncludeCurrencyInfo, includeCurrencyInfo);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<CurrencyTransfersReply>("getCurrencyTransfers", queryParameters);
        }

        public async Task<ExchangesReply> GetExchanges(CurrencyOrAccountLocator locator, int? firstIndex = null,
            int? lastIndex = null, DateTime? timestamp = null, bool? includeCurrencyInfo = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeCurrencyInfo, includeCurrencyInfo);
            queryParameters.AddIfHasValue(Parameters.Timestamp, timestamp);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ExchangesReply>("getExchanges", queryParameters);
        }

        public async Task<ExchangesReply> GetExchangesByExchangeRequest(ulong transactionId,
            bool? includeCurrencyInfo = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Transaction, transactionId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.IncludeCurrencyInfo, includeCurrencyInfo);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ExchangesReply>("getExchangesByExchangeRequest", queryParameters);
        }

        public async Task<ExchangesReply> GetExchangesByOffer(ulong offerId, int? firstIndex = null,
            int? lastIndex = null, bool? includeCurrencyInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Offer, offerId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeCurrencyInfo, includeCurrencyInfo);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ExchangesReply>("getExchangesByOffer", queryParameters);
        }

        public async Task<GetExpectedOffersReply> GetExpectedBuyOffers(ulong? currencyId = null, Account account = null,
            bool? sortByRate = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Currency, currencyId);
            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(Parameters.SortByRate, sortByRate);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetExpectedOffersReply>("getExpectedBuyOffers", queryParameters);
        }

        public async Task<ExpectedCurrencyTransfersReply> GetExpectedCurrencyTransfers(ulong? currencyId = null,
            Account account = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Currency, currencyId);
            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ExpectedCurrencyTransfersReply>("getExpectedCurrencyTransfers", queryParameters);
        }

        public async Task<GetExpectedExchangeRequestsReply> GetExpectedExchangeRequests(Account account = null,
            ulong? currencyId = null, bool? includeCurrencyInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(Parameters.Currency, currencyId);
            queryParameters.AddIfHasValue(Parameters.IncludeCurrencyInfo, includeCurrencyInfo);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetExpectedExchangeRequestsReply>("getExpectedExchangeRequests", queryParameters);
        }

        public async Task<ExchangesReply> GetLastExchanges(IEnumerable<ulong> currencyIds, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {Parameters.Currencies, currencyIds.Select(id => id.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ExchangesReply>("getLastExchanges", queryParameters);
        }

        public async Task<GetExpectedOffersReply> GetExpectedSellOffers(ulong? currencyId = null,
            Account account = null, bool? sortByRate = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Currency, currencyId);
            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(Parameters.SortByRate, sortByRate);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetExpectedOffersReply>("getExpectedSellOffers", queryParameters);
        }

        public async Task<GetMintingTargetReply> GetMintingTarget(ulong currencyId, Account account, long units,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Currency, currencyId.ToString()},
                {Parameters.Account, account.AccountId.ToString()},
                {Parameters.Units, units.ToString()}
            };
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetMintingTargetReply>("getMintingTarget", queryParameters);
        }

        public async Task<GetOfferReply> GetOffer(ulong offerId, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Offer, offerId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetOfferReply>("getOffer", queryParameters);
        }

        public async Task<GetOffersReply> GetSellOffers(CurrencyOrAccountLocator locator, bool? availableOnly = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(Parameters.AvailableOnly, availableOnly);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
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
            var queryParameters = new Dictionary<string, string> {{Parameters.Query, query}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeCounts, includeCounts);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<CurrenciesReply>("searchCurrencies", queryParameters);
        }

        public async Task<TransactionCreatedReply> TransferCurrency(Account recipient, ulong currencyId, long units,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Recipient, recipient.AccountId.ToString()},
                {Parameters.Currency, currencyId.ToString()},
                {Parameters.Units, units.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("transferCurrency", queryParameters);
        }

        private async Task<TransactionCreatedReply> CurrencyTrade(ulong currencyId, Amount rate, long units,
            CreateTransactionParameters parameters, string tradeType)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Currency, currencyId.ToString()},
                {Parameters.RateNqt, rate.Nqt.ToString()},
                {Parameters.Units, units.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>(tradeType, queryParameters);
        }
    }
}