using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class AccountTransactionIdsReply : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public List<ulong> TransactionIds { get; set; }
    }
}