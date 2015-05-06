using System;
using System.Linq;
using NxtLib;
using NxtLib.AssetExchange;
using NxtLib.Transactions;

namespace DividendPayout
{
    class Program
    {
        static void Main(string[] args)
        {
            var assetService = new AssetExchangeService();
            var transactionService = new TransactionService();
            var transactionId = 0UL;
            if (args.Length > 0 && args[0].Equals("-transaction", StringComparison.InvariantCultureIgnoreCase))
                UInt64.TryParse(args[1], out transactionId);
            else
            {
                Console.WriteLine("Provide a dividend transaction id as argument using -transaction xxxxx");
                Environment.Exit(0);
            }

            var transaction = transactionService.GetTransaction(new GetTransactionLocator(transactionId)).Result;
            var attachment = (ColoredCoinsDividendPaymentAttachment)transaction.Attachment;
            var nqtPerQnt = (int)attachment.AmountPerQnt.Nqt;

            var asset = assetService.GetAsset(attachment.AssetId).Result;

            var assetAccounts = assetService.GetAssetAccounts(attachment.AssetId, attachment.Height).Result;
            var totalSpent = Amount.CreateAmountFromNqt(((long)asset.QuantityQnt) * nqtPerQnt);

            Console.WriteLine("Fetching dividend payments for asset: {0} using transaction id: {1}", asset.Name, transactionId);
            Console.WriteLine("Total in dividend: {0} NXT", totalSpent.Nxt);
            Console.WriteLine("Per share (qnt): {0} NQT / {1} NXT", nqtPerQnt, attachment.AmountPerQnt.Nxt);
            Console.WriteLine("Number of shareholders at height {0}: {1}", attachment.Height, assetAccounts.AccountAssets.Count);

            foreach (var accountAsset in assetAccounts.AccountAssets.OrderByDescending(a => a.QuantityQnt))
            {
                var quantityQnt = (long)accountAsset.QuantityQnt;
                var amountRecieved = Amount.CreateAmountFromNqt(quantityQnt * nqtPerQnt);
                Console.WriteLine("Account: {0}, Shares owned (QNT): {1}, Amount recieved: {2} NQT ({3} NXT)",
                    accountAsset.AccountRs, accountAsset.QuantityQnt, amountRecieved.Nqt, amountRecieved.Nxt);
            }
        }
    }
}
