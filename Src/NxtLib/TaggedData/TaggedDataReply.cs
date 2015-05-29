using System.Collections.Generic;

namespace NxtLib.TaggedData
{
    public class TaggedDataReply : BaseReply
    {
        public List<AccountTaggedData> Data { get; set; }
    }
}