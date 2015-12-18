using System.Collections.Generic;

namespace NxtLib.AccountControl
{
    public class PhasingOnlyControlsReply : BaseReply
    {
        public IList<PhasingOnlyControl> PhasingOnlyControls { get; set; }
    }
}