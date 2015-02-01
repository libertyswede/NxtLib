using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.MonetarySystem
{
    public class CurrencyIdsReply : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public List<ulong> CurrencyIds { get; set; }
    }
}