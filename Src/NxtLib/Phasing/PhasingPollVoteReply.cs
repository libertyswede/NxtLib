﻿using System.Collections.Generic;

namespace NxtLib.Phasing
{
    public class PhasingPollVoteReply : PhasingPollVote, IBaseReply
    {
        public IEnumerable<KeyValuePair<string, string>> PostData { get; set; }
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}