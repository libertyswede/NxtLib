using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.VotingSystem
{
    public class PollResult
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long? Result { get; set; }
        public long Weight { get; set; }
    }
}