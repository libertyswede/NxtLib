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
        public InboundTransaction(Transaction transaction)
        {
            TransactionId = transaction.TransactionId.Value.ToSigned();
            AmountNqt = transaction.Amount.Nqt;
            SetStatus(transaction.Confirmations);
        }

        private void SetStatus(int? confirmations)
        {
            if (!confirmations.HasValue)
            {
                Status = TransactionStatus.Pending;
            }
            else
            {
                SetStatus(confirmations.Value);
            }
        }

        private void SetStatus(int confirmations)
        {
            if (confirmations < 10)
            {
                Status = TransactionStatus.Pending;
            }
            else if (confirmations < 720)
            {
                Status = TransactionStatus.Confirmed;
            }
            else if (confirmations >= 720)
            {
                Status = TransactionStatus.Secured;
            }
        }

        public int Id { get; set; }

        [Index("UQ_TransactionId", IsUnique = true)]
        public long TransactionId { get; set; }
        public int CustomerId { get; set; }
        public string DecryptedMessage { get; set; }
        public long AmountNqt { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
