using System.Collections.Generic;

namespace NxtLib.Shuffling
{
    public class ShufflingParticipantsReply : BaseReply
    {
        public IList<ShufflingParticipant> Participants { get; set; }
    }
}