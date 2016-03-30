using System.Collections;
using System.Collections.Generic;

namespace NxtLib.Accounts
{
    public class FundingMonitorReply : BaseReply
    {
        public IList<FundingMonitor> Monitors { get; set; }
    }
}