namespace NxtLib.Accounts
{
    public class GetAccountLedgerEntryReply : AccountLedgerEntry, IBaseReply
    {
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}