using System.Diagnostics;

namespace NxtLib.VotingSystem
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum VotingModel
    {
        OneVotePerAccount = 0,
        VoteByNxtBalance = 1,
        VoteByAsset = 2,
        VoteByCurrency = 3
    }
}