using System.Diagnostics;

namespace NxtLib.VotingSystem
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum VotingModel
    {
        [NxtApi("NONE")]
        None = -1,
        [NxtApi("ACCOUNT")]
        Account = 0,
        [NxtApi("NQT")]
        Nqt = 1,
        [NxtApi("ASSET")]
        Asset = 2,
        [NxtApi("CURRENCY")]
        Currency = 3,
        [NxtApi("TRANSACTION")]
        Transaction = 4,
        [NxtApi("HASH")]
        Hash = 5
    }
}