using System.Collections.Generic;

namespace NxtLib.MonetarySystemOperations
{
    public class GetAccountExchangeRequestsReply : BaseReply
    {
        public List<ExchangeRequest> ExchangeRequests { get; set; }
    }
}