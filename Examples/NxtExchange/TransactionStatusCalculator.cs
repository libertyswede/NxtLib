using NxtExchange.DAL;

namespace NxtExchange
{
    public class TransactionStatusCalculator
    {
        public static TransactionStatus GetStatus(int? confirmations)
        {
            return !confirmations.HasValue ? TransactionStatus.Pending : GetStatus(confirmations.Value);
        }

        public static TransactionStatus GetStatus(int confirmations)
        {
            if (confirmations < 10)
            {
                return TransactionStatus.Pending;
            }
            if (confirmations < 720)
            {
                return TransactionStatus.Confirmed;
            }
            return TransactionStatus.Secured;
        }
    }
}