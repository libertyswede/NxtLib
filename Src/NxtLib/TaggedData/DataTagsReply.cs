using System.Collections.Generic;

namespace NxtLib.TaggedData
{
    public class DataTagsReply : BaseReply
    {
        public List<TagCount> Tags { get; set; }
    }
}