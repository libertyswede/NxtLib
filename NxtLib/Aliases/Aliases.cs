using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.Aliases
{
    public class Aliases : BaseReply
    {
        [JsonProperty(PropertyName = "aliases")]
        public List<Alias> AliasList { get; set; }
    }
}