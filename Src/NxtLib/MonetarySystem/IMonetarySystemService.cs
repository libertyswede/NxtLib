using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.MonetarySystem
{
    public interface IMonetarySystemService
    {
        Task<CanDeleteCurrencyReply> CanDeleteCurrency(Account account, ulong currencyId, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TransactionCreatedReply> CurrencyBuy(ulong currencyId, Amount rate, long units,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> CurrencyMint(ulong currencyId, long nonce, long units, long counter,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> CurrencyReserveClaim(ulong currencyId, long units,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> CurrencyReserveIncrease(ulong currencyId, Amount amountPerUnitNqt,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> CurrencySell(ulong currencyId, Amount rate, long units,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> DeleteCurrency(ulong currencyId, CreateTransactionParameters parameters);

        Task<GetAccountCurrenciesReply> GetAccountCurrencies(Account account, ulong? currencyId = null,
            int? height = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetAccountCurrencyCountReply> GetAccountCurrencyCount(Account account, int? height = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetAccountExchangeRequestsReply> GetAccountExchangeRequests(Account account, ulong currencyId,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<CurrenciesReply> GetAllCurrencies(int? firstIndex = null, int? lastIndex = null, bool? includeCounts = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ExchangesReply> GetAllExchanges(DateTime? timestamp = null, int? firstIndex = null,
            int? lastIndex = null, bool? includeCurrencyInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GetOffersReply> GetBuyOffers(CurrencyOrAccountLocator locator,
            bool? availableOnly = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<CurrenciesReply> GetCurrencies(IEnumerable<ulong> currencyIds, bool? includeCounts = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<CurrenciesReply> GetCurrenciesByIssuer(IEnumerable<Account> accounts, int? firstIndex = null,
            int? lastIndex = null, bool? includeCounts = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<CurrencyReply> GetCurrency(CurrencyLocator locator, bool? includeCounts = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<CountReply> GetCurrencyAccountCount(ulong currencyId, int? height = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<CurrencyAccountsReply> GetCurrencyAccounts(ulong currencyId, int? height = null, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<CurrencyFoundersReply> GetCurrencyFounders(ulong currencyId, Account account = null, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<CurrencyIdsReply> GetCurrencyIds(int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<CurrencyTransfersReply> GetCurrencyTransfers(CurrencyOrAccountLocator locator, int? firstIndex = null,
            int? lastIndex = null, DateTime? timestamp = null, bool? includeCurrencyInfo = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ExchangesReply> GetExchanges(CurrencyOrAccountLocator locator, int? firstIndex = null,
            int? lastIndex = null, DateTime? timestamp = null, bool? includeCurrencyInfo = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ExchangesReply> GetExchangesByExchangeRequest(ulong transactionId, bool? includeCurrencyInfo = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ExchangesReply> GetExchangesByOffer(ulong offerId, int? firstIndex = null,
            int? lastIndex = null, bool? includeCurrencyInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GetExpectedOffersReply> GetExpectedBuyOffers(ulong? currencyId = null, Account account = null,
            bool? sortByRate = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ExpectedCurrencyTransfersReply> GetExpectedCurrencyTransfers(ulong? currencyId = null,
            Account account = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetExpectedExchangeRequestsReply> GetExpectedExchangeRequests(Account account = null,
            ulong? currencyId = null, bool? includeCurrencyInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GetExpectedOffersReply> GetExpectedSellOffers(ulong? currencyId = null, Account account = null,
            bool? sortByRate = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ExchangesReply> GetLastExchanges(IEnumerable<ulong> currencyIds, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GetMintingTargetReply> GetMintingTarget(ulong currencyId, Account account, long units,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetOfferReply> GetOffer(ulong offerId, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetOffersReply> GetSellOffers(CurrencyOrAccountLocator locator,
            bool? availableOnly = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TransactionCreatedReply> IssueCurrency(IssueCurrencyParameters issueCurrencyParameters,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> PublishExchangeOffer(PublishExchangeOfferParameters exchangeOfferParameters,
            CreateTransactionParameters parameters);

        Task<CurrenciesReply> SearchCurrencies(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeCounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TransactionCreatedReply> TransferCurrency(Account recipient, ulong currencyId, long units,
            CreateTransactionParameters parameters);
    }
}