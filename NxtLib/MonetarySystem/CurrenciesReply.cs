using System.Collections.Generic;

namespace NxtLib.MonetarySystemOperations
{
    public class CurrenciesReply : BaseReply
    {
        public List<Currency> Currencies { get; set; }
    }
}