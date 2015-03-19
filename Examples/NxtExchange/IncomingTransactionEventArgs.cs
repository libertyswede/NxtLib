using NxtExchange.DAL;

namespace NxtExchange
{
    public delegate void IncomingTransactionHandler(object sender, IncomingTransactionEventArgs e);

    public class IncomingTransactionEventArgs
    {
        public InboundTransaction Transaction { get; set; }

        public IncomingTransactionEventArgs(InboundTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}