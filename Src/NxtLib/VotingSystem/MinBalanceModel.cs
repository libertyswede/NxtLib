using System.Diagnostics;

namespace NxtLib.VotingSystem
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum MinBalanceModel
    {
        Nxt = 1,
        AssetBalance = 2,
        CurrencyBalance = 3
    }
}