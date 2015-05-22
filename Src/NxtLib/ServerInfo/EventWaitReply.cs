using System.Collections.Generic;

namespace NxtLib.ServerInfo
{
    public class EventWaitReply : BaseReply
    {
        public List<WaitEvent> Events { get; set; }
    }
}