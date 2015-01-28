using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.AliasOperations
{
    public class Aliases : BaseReply
    {
        [JsonProperty(PropertyName = "aliases")]
        public List<Alias> AliasList { get; set; }
    }
}