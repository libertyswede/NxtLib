using System.Collections.Generic;

namespace NxtLib.MonetarySystemOperations
{
    public class GetOffersReply : BaseReply
    {
        public List<CurrencyExchangeOffer> Offers { get; set; }
    }
}