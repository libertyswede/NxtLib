using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class CurrenciesReply : BaseReply
    {
        public List<Currency> Currencies { get; set; }
    }
}