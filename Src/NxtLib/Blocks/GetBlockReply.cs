using System.Collections.Generic;

namespace NxtLib.Blocks
{
    public class GetBlockReply<T> : Block<T>, IBaseReply
    {
        public IEnumerable<KeyValuePair<string, string>> PostData { get; set; }
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}