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
        
        static void Main(string[] args)
        {
            var options = Parse(args);
            _service = new AssetExchangeService();
            var transactionService = new TransactionService();
            var asset = (Asset)_service.GetAsset(options.AssetId, true).Result;

            var transfers = GetAssetTransfers(asset);
            var trades = GetAssetTrades(asset);
            var assetIssueTransaction = transactionService.GetTransaction(GetTransactionLocator.ByTransactionId(asset.AssetId)).Result;
            if (assetIssueTransaction.Timestamp.CompareTo(options.StartDate) > 0)
            {
                options.StartDate = assetIssueTransaction.Timestamp;
            }

            Console.WriteLine($"Asset Name     : {asset.Name}");
            Console.WriteLine($"Asset ID       : {asset.AssetId}");
            Console.WriteLine($"Asset Trades   : {asset.NumberOfTrades}");
            Console.WriteLine($"Asset Transfers: {asset.NumberOfTransfers}");
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

        private static void WriteYearlyData(DateTime startDate, IList<AssetTradeInfo> trades, IList<AssetTransfer> transfers)
        {
            var dateIndex = startDate.Date;
            Console.WriteLine("Date Trades Transfers");

            while (dateIndex < DateTime.Now)
            {
                var tradeCount = trades.Count(t => t.Timestamp.Year == dateIndex.Year && t.Timestamp < startDate);
                var transferCount = transfers.Count(t => t.Timestamp.Year == dateIndex.Year && t.Timestamp < startDate);

                Console.WriteLine($"{dateIndex:yyyy} {tradeCount.ToString().PadLeft(6)} {transferCount.ToString().PadLeft(9)}");
                dateIndex = dateIndex.AddYears(1);
            }
        }

        private static void WriteDailyData(DateTime startDate, IList<AssetTradeInfo> trades, IList<AssetTransfer> transfers)
        {
            var dateIndex = startDate.Date;
            Console.WriteLine("Date       Trades Transfers");

            while (dateIndex < DateTime.Now)
            {
                var tradeCount = trades.Count(t => t.Timestamp.Date == dateIndex);
                var transferCount = transfers.Count(t => t.Timestamp.Date == dateIndex);

                Console.WriteLine($"{dateIndex:yyyy-MM-dd} {tradeCount.ToString().PadLeft(6)} {transferCount.ToString().PadLeft(9)}");
                dateIndex = dateIndex.AddDays(1);
            }
        }

        private static void WriteWeeklyData(DateTime startDate, IList<AssetTradeInfo> trades, IList<AssetTransfer> transfers)
        {
            var dateIndex = startDate.AddDays(-((int)(startDate.DayOfWeek + 6) % 7)).Date;
            var calendar = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar;
            Console.WriteLine("Year-Week Trades Transfers");

            while (dateIndex < DateTime.Now)
            {
                var tradeCount = trades.Count(t => (t.Timestamp - dateIndex).Days < 7 && (t.Timestamp - dateIndex).Days > 0 && t.Timestamp < startDate);
                var transferCount = transfers.Count(t => (t.Timestamp - dateIndex).Days < 7 && (t.Timestamp - dateIndex).Days > 0 && t.Timestamp < startDate);
                var week = calendar.GetWeekOfYear(dateIndex, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

                Console.WriteLine($"{dateIndex:yyyy}   {week.ToString().PadLeft(2)} {tradeCount.ToString().PadLeft(6)} {transferCount.ToString().PadLeft(9)}");
                dateIndex = dateIndex.AddDays(7);
            }
        }

        private static void WriteMonthlyData(DateTime startDate, IList<AssetTradeInfo> trades, IList<AssetTransfer> transfers)
        {
            var dateIndex = new DateTime(startDate.Year, startDate.Month, 1);
            Console.WriteLine("Date    Trades Transfers");

            while (dateIndex < DateTime.Now)
            {
                var tradeCount = trades.Count(t => t.Timestamp.Year == dateIndex.Year && t.Timestamp.Month == dateIndex.Month && t.Timestamp < startDate);
                var transferCount = transfers.Count(t => t.Timestamp.Year == dateIndex.Year && t.Timestamp.Month == dateIndex.Month && t.Timestamp < startDate);

                Console.WriteLine($"{dateIndex:yyyy-MM} {tradeCount.ToString().PadLeft(6)} {transferCount.ToString().PadLeft(9)}");
                dateIndex = dateIndex.AddMonths(1);
            }
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
