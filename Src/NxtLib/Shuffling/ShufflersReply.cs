using System.Collections.Generic;

namespace NxtLib.Shuffling
{
    public class ShufflersReply : BaseReply
    {
        public IList<Shuffler> Shufflers { get; set; }
    }
}