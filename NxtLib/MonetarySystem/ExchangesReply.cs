using System.Collections.Generic;

namespace NxtLib.MonetarySystemOperations
{
    public class ExchangesReply : BaseReply
    {
        public List<CurrencyExchange> Exchanges { get; set; }
    }
}