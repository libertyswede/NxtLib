using System.Collections.Generic;
using NxtLib.Internal;
using NxtLib.VotingSystem;

namespace NxtLib
{
    public class CreateTransactionPhasing
    {
        public bool Phased { get; set; }
        public int FinishHeight { get; set; }
        public BinaryHexString HashedSecret { get; set; }
        public HashAlgorithm? HashedSecretAlgorithm { get; set; }
        public ulong HoldingId { get; set; } = 0;
        public List<BinaryHexString> LinkedFullHash { get; set; }
        public long MinBalance { get; set; } = 0;
        public MinBalanceModel MinBalanceModel { get; set; } = MinBalanceModel.None;
        public long Quorum { get; set; }
        public VotingModel VotingModel { get; set; }
        public List<Account> WhiteListed { get; set; }

        public CreateTransactionPhasing(int finishHeight, VotingModel votingModel, long quorum)
        {
            Phased = true;
            FinishHeight = finishHeight;
            VotingModel = votingModel;
            Quorum = quorum;
            WhiteListed = new List<Account>();
            LinkedFullHash = new List<BinaryHexString>();
        }

        public void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            if (Phased)
            {
                queryParameters.Add(Parameters.Phased, Phased.ToString());
                queryParameters.Add(Parameters.PhasingFinishHeight, FinishHeight.ToString());
                queryParameters.Add(Parameters.PhasingVotingModel, ((int)VotingModel).ToString());
                queryParameters.Add(Parameters.PhasingQuorum, Quorum.ToString());

                if (MinBalance > 0)
                {
                    queryParameters.Add(Parameters.PhasingMinBalance, MinBalance.ToString());
                }
                if (HoldingId > 0)
                {
                    queryParameters.Add(Parameters.PhasingHolding, HoldingId.ToString());
                }
                if (MinBalanceModel != MinBalanceModel.None)
                {
                    queryParameters.Add(Parameters.PhasingMinBalanceModel, ((int)MinBalanceModel).ToString());
                }
                WhiteListed.ForEach(w => queryParameters.Add(Parameters.PhasingWhitelisted, w.AccountRs));
                LinkedFullHash.ForEach(h => queryParameters.Add(Parameters.PhasingLinkedFullHash, h.ToHexString()));
                if (HashedSecret != null)
                {
                    queryParameters.Add(Parameters.PhasingHashedSecret, HashedSecret.ToHexString());
                }
                if (HashedSecretAlgorithm.HasValue)
                {
                    queryParameters.Add(Parameters.PhasingHashedSecretAlgorithm, ((int)HashedSecretAlgorithm.Value).ToString());
                }
            }
        }
    }
}