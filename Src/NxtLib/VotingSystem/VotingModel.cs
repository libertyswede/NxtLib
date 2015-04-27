using System.Diagnostics;

namespace NxtLib.VotingSystem
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum VotingModel
    {
        [Description("NONE")]
        None = -1,
        [Description("ACCOUNT")]
        Account = 0,
        [Description("NQT")]
        Nqt = 1,
        [Description("ASSET")]
        Asset = 2,
        [Description("CURRENCY")]
        Currency = 3,
        [Description("TRANSACTION")]
        Transaction = 4,
        [Description("HASH")]
        Hash = 5
    }
}