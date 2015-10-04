using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class ExpectedCurrencyTransfersReply : BaseReply
    {
        public List<ExpectedCurrencyTransfer> Transfers { get; set; }
    }
}