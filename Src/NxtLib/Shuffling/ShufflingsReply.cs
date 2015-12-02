using System.Collections.Generic;

namespace NxtLib.Shuffling
{
    public class ShufflingsReply : BaseReply
    {
        public IEnumerable<ShufflingData> Shufflings { get; set; }
    }
}