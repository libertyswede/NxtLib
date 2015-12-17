using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.VotingSystem;

namespace NxtLib.AccountControl
{
    public interface IAccountControlService
    {
        Task<object> GetAllPhasingOnlyControls(int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<object> GetPhasingOnlyControl(Account account, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TransactionCreatedReply> SetPhasingOnlyControl(VotingModel controlVotingModel, long controlQuorum, CreateTransactionParameters parameters, long? controlMinBalance = null, VotingModel? controlMinBalanceModel = null, ulong? controlHolding = null, IEnumerable<string> controlWhitelisted = null, Amount controlMaxFees = null, int? controlMinDuration = null, int? controlMaxDuration = null);
    }
}