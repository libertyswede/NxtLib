using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class GetExpectedOffersReply : BaseReply
    {
        public List<ExpectedCurrencyExchangeOffer> Offers { get; set; }
    }
}