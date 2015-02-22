using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class GetAccountExchangeRequestsReply : BaseReply
    {
        public List<ExchangeRequest> ExchangeRequests { get; set; }
    }
}