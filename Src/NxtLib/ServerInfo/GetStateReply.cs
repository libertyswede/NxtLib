using System.Collections.Generic;

namespace NxtLib.ServerInfo
{
    public class GetStateReply : BlockchainStatus, IBaseReply
    {
        public int AvailableProcessors { get; set; }
        public ulong FreeMemory { get; set; }
        public bool IsOffline { get; set; }
        public ulong LastBlock { get; set; }
        public bool NeedsAdminPassword { get; set; }
        public long MaxMemory { get; set; }
        public int NumberOfAccountLeases { get; set; }
        public int NumberOfAccounts { get; set; }
        public int NumberOfActiveAccountLeases { get; set; }
        public int NumberOfActivePeers { get; set; }
        public int NumberOfAliases { get; set; }
        public int NumberOfAskOrders { get; set; }
        public int NumberOfAssets { get; set; }
        public int NumberOfBidOrders { get; set; }
        public int NumberOfCurrencies { get; set; }
        public int NumberOfCurrencyTransfers { get; set; }
        public int NumberOfDataTags { get; set; }
        public int NumberOfExchangeRequests { get; set; }
        public int NumberOfExchanges { get; set; }
        public int NumberOfGoods { get; set; }
        public int NumberOfOffers { get; set; }
        public int NumberOfOrders { get; set; }
        public int NumberOfPeers { get; set; }
        public int NumberOfPhasedTransactions { get; set; }
        public int NumberOfPolls { get; set; }
        public int NumberOfPrunableMessages { get; set; }
        public int NumberOfPurchases { get; set; }
        public int NumberOfTaggedData { get; set; }
        public int NumberOfTrades { get; set; }
        public int NumberOfTags { get; set; }
        public int NumberOfTransactions { get; set; }
        public int NumberOfTransfers { get; set; }
        public int NumberOfUnlockedAccounts { get; set; }
        public int NumberOfVotes { get; set; }
        public int PeerPort { get; set; }
        public IEnumerable<KeyValuePair<string, string>> PostData { get; set; }
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
        public long TotalMemory { get; set; }
    }
}