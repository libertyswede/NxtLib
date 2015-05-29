using System;
using System.Globalization;
using System.Linq;
using NxtLib;
using NxtLib.AssetExchange;
using NxtLib.Blocks;
using NxtLib.ServerInfo;
using TransactionSubType = NxtLib.TransactionSubType;

namespace DividendTracker
{
    /// <summary>
    /// Simple program that prints out all dividends payed out by using the built in dividend transaction type.
    /// To help out the folks on nxtforum: https://nxtforum.org/assets-board/record-of-invisible-dividends-(sticky-requested)/
    /// Start it with command line arguments: -height x to set the blockchein height where it should start scan
    /// Assumes you have an Nxt node running on localhost on default port (7876) open.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var blockService = new BlockService();
            var serverInfoService = new ServerInfoService();
            var assetService = new AssetExchangeService();
            var currentHeight = serverInfoService.GetBlockchainStatus().Result.NumberOfBlocks;
            var blockHeight = 335690;
            Block<Transaction> block = null;

            if (args.Length > 0 && args[0].Equals("-height", StringComparison.InvariantCultureIgnoreCase))
                Int32.TryParse(args[1], out blockHeight);

            Console.WriteLine("Start scanning blockchain at height: {0}", blockHeight);
            for (; blockHeight < currentHeight; blockHeight++)
            {
                block = blockService.GetBlockIncludeTransactions(BlockLocator.ByHeight(blockHeight)).Result;
                foreach (var transaction in block.Transactions.Where(transaction => transaction.SubType == TransactionSubType.ColoredCoinsDividendPayment))
                {
                    var attachment = (ColoredCoinsDividendPaymentAttachment)transaction.Attachment;
                    var asset = assetService.GetAsset(attachment.AssetId).Result;
                    Console.WriteLine("{0} {1} {2} {3}", transaction.Timestamp.ToString("d"), transaction.TransactionId, asset.Name, attachment.AmountPerQnt.Nxt);
                }
            }
            if (block != null)
            {
                Console.WriteLine("Last checked block height: {0} ({1})", blockHeight, block.Timestamp.ToString(CultureInfo.InvariantCulture));
            }
            Console.ReadLine();
        }
    }
}
