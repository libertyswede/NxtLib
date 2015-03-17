namespace NxtExchange.DAL
{
    public class BlockchainStatus
    {
        public int Id { get; set; }
        public long LastConfirmedBlockId { get; set; }
        public long LastKnownBlockId { get; set; }
    }
}
