using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class AccountProperty
    {
        public string Property { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Setter)]
        public ulong? SetterId { get; set; }
        public string SetterRs { get; set; }
        public string Value { get; set; }
    }
}