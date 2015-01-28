using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Utils
{
    public class LongConvertReply : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long LongId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong StringId { get; set; }
    }
}