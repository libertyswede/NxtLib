using System.Diagnostics;

namespace NxtLib.VotingSystem
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum MinBalanceModel
    {
        [Description("NONE")]
        None = 0,
        [Description("NQT")]
        Nqt = 1,
        [Description("ASSET")]
        Asset = 2,
        [Description("CURRENCY")]
        Currency = 3
    }
}