using System.Collections.Generic;
using NxtLib.VotingSystem;

namespace NxtLib
{
    public class TransactionPhasing
    {
        public bool Phased { get; set; }
        public int FinishHeight { get; set; }
        public VotingModel VotingModel { get; set; }
        public string Quorum { get; set; }
        public long MinBalance { get; set; }
        public ulong HoldingId { get; set; }
        public MinBalanceModel MinBalanceModel { get; set; }
        public List<string> WhiteListed { get; set; }
        public List<BinaryHexString> LinkedFullHash { get; set; }
        public BinaryHexString HashedSecret { get; set; }
        public string HashedSecretAlgorithm { get; set; }

        public void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            if (Phased)
            {
                // TODO: Continue and update with optional parameters
                queryParameters.Add("phased", Phased.ToString());
                queryParameters.Add("phasingFinishHeight", FinishHeight.ToString());
                queryParameters.Add("phasingVotingModel", ((int)VotingModel).ToString());
                queryParameters.Add("phasingQuorum", Quorum);
                queryParameters.Add("phasingMinBalance", MinBalance.ToString());
                queryParameters.Add("phasingHolding", HoldingId.ToString());

            }
        }
    }
}