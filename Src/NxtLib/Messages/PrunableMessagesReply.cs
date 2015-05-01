using System.Collections.Generic;

namespace NxtLib.Messages
{
    public class PrunableMessagesReply : BaseReply
    {
        public List<PrunableMessage> PrunableMessages { get; set; }
    }
}