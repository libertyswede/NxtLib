using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
}