using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Phasing
{
    public class PhasingPollWhiteList
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong WhiteListed { get; set; }
        public string WhiteListedRs { get; set; }
    }
}