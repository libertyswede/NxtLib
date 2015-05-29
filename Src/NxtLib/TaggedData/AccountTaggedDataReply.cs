using System.Collections.Generic;

namespace NxtLib.TaggedData
{
    public class AccountTaggedDataReply : BaseReply
    {
        public List<AccountTaggedData> Data { get; set; }
    }
}