using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class CurrencyFoundersReply : BaseReply
    {
        public List<CurrencyFounder> Founders { get; set; }
    }
}