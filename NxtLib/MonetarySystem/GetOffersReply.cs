using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class GetOffersReply : BaseReply
    {
        public List<CurrencyExchangeOffer> Offers { get; set; }
    }
}