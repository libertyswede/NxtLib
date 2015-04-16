using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.ServerInfo
{
    public class GetStateReply : BaseReply
    {
        public string Application { get; set; }
        public int AvailableProcessors { get; set; }
        public ulong CumulativeDifficulty { get; set; }
        public int CurrentMinRollbackHeight { get; set; }
        public ulong FreeMemory { get; set; }
        public bool IsScanning { get; set; }
        public bool IsOffline { get; set; }
        public bool IsTestnet { get; set; }
        public ulong LastBlock { get; set; }
        public string LastBlockchainFeeder { get; set; }
        public int LastBlockchainFeederHeight { get; set; }
        public bool NeedsAdminPassword { get; set; }
        public long MaxMemory { get; set; }
        public int MaxPrunableLifetime { get; set; }
        public int MaxRollback { get; set; }
        public int NumberOfAccounts { get; set; }
        public int NumberOfAliases { get; set; }
        public int NumberOfAskOrders { get; set; }
        public int NumberOfAssets { get; set; }
        public int NumberOfBidOrders { get; set; }
        public int NumberOfBlocks { get; set; }
        public int NumberOfCurrencies { get; set; }
        public int NumberOfCurrencyTransfers { get; set; }
        public int NumberOfExchanges { get; set; }
        public int NumberOfGoods { get; set; }
        public int NumberOfOffers { get; set; }
        public int NumberOfOrders { get; set; }
        public int NumberOfPeers { get; set; }
        public int NumberOfPhasedTransactions { get; set; }
        public int NumberOfPolls { get; set; }
        public int NumberOfPrunableMessages { get; set; }
        public int NumberOfPurchases { get; set; }
        public int NumberOfTrades { get; set; }
        public int NumberOfTags { get; set; }
        public int NumberOfTransactions { get; set; }
        public int NumberOfTransfers { get; set; }
        public int NumberOfUnlockedAccounts { get; set; }
        public int NumberOfVotes { get; set; }
        public int PeerPort { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Time { get; set; }
        public long TotalMemory { get; set; }
        public string Version { get; set; }
    }
}