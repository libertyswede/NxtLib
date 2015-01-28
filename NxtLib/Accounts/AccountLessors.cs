using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class AccountLessors : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Account { get; set; }
        public string AccountRs { get; set; }
        public int Height { get; set; }
        public List<AccountLessor> Lessors { get; set; }
    }
}