using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Forging
{
    public class ForgingService : BaseService, IForgingService
    {
        public ForgingService(string baseAddress = Constants.DefaultNxtUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public ForgingService(IDateTimeConverter dateTimeConverter) : base(dateTimeConverter)
        {
        }

        public async Task<GetForgingReply> GetForging(SecretPhraseOrAdminPassword secretPhraseOrAdminPassword)
        {
            var queryParameters = secretPhraseOrAdminPassword.QueryParameters;
            return await Post<GetForgingReply>("getForging", queryParameters);
        }

        public async Task<TransactionCreatedReply> LeaseBalance(int period, Account recipient,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {nameof(period), period.ToString()},
                {nameof(recipient), recipient.AccountId.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("leaseBalance", queryParameters);
        }

        public async Task<StartForgingReply> StartForging(string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(secretPhrase), secretPhrase}};
            return await Post<StartForgingReply>("startForging", queryParameters);
        }

        public async Task<StopForgingReply> StopForging(SecretPhraseOrAdminPassword secretPhraseOrAdminPassword)
        {
            var queryParameters = secretPhraseOrAdminPassword.QueryParameters;
            return await Post<StopForgingReply>("stopForging", queryParameters);
        }
    }
}