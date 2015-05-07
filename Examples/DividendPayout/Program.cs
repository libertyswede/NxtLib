using System;
using System.Collections.Generic;
using System.Linq;
using NxtLib;
using NxtLib.AssetExchange;
using NxtLib.Transactions;

namespace DividendPayout
{
    class Program
    {
        private static IAssetExchangeService _assetService;
        private static Asset _asset;
        private static TransactionService _transactionService;
        private const ulong GenesisAccountId = 1739068987193023818;
        private const string GenesisAccountRs = "NXT-MRCC-2YLS-8M54-3CMAJ";

        static void Main(string[] args)
        {
            _assetService = new AssetExchangeService();
            _transactionService = new TransactionService();
            var transactionId = GetTransactionIdFromArguments(args);
            var transaction = _transactionService.GetTransaction(new GetTransactionLocator(transactionId)).Result;
            var attachment = (ColoredCoinsDividendPaymentAttachment)transaction.Attachment;

            _asset = _assetService.GetAsset(attachment.AssetId, true).Result;
            var totalSpent = Amount.CreateAmountFromNqt(((long)_asset.QuantityQnt) * attachment.AmountPerQnt.Nqt);
            var dividendRecievers = GetRecievers(attachment.Height).ToList();

            Console.WriteLine("Fetching dividend payments for asset: {0} using transaction id: {1}", _asset.Name, transactionId);
            Console.WriteLine("Total in dividend: {0} NXT", totalSpent.Nxt);
            Console.WriteLine("Per share (qnt): {0} NQT / {1} NXT", attachment.AmountPerQnt.Nqt, attachment.AmountPerQnt.Nxt);
            Console.WriteLine("Number of shareholders at height {0}: {1}", attachment.Height, dividendRecievers.Count());

            foreach (var accountAsset in dividendRecievers.OrderByDescending(d => d.QuantityQnt))
            {
                var quantityQnt = accountAsset.QuantityQnt;
                var amountRecieved = Amount.CreateAmountFromNqt(quantityQnt * attachment.AmountPerQnt.Nqt);
                Console.WriteLine("Account: {0}, Shares owned (QNT): {1}, Amount recieved: {2} NQT ({3} NXT)",
                    accountAsset.AccountRs, accountAsset.QuantityQnt, amountRecieved.Nqt, amountRecieved.Nxt);
            }
        }

        private static IEnumerable<DividendReciever> GetRecievers(int height)
        {
            try
            {
                var assetAccounts = _assetService.GetAssetAccounts(_asset.AssetId, height).Result;
                return assetAccounts.AccountAssets
                    .Where(aa => aa.AccountId != _asset.AccountId && aa.AccountId != GenesisAccountId)
                    .Select(aa => new DividendReciever(aa.AccountRs, aa.QuantityQnt));
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    var nxtException = e as NxtException;
                    return nxtException != null && nxtException.Message.StartsWith("Historical data as of height");
                });
            }
            return GetRecieversTheHardWay(height);
        }

        private static IEnumerable<DividendReciever> GetRecieversTheHardWay(int height)
        {
            var owners = new Dictionary<string, long> {{_asset.AccountRs, (long) _asset.QuantityQnt}};
            var index = 0;
            while (index < _asset.NumberOfTrades)
            {
                var getTradesResult = _assetService.GetTrades(new AssetIdOrAccountId(_asset.AssetId), index, index + 100, false).Result;
                getTradesResult.Trades.Where(t => t.Height <= height)
                    .ToList()
                    .ForEach(t => UpdateOwnership(owners, t.BuyerRs, t.SellerRs, (long) t.QuantityQnt));
                index += 100;
            }
            index = 0;
            while (index < _asset.NumberOfTransfers)
            {
                var getTransfersResult = _assetService.GetAssetTransfers(new AssetIdOrAccountId(_asset.AssetId), index, index + 100, false).Result;
                getTransfersResult.Transfers.Where(t => t.Height <= height)
                    .ToList()
                    .ForEach(t => UpdateOwnership(owners, t.RecipientRs, t.SenderRs, (long)t.QuantityQnt));
                index += 100;
            }
            return owners.ToList().OrderBy(o => o.Value)
                    .Where(o => o.Key != _asset.AccountRs && o.Key != GenesisAccountRs)
                    .Select(o => new DividendReciever(o.Key, o.Value));
        }

        private static void UpdateOwnership(Dictionary<string, long> owners, string buyerRs, string sellerRs,
            long quantity)
        {
            if (!owners.ContainsKey(sellerRs))
            {
                owners.Add(sellerRs, 0);
            }
            if (!owners.ContainsKey(buyerRs))
            {
                owners.Add(buyerRs, 0);
            }
            owners[sellerRs] -= quantity;
            owners[buyerRs] += quantity;
        }

        private static ulong GetTransactionIdFromArguments(string[] args)
        {
            var transactionId = 0UL;
            if (args.Length > 0 && args[0].Equals("-transaction", StringComparison.InvariantCultureIgnoreCase))
            {
                UInt64.TryParse(args[1], out transactionId);
            }
            else
            {
                Console.WriteLine("Provide a dividend transaction id as argument using -transaction xxxxx");
                Environment.Exit(0);
            }
            return transactionId;
        }
    }
    public class DividendReciever
    {
        public string AccountRs { get; set; }
        public long QuantityQnt { get; set; }

        public DividendReciever(string accountRs, long quantityQnt)
        {
            AccountRs = accountRs;
            QuantityQnt = quantityQnt;
        }

        public DividendReciever(string accountRs, ulong quantityQnt)
            : this(accountRs, (long)quantityQnt)
        {
        }
    }
}
