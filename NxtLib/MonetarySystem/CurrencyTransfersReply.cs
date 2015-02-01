using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class CurrencyTransfersReply : BaseReply
    {
        public List<CurrencyTransfer> Transfers { get; set; }
    }
}