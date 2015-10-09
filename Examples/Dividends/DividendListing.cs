using System;
using System.Collections.Generic;
using System.Linq;
using NxtLib;
using NxtLib.AssetExchange;
using NxtLib.Transactions;

namespace Dividends
{
    public class DividendListing
    {
        private readonly IAssetExchangeService _assetService = new AssetExchangeService();
        private readonly ITransactionService _transactionService = new TransactionService();
        private List<AssetTradeInfo> _trades;
        private List<AssetTransfer> _transfers;

        public void List(ProgramOptions options)
        {
            var assetList = GetAssetList(options);
            var dividendTransactions = GetDividendTransactions(assetList, options);
            PrintDividendTransactions(dividendTransactions, assetList);
        }

        private IList<Asset> GetAssetList(ProgramOptions options)
        {
            var assetList = new List<Asset>();
            if (options.Mode == Mode.All)
            {
                assetList = _assetService.GetAllAssets().Result.AssetList;
            }
            else if (options.Mode == Mode.Account)
            {
                _transfers = _assetService.GetAssetTransfers(AssetIdOrAccountId.ByAccountId(options.Id.ToString()), includeAssetInfo: false).Result.Transfers;
                _trades = _assetService.GetTrades(AssetIdOrAccountId.ByAccountId(options.Id.ToString()), includeAssetInfo: false).Result.Trades;

                var assetIds = _transfers.Select(transfer => transfer.AssetId).Union(_trades.Select(trade => trade.AssetId)).Distinct();

                assetList = _assetService.GetAssets(assetIds).Result.AssetList;
            }
            else if (options.Mode == Mode.Asset)
            {
                assetList = new List<Asset> { _assetService.GetAsset(options.Id).Result };
            }
            return assetList;
        }

        private IEnumerable<Transaction> GetDividendTransactions(IEnumerable<Asset> assetList, ProgramOptions options)
        {
            var dividendTransactions = new List<Transaction>();

            foreach (var asset in assetList)
            {
                if (dividendTransactions.All(t => t.Sender != asset.AccountId))
                {
                    var transactionsReply = _transactionService.GetBlockchainTransactions(asset.AccountRs, transactionType: TransactionSubType.ColoredCoinsDividendPayment).Result;
                    transactionsReply.Transactions
                        .Where(t => options.Mode != Mode.Account || OwnsAssetAtHeight(options.Id, t))
                        .ToList()
                        .ForEach(t => dividendTransactions.Add(t));
                }
            }
            return dividendTransactions;
        }

        private bool OwnsAssetAtHeight(ulong accountId, Transaction dividendTransaction)
        {
            var quantity = 0L;
            var attachment = (ColoredCoinsDividendPaymentAttachment) dividendTransaction.Attachment;
            var assetId = attachment.AssetId;
            var height = attachment.Height;

            _trades.Where(t => t.Buyer == accountId && t.AssetId == assetId && t.Height < height).ToList().ForEach(t => quantity += t.QuantityQnt);
            _trades.Where(t => t.Seller == accountId && t.AssetId == assetId && t.Height < height).ToList().ForEach(t => quantity -= t.QuantityQnt);

            _transfers.Where(t => t.RecipientId == accountId && t.AssetId == assetId && t.Height < height).ToList().ForEach(t => quantity += t.QuantityQnt);
            _transfers.Where(t => t.SenderId == accountId && t.AssetId == assetId && t.Height < height).ToList().ForEach(t => quantity -= t.QuantityQnt);

            return quantity > 0;
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