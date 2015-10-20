using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NxtLib.AssetExchange;
using NxtLib.Transactions;

namespace AssetActivity
{
    class Program
    {
        private static AssetExchangeService _service;
        private static Asset _asset;
        private static decimal _assetDecimalFactor;

        static void Main(string[] args)
        {
            var options = Parse(args);
            _service = new AssetExchangeService();
            var transactionService = new TransactionService();
            _asset = _service.GetAsset(options.AssetId, true).Result;
            _assetDecimalFactor = (decimal)Math.Pow(10, -_asset.Decimals);

            var transfers = GetAssetTransfers(_asset);
            var trades = GetAssetTrades(_asset);
            var assetIssueTransaction = transactionService.GetTransaction(GetTransactionLocator.ByTransactionId(_asset.AssetId)).Result;
            if (assetIssueTransaction.Timestamp.CompareTo(options.StartDate) > 0)
            {
                options.StartDate = assetIssueTransaction.Timestamp;
            }

            Console.WriteLine($"Asset Name     : {_asset.Name}");
            Console.WriteLine($"Asset ID       : {_asset.AssetId}");
            Console.WriteLine($"Asset Trades   : {_asset.NumberOfTrades}");
            Console.WriteLine($"Asset Transfers: {_asset.NumberOfTransfers}");
            Console.WriteLine($"Issue Date     : {assetIssueTransaction.Timestamp:g}");
            Console.WriteLine($"Start Date     : {options.StartDate:d}");
            Console.WriteLine("------------------------------------------------");

            if (options.Range == Range.Daily)
            {
                WriteDailyData(options.StartDate, trades, transfers);
            }
            if (options.Range == Range.Weekly)
            {
                WriteWeeklyData(options.StartDate, trades, transfers);
            }
            if (options.Range == Range.Monthly)
            {
                WriteMonthlyData(options.StartDate, trades, transfers);
            }
            if (options.Range == Range.Yearly)
            {
                WriteYearlyData(options.StartDate, trades, transfers);
            }

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }

        private static void WriteDailyData(DateTime startDate, IList<AssetTradeInfo> allTrades, IList<AssetTransfer> allTransfers)
        {
            var dateIndex = startDate.Date;
            Console.WriteLine("Date       #Trades     Volume #Transfers     Volume");

            while (dateIndex < DateTime.Now)
            {
                var currentTrades = allTrades.Where(t => t.Timestamp.Date == dateIndex).ToList();
                var currentTransfers = allTransfers.Where(t => t.Timestamp.Date == dateIndex).ToList();
                Console.Write($"{dateIndex:yyyy-MM-dd} ");

                WriteAssetData(currentTrades, currentTransfers);
                dateIndex = dateIndex.AddDays(1);
            }
        }

        private static void WriteWeeklyData(DateTime startDate, IList<AssetTradeInfo> allTrades, IList<AssetTransfer> allTransfers)
        {
            var dateIndex = startDate.AddDays(-((int)(startDate.DayOfWeek + 6) % 7)).Date;
            var calendar = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar;
            Console.WriteLine("Year-Week #Trades     Volume #Transfers     Volume");

            while (dateIndex < DateTime.Now)
            {
                var currentTrades = allTrades.Where(t => (t.Timestamp - dateIndex).Days < 7 && (t.Timestamp - dateIndex).Days > 0 && t.Timestamp < startDate).ToList();
                var currentTransfers = allTransfers.Where(t => (t.Timestamp - dateIndex).Days < 7 && (t.Timestamp - dateIndex).Days > 0 && t.Timestamp < startDate).ToList();
                var week = calendar.GetWeekOfYear(dateIndex, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

                Console.WriteLine($"{dateIndex:yyyy}   {week.ToString().PadLeft(2)}");
                WriteAssetData(currentTrades, currentTransfers);
                dateIndex = dateIndex.AddDays(7);
            }
        }

        private static void WriteMonthlyData(DateTime startDate, IList<AssetTradeInfo> allTrades, IList<AssetTransfer> allTransfers)
        {
            var dateIndex = new DateTime(startDate.Year, startDate.Month, 1);
            Console.WriteLine("Date    #Trades     Volume #Transfers     Volume");

            while (dateIndex < DateTime.Now)
            {
                var currentTrades = allTrades.Where(t => t.Timestamp.Year == dateIndex.Year && t.Timestamp.Month == dateIndex.Month && t.Timestamp < startDate).ToList();
                var currentTransfers = allTransfers.Where(t => t.Timestamp.Year == dateIndex.Year && t.Timestamp.Month == dateIndex.Month && t.Timestamp < startDate).ToList();

                Console.WriteLine($"{dateIndex:yyyy-MM} ");
                WriteAssetData(currentTrades, currentTransfers);
                dateIndex = dateIndex.AddMonths(1);
            }
        }

        private static void WriteYearlyData(DateTime startDate, IList<AssetTradeInfo> allTrades, IList<AssetTransfer> allTransfers)
        {
            var dateIndex = startDate.Date;
            Console.WriteLine("Date #Trades     Volume #Transfers     Volume");

            while (dateIndex < DateTime.Now)
            {
                var currentTrades = allTrades.Where(t => t.Timestamp.Year == dateIndex.Year && t.Timestamp < startDate).ToList();
                var currentTransfers = allTransfers.Where(t => t.Timestamp.Year == dateIndex.Year && t.Timestamp < startDate).ToList();

                Console.WriteLine($"{dateIndex:yyyy} ");
                WriteAssetData(currentTrades, currentTransfers);
                dateIndex = dateIndex.AddYears(1);
            }
        }

        private static void WriteAssetData(ICollection<AssetTradeInfo> trades, ICollection<AssetTransfer> transfers)
        {
            var tradeCount = trades.Count;
            var transferCount = transfers.Count;
            var tradeVolume = trades.Sum(t => t.QuantityQnt) * _assetDecimalFactor;
            var transferVolume = transfers.Sum(t => t.QuantityQnt) * _assetDecimalFactor;

            Console.WriteLine($"{tradeCount.ToString().PadLeft(7)} {tradeVolume.ToString(CultureInfo.InvariantCulture).PadLeft(10)} " +
                              $"{transferCount.ToString().PadLeft(10)} {transferVolume.ToString(CultureInfo.InvariantCulture).PadLeft(10)}");
        }

        private static IList<AssetTradeInfo> GetAssetTrades(Asset asset)
        {
            const int count = 100;
            var index = 0;
            var hasMore = true;
            var trades = new List<AssetTradeInfo>();

            while (hasMore)
            {
                var tradesReply = _service.GetTrades(AssetIdOrAccountId.ByAssetId(asset.AssetId), index, index + count - 1,
                    includeAssetInfo: false).Result;
                trades.AddRange(tradesReply.Trades);

                index += count;
                hasMore = tradesReply.Trades.Count == count;
            }

            return trades;
        }

        private static IList<AssetTransfer> GetAssetTransfers(Asset asset)
        {
            var index = 0;
            const int count = 100;
            var hasMore = true;
            var transfers = new List<AssetTransfer>();

            while (hasMore)
            {
                var transfersReply =
                    _service.GetAssetTransfers(AssetIdOrAccountId.ByAssetId(asset.AssetId), index, index + count - 1,
                        includeAssetInfo: false).Result;
                transfers.AddRange(transfersReply.Transfers);

                index += count;
                hasMore = transfersReply.Transfers.Count == count;
            }
            return transfers;
        }

        private static ProgramOptions Parse(IReadOnlyList<string> args)
        {
            if (args.Count >= 2)
            {
                ulong id;
                if (ulong.TryParse(args[0], out id))
                {
                    var range = ParseRange(args[1]);
                    var startDate = DateTime.MinValue;
                    if (args.Count == 3)
                    {
                        DateTime.TryParse(args[2], out startDate);
                    }
                    if (range != Range.Undefined)
                    {
                        return new ProgramOptions { AssetId = id, Range = range, StartDate = startDate };
                    }
                }
            }
            Console.WriteLine("Usage: ");
            Console.WriteLine("[assetid] [range] [startdate]");
            Console.WriteLine("valid range values: daily, weekly, monthly, yearly");
            Console.WriteLine("Example: AssetActivity.exe 12982485703607823902 daily 2015-10-01");
            throw new NotSupportedException();
        }

        private static Range ParseRange(string argument)
        {
            switch (argument)
            {
                case "daily":
                    return Range.Daily;
                case "weekly":
                    return Range.Weekly;
                case "monthly":
                    return Range.Monthly;
                case "yearly":
                    return Range.Yearly;
                default: return Range.Undefined;
            }
        }
    }
}
