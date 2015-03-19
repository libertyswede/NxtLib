using NxtExchange.DAL;

namespace NxtExchange
{
    public delegate void TransactionStatusUpdatedHandler(object sender, StatusUpdatedEventArgs e);

    public class StatusUpdatedEventArgs
    {
        public InboundTransaction Transaction { get; private set; }
        public TransactionStatus PreviousStatus { get; private set; }

        public StatusUpdatedEventArgs(InboundTransaction transaction, TransactionStatus previousStatus)
        {
            Transaction = transaction;
            PreviousStatus = previousStatus;
        }
    }
}