using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Aliases
{
    public class AliasesReply : BaseReply
    {
        [JsonProperty(PropertyName = Parameters.Aliases)]
        public List<Alias> AliasList { get; set; }
    }
}