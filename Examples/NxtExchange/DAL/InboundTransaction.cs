namespace NxtExchange.DAL
{
    public enum TransactionStatus
    {
        Unconfirmed, // No confirmations
        Confirmed,   // >= 10 confirmations
        Secured,     // > 720 confirmations
        Removed      // TX did exist, but was removed from blockchain after for eg. processing a fork or blockchain reorganization
    }

    public class InboundTransaction
    {
        public int Id { get; set; }
        public long TransactionId { get; set; }
        public int CustomerId { get; set; }
        public long AmountNqt { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
