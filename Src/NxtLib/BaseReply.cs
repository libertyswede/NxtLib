using System.Collections.Generic;

namespace NxtLib
{
    public interface IBaseReply
    {
        IEnumerable<KeyValuePair<string, string>> PostData { get; set; }
        string RequestUri { get; set; }
        int RequestProcessingTime { get; set; }
        string RawJsonReply { get; set; }
    }

    public abstract class BaseReply : IBaseReply
    {
        public IEnumerable<KeyValuePair<string, string>> PostData { get; set; }
        public string RequestUri { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RawJsonReply { get; set; }
    }
}