using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class UnconfirmedAccountTransactionIds : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public List<ulong> UnconfirmedTransactionIds { get; set; }
    }
}