using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;
using NxtLib.VotingSystem;

namespace NxtLib.AccountControl
{
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

        public async Task<TransactionCreatedReply> SetPhasingOnlyControl(VotingModel controlVotingModel, long controlQuorum,
            CreateTransactionParameters parameters, long? controlMinBalance = null,
            VotingModel? controlMinBalanceModel = null, ulong? controlHolding = null, IEnumerable<string> controlWhitelisted = null,
            Amount controlMaxFees = null, int? controlMinDuration = null, int? controlMaxDuration = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {Parameters.ControlVotingModel, new List<string> {((int) controlVotingModel).ToString()}},
                {Parameters.ControlQuorum, new List<string> {controlQuorum.ToString()}}
            };
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.AddIfHasValue(Parameters.ControlMinBalance, controlMinBalance);
            queryParameters.AddIfHasValue(Parameters.ControlMinBalanceModel,
                controlMinBalanceModel != null ? (int) controlMinBalanceModel : (int?) null);

            if (controlWhitelisted != null)
            {
                queryParameters.Add(Parameters.ControlWhitelisted, controlWhitelisted.ToList());
            }
            queryParameters.AddIfHasValue(Parameters.ControlMaxFees, controlMaxFees?.Nqt.ToString());
            queryParameters.AddIfHasValue(Parameters.ControlMinDuration, controlMinDuration);
            queryParameters.AddIfHasValue(Parameters.ControlMaxDuration, controlMaxDuration);
            return await Post<TransactionCreatedReply>("setPhasingOnlyControl", queryParameters);
        }
    }
}
