using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class LessorInfo
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public int CurrentHeightFrom { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public int CurrentHeightTo { get; set; }
        public string CurrentLesseeRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long EffectiveBalanceNxt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public int NextHeightFrom { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public int NextHeightTo { get; set; }
        public string NextLesseeRs { get; set; }
    }
}