using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.Aliases
{
    public class AliasesReply : BaseReply
    {
        [JsonProperty(PropertyName = "aliases")]
        public List<Alias> AliasList { get; set; }
    }
}