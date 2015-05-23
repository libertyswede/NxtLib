using System;
using System.Collections.Generic;
using System.Linq;
using NxtLib;
using NxtLib.Accounts;
using NxtLib.AssetExchange;

namespace Dividends
{
    public class DividendListing
    {
        private readonly IAssetExchangeService _assetService = new AssetExchangeService();
        private readonly IAccountService _accountService = new AccountService();

        public void List(ProgramOptions options)
        {
            var assetList = GetAssetList(options);
            var dividendTransactions = GetDividendTransactions(assetList);
            PrintDividendTransactions(dividendTransactions, assetList);
        }

        private IList<Asset> GetAssetList(ProgramOptions options)
        {
            var assetList = new List<Asset>();
            if (options.Mode == Mode.All || options.Mode == Mode.Account)
            {
                assetList = _assetService.GetAllAssets().Result.AssetList;
            }
            else if (options.Mode == Mode.Asset)
            {
                assetList = new List<Asset> { _assetService.GetAsset(options.Id).Result };
            }
            return assetList;
        }

        private IEnumerable<Transaction> GetDividendTransactions(IEnumerable<Asset> assetList)
        {
            var dividendTransactions = new List<Transaction>();

            foreach (var asset in assetList)
            {
                if (dividendTransactions.All(t => t.Sender != asset.AccountId))
                {
                    var transactionsReply = _accountService.GetAccountTransactions(asset.AccountRs, transactionType: TransactionSubType.ColoredCoinsDividendPayment).Result;
                    dividendTransactions.AddRange(transactionsReply.Transactions);
                }
            }
            return dividendTransactions;
        }

        private void PrintDividendTransactions(IEnumerable<Transaction> dividendTransactionsEnumerable, IList<Asset> assetList)
        {
            var transactions = dividendTransactionsEnumerable.OrderBy(t => t.Height).ToList();
            foreach (var transaction in transactions)
            {
                var attachment = (ColoredCoinsDividendPaymentAttachment)transaction.Attachment;
                var asset = assetList.Single(a => a.AssetId == attachment.AssetId);
                Console.WriteLine("{0} {1} {2} {3}", transaction.Timestamp.ToString("d"), transaction.TransactionId, asset.Name, attachment.AmountPerQnt.Nxt);
            }
        }
    }
}