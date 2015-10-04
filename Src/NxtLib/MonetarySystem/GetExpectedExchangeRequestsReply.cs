using System.Collections.Generic;

namespace NxtLib.MonetarySystem
{
    public class GetExpectedExchangeRequestsReply : BaseReply
    {
        public List<ExpectedExchangeRequest> ExchangeRequests { get; set; }
    }
}