using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Local;
using NxtLib.VotingSystem;

namespace NxtLib.AccountControl
{
    // TODO: Pull members up to interface
    public class AccountControlService : BaseService, IAccountControlService
    {
        public AccountControlService(string baseAddress = Constants.DefaultNxtUrl)
            : base(baseAddress)
        {
        }
        
        public Task<object> GetAllPhasingOnlyControls(int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetPhasingOnlyControl(Account account, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            throw new NotImplementedException();
        }

        // TODO: Check that parameters are of correct type and what is required and what is optional
        public Task<object> SetPhasingOnlyControl(VotingModel controlVotingModel, long controlQuorum,
            long controlMinBalance, VotingModel controlMinBalanceModel, IEnumerable<string> controlWhitelisted,
            Amount controlMaxFees, int controlMinDuration, int controlMaxDuration,
            CreateTransactionParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
