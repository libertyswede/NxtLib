using System.Collections.Generic;

namespace NxtLib.TaggedData
{
    public class TaggedDataListReply : BaseReply
    {
        public List<TaggedData> Data { get; set; }
    }
}