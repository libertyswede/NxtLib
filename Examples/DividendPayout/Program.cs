using System;
using System.Collections.Generic;
using System.Linq;
using NxtLib;
using NxtLib.AssetExchange;
using NxtLib.Transactions;

namespace DividendPayout
{
    public class Program
    {
        private const string GenesisAccountRs = "NXT-MRCC-2YLS-8M54-3CMAJ";
        private static readonly IAssetExchangeService AssetService = new AssetExchangeService();
        private static readonly ITransactionService TransactionService = new TransactionService();
        private static Asset _asset;

        public static void Main(string[] args)
        {
            var transactionId = GetTransactionIdFromArguments(args);
            var attachment = GetTransactionAttachment(transactionId);
            _asset = AssetService.GetAsset(attachment.AssetId, true).Result;
            var decimalMultiplier = (decimal)Math.Pow(10, _asset.Decimals);
            var assetOwners = GetOwners(attachment.Height).Where(FilterOwners()).OrderByDescending(d => d.QuantityQnt).ToList();
            var totalSpent = Amount.CreateAmountFromNqt(assetOwners.Sum(o => o.QuantityQnt) * attachment.AmountPerQnt.Nqt);

            Console.WriteLine("Using dividend transaction: {0}", transactionId);
            Console.WriteLine("Fetching dividend payments for asset: {0} ({1})", _asset.Name, _asset.AssetId);
            Console.WriteLine("Total in dividend: {0} NXT", totalSpent.Nxt);
            Console.WriteLine("Per share: {0} NXT", attachment.AmountPerQnt.Nxt * decimalMultiplier);
            Console.WriteLine("Number of shareholders at height {0}: {1}", attachment.Height, assetOwners.Count);
            Console.WriteLine("----------------------------------------------------------------");

            foreach (var assetOwner in assetOwners)
            {
                var quantityQnt = assetOwner.QuantityQnt;
                var amountRecieved = Amount.CreateAmountFromNqt(quantityQnt * attachment.AmountPerQnt.Nqt);
                Console.WriteLine("Account: {0}, Shares: {1}, Amount: {2} NXT",
                    assetOwner.AccountRs, assetOwner.QuantityQnt / decimalMultiplier, amountRecieved.Nxt);
            }

            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static ulong GetTransactionIdFromArguments(IReadOnlyList<string> args)
        {
            ulong transactionId = 0;
            if (args.Count <= 1 || !args[0].Equals("-transaction") || !UInt64.TryParse(args[1], out transactionId))
            {
                Console.WriteLine("Provide a dividend transaction id as argument using -transaction xxxxx");
                Environment.Exit(0);
            }
            return transactionId;
        }

        private static ColoredCoinsDividendPaymentAttachment GetTransactionAttachment(ulong transactionId)
        {
            var transaction = TransactionService.GetTransaction(GetTransactionLocator.ByTransactionId(transactionId)).Result;
            return (ColoredCoinsDividendPaymentAttachment) transaction.Attachment;
        }

        private static Func<AssetOwner, bool> FilterOwners()
        {
            return o => o.AccountRs != _asset.AccountRs && o.AccountRs != GenesisAccountRs;
        }

        private static IEnumerable<AssetOwner> GetOwners(int height)
        {
            try
            {
                var assetAccounts = AssetService.GetAssetAccounts(_asset.AssetId, height).Result;
                return assetAccounts.AccountAssets.Select(aa => new AssetOwner(aa.AccountRs, aa.QuantityQnt));
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    var nxtException = e as NxtException;
                    return nxtException != null && nxtException.Message.StartsWith("Historical data as of height");
                });
            }
            return CalculateOwnership(height);
        }

        private static IEnumerable<AssetOwner> CalculateOwnership(int height)
        {
            var owners = new Dictionary<string, long> {{_asset.AccountRs,  _asset.QuantityQnt}};
            var index = 0;
            while (index < _asset.NumberOfTrades)
            {
                var getTradesResult = AssetService.GetTrades(AssetIdOrAccountId.ByAssetId(_asset.AssetId), index, index + 99, includeAssetInfo: false).Result;
                foreach (var trade in getTradesResult.Trades.Where(t => t.Height <= height))
                {
                    UpdateOwnership(owners, trade.BuyerRs, trade.SellerRs, trade.QuantityQnt);
                }
                index += 100;
            }
            index = 0;
            while (index < _asset.NumberOfTransfers)
            {
                var getTransfersResult = AssetService.GetAssetTransfers(AssetIdOrAccountId.ByAssetId(_asset.AssetId), index, index + 99, includeAssetInfo: false).Result;
                foreach (var transfer in getTransfersResult.Transfers.Where(t => t.Height <= height))
                {
                    UpdateOwnership(owners, transfer.RecipientRs, transfer.SenderRs, transfer.QuantityQnt);
                }
                index += 100;
            }
            return owners.Where(o => o.Value != 0).Select(o => new AssetOwner(o.Key, o.Value));
        }

        private static void UpdateOwnership(IDictionary<string, long> owners, string buyerRs, string sellerRs,
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
    }

    public class AssetOwner
    {
        public string AccountRs { get; set; }
        public long QuantityQnt { get; set; }

        public AssetOwner(string accountRs, long quantityQnt)
        {
            AccountRs = accountRs;
            QuantityQnt = quantityQnt;
        }

        public AssetOwner(string accountRs, ulong quantityQnt)
            : this(accountRs, (long)quantityQnt)
        {
        }
    }
}
