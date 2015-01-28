using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class ExchangesReply : BaseReply
    {
        public List<CurrencyExchange> Exchanges { get; set; }
    }
}