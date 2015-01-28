using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.ForgingOperations
{
    public interface IForgingService
    {
        Task<GetForgingReply> GetForging(string secretPhrase);
        Task<TransactionCreated> LeaseBalance(int period, string recipient, CreateTransactionParameters parameters);
        Task<StartForgingReply> StartForging(string secretPhrase);
        Task<StopForgingReply> StopForging(string secretPhrase);
    }

    public class ForgingService : BaseService, IForgingService
    {
        public ForgingService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public ForgingService(IDateTimeConverter dateTimeConverter) : base(dateTimeConverter)
        {
        }

        public async Task<GetForgingReply> GetForging(string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string> {{"secretPhrase", secretPhrase}};
            return await Post<GetForgingReply>("getForging", queryParameters);
        }

        public async Task<TransactionCreated> LeaseBalance(int period, string recipient, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"period", period.ToString()},
                {"recipient", recipient}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("leaseBalance", queryParameters);
        }

        public async Task<StartForgingReply> StartForging(string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string> {{"secretPhrase", secretPhrase}};
            return await Post<StartForgingReply>("startForging", queryParameters);
        }

        public async Task<StopForgingReply> StopForging(string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string> {{"secretPhrase", secretPhrase}};
            return await Post<StopForgingReply>("stopForging", queryParameters);
        }
    }
}
