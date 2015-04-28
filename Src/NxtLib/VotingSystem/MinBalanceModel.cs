using System.Diagnostics;

namespace NxtLib.VotingSystem
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum MinBalanceModel
    {
        [NxtApi("NONE")]
        None = 0,
        [NxtApi("NQT")]
        Nqt = 1,
        [NxtApi("ASSET")]
        Asset = 2,
        [NxtApi("CURRENCY")]
        Currency = 3
    }
}