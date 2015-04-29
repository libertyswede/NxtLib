using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class AccountInfo
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "account")]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}