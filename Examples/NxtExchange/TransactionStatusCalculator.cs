using System;
using NxtExchange.DAL;
using NxtLib;

namespace NxtExchange
{
    public class TransactionStatusCalculator
    {
        public static TransactionStatus GetStatus(Transaction transaction)
        {
            var confirmations = transaction.Confirmations;
            var blockTimestamp = transaction.BlockTimestamp;

            return !confirmations.HasValue || !blockTimestamp.HasValue
                ? TransactionStatus.Pending
                : GetStatus(transaction, confirmations.Value, blockTimestamp.Value);
        }

        private static TransactionStatus GetStatus(Transaction transaction, int confirmations, DateTime blockTimestamp)
        {
            var status = GetStatus(confirmations);

            // Check for risk of getting orphaned, see details on page 6 here: http://nxtinside.org/downloads/nxt-integration.pdf
            if (transaction.Timestamp.AddMinutes(transaction.Deadline) <= blockTimestamp.AddHours(23))
            {
                status = status == TransactionStatus.Confirmed ? TransactionStatus.Pending : status;
            }
            return status;
        }

        private static TransactionStatus GetStatus(int confirmations)
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