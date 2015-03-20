using System.ComponentModel.DataAnnotations.Schema;
using NxtLib;

namespace NxtExchange.DAL
{
    public enum TransactionStatus
    {
        Pending,     // 0-10 confirmations
        Confirmed,   // >= 10 confirmations
        Secured,     // > 720 confirmations
        Removed      // TX did exist, but was removed from blockchain after for eg. processing a fork or blockchain reorganization
    }

    public class InboundTransaction
    {
        public int Id { get; set; }

        [Index("UQ_TransactionId", IsUnique = true)]
        public long TransactionId { get; set; }
        public string DecryptedMessage { get; set; }
        public long AmountNqt { get; set; }
        public TransactionStatus Status { get; set; }

        public InboundTransaction()
        {
        }

        public InboundTransaction(Transaction transaction)
        {
            TransactionId = transaction.TransactionId.Value.ToSigned();
            AmountNqt = transaction.Amount.Nqt;
            Status = TransactionStatusCalculator.GetStatus(transaction.Confirmations);
        }

        public Amount GetAmount()
        {
            return Amount.CreateAmountFromNqt(AmountNqt);
        }
    }
}
