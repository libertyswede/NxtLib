using System.Collections.Generic;

namespace NxtLib.TaggedData
{
    public class AllTaggedDataReply : BaseReply
    {
        public List<TaggedData> TaggedData { get; set; }
    }
}