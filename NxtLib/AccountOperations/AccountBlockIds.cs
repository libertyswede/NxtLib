using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AccountOperations
{
    public class AccountBlockIds : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public List<ulong> BlockIds { get; set; }
    }
}