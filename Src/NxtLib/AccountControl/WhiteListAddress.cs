using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AccountControl
{
    public class WhiteListAddress
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Whitelisted { get; set; }
        public string WhitelistedRs { get; set; }
    }
}