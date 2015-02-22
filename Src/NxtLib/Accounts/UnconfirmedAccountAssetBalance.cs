using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class UnconfirmedAccountAssetBalance
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Asset { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong UnconfirmedBalanceQnt { get; set; }
    }
}