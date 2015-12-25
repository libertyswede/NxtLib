using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class AccountPropertiesReply : BaseReply
    {
        public IEnumerable<AccountProperty> Properties { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Recipient)]
        public ulong RecipientId { get; set; }
        public string RecipientRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Setter)]
        public ulong? SetterId { get; set; }
        public string SetterRs { get; set; }
    }
}