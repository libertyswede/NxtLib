using System;
using System.Collections.Generic;
using System.Linq;
using NxtLib;
using NxtLib.AssetExchange;
using NxtLib.Local;
using NxtLib.Transactions;

namespace FindBigTraders
{
    /// <summary>
    /// The purpose of this application is to fetch top 5 accounts that has:
    /// * Most asset transfer transactions
    /// * Most asset trades
    /// * Most asset transfers and trades combined
    /// * Recieved most native dividend transactions
    /// </summary>
    public class Program
    {
        private static readonly IAssetExchangeService AssetExchangeService = new AssetExchangeService();
        private static readonly IDictionary<ulong, int> AssetTradeCount = new Dictionary<ulong, int>();
        private static readonly IDictionary<ulong, int> AssetTransferCount = new Dictionary<ulong, int>();
        private static readonly IDictionary<ulong, int> DividendCount = new Dictionary<ulong, int>();
        private static readonly IDictionary<ulong, List<AssetTrade>> AssetTrades = new Dictionary<ulong, List<AssetTrade>>();
        private static readonly IDictionary<ulong, List<AssetTransfer>> AssetTransfers = new Dictionary<ulong, List<AssetTransfer>>();
        private static readonly List<Asset> Assets = new List<Asset>();

        public static void Main()
        {
            GetAllAssets();
            GetAllTrades();
            GetAllTransfers();
            GetDividendPayments();


            foreach (var topTraders in AssetTradeCount.OrderByDescending(t => t.Value).Take(5))
            {
                var account = new Account(topTraders.Key);
                Console.WriteLine("Account: {0}, Trades: {1}", account.AccountRs, topTraders.Value);
            }
            foreach (var topTransferers in AssetTransferCount.OrderByDescending(t => t.Value).Take(5))
            {
                var account = new Account(topTransferers.Key);
                Console.WriteLine("Account: {0}, Transfers: {1}", account.AccountRs, topTransferers.Value);
            }
            var tradesAndTransfers = new Dictionary<ulong, int>();
            AssetTradeCount.ToList().ForEach(t => tradesAndTransfers[t.Key] = t.Value);
            AssetTransferCount.ToList().ForEach(t => tradesAndTransfers[t.Key] = GetValueOrDefault(tradesAndTransfers, t.Key) + t.Value);

            foreach (var combined in tradesAndTransfers.OrderByDescending(t => t.Value).Take(5))
            {
                var account = new Account(combined.Key);
                Console.WriteLine("Account: {0}, Trades & transfers: {1}", account.AccountRs, combined.Value);
            }

            foreach (var topDividendRecipients in DividendCount.OrderByDescending(t => t.Value).Take(5))
            {
                var account = new Account(topDividendRecipients.Key);
                Console.WriteLine("Account: {0}, Dividend count: {1}", account.AccountRs, topDividendRecipients.Value);
            }

            Console.WriteLine("Done and done!");
            Console.ReadLine();
        }

        private static void GetDividendPayments()
        {
            var transactionService = new TransactionService();
            var dividendPayingAccounts = Assets.Select(a => a.AccountId).Distinct().ToList();
            foreach (var accountId in dividendPayingAccounts)
            {
                var accountTransactionsReply = transactionService.GetBlockchainTransactions(accountId.ToString(), 
                    transactionType: TransactionSubType.ColoredCoinsDividendPayment).Result;
                foreach (var transaction in accountTransactionsReply.Transactions)
                {
                    var attachment = (ColoredCoinsDividendPaymentAttachment) transaction.Attachment;
                    var owners = CalculateOwnership(attachment);
                    owners.ToList().ForEach(o => DividendCount[o.AccountId] = GetValueOrDefault(DividendCount, o.AccountId) + 1);
                }
            }
        }

        private static IEnumerable<AssetOwner> CalculateOwnership(ColoredCoinsDividendPaymentAttachment dividend)
        {
            var asset = Assets.Single(a => a.AssetId == dividend.AssetId);
            var owners = new Dictionary<ulong, long> {{asset.AccountId, asset.QuantityQnt}};
            foreach (var trade in AssetTrades[asset.AssetId].Where(t => t.Height <= dividend.Height))
            {
                UpdateOwnership(owners, trade.Buyer, trade.Seller, trade.QuantityQnt);
            }
            foreach (var transfer in AssetTransfers[asset.AssetId].Where(t => t.Height <= dividend.Height))
            {
                UpdateOwnership(owners, transfer.RecipientId, transfer.SenderId, transfer.QuantityQnt);
            }
            return owners
                .Where(o => o.Value != 0 && o.Key != asset.AccountId && o.Key != Constants.GenesisAccount.AccountId)
                .Select(o => new AssetOwner(o.Key, o.Value));
        }

        private static void UpdateOwnership(IDictionary<ulong, long> owners, ulong buyerId, ulong sellerId,
            long quantity)
        {
            if (!owners.ContainsKey(sellerId))
            {
                owners.Add(sellerId, 0);
            }
            if (!owners.ContainsKey(buyerId))
            {
                owners.Add(buyerId, 0);
            }
            owners[sellerId] -= quantity;
            owners[buyerId] += quantity;
        }

        private static void GetAllTransfers()
        {
            foreach (var asset in Assets)
            {
                var transfers = new List<AssetTransfer>();
                AssetTransfers[asset.AssetId] = transfers;

                var index = 0;
                int replyCount;
                const int increase = 100;
                do
                {
                    var transfersReply = AssetExchangeService.GetAssetTransfers(AssetIdOrAccountId.ByAssetId(asset.AssetId), index,
                            index + increase - 1, includeAssetInfo: false).Result;
                    transfers.AddRange(transfersReply.Transfers);

                    foreach (var assetTransfer in transfersReply.Transfers)
                    {
                        AssetTransferCount[assetTransfer.SenderId] = GetValueOrDefault(AssetTransferCount, assetTransfer.SenderId) + 1;
                        AssetTransferCount[assetTransfer.RecipientId] = GetValueOrDefault(AssetTransferCount, assetTransfer.RecipientId) + 1;
                    }
                    replyCount = transfersReply.Transfers.Count;
                    index += increase;
                } while (replyCount == increase);
            }
        }

        private static void GetAllTrades()
        {
            foreach (var asset in Assets)
            {
                var trades = new List<AssetTrade>();
                AssetTrades.Add(asset.AssetId, trades);

                var index = 0;
                int replyCount;
                const int increase = 100;
                do
                {
                    var tradesReply = AssetExchangeService.GetTrades(AssetIdOrAccountId.ByAssetId(asset.AssetId), index,
                        index + increase - 1, includeAssetInfo: false).Result;
                    trades.AddRange(tradesReply.Trades);

                    foreach (var assetTrade in tradesReply.Trades)
                    {
                        AssetTradeCount[assetTrade.Buyer] = GetValueOrDefault(AssetTradeCount, assetTrade.Buyer) + 1;
                        AssetTradeCount[assetTrade.Seller] = GetValueOrDefault(AssetTradeCount, assetTrade.Seller) + 1;
                    }
                    replyCount = tradesReply.Trades.Count;
                    index += increase;
                } while (replyCount == increase);
            }
        }

        private static int GetValueOrDefault(IDictionary<ulong, int> tradesDictionary, ulong key)
        {
            int value;
            tradesDictionary.TryGetValue(key, out value);
            return value;
        }

        private static void GetAllAssets()
        {
            int replyCount;
            var index = 0;
            const int increase = 100;
            do
            {
                var allAssetsReply = AssetExchangeService.GetAllAssets(index, index + increase - 1, true).Result;
                Assets.AddRange(allAssetsReply.AssetList);
                replyCount = allAssetsReply.AssetList.Count;
                index += increase;
            } while (replyCount == increase);
        }
    }
}
