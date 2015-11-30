using System.Collections.Generic;

namespace NxtLib.Shuffling
{
    public class GetShufflingsReply : BaseReply
    {
        public IEnumerable<ShufflingData> Shufflings { get; set; }
    }
}