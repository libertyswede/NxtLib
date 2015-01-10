using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.ForgingOperations
{
    public interface IForgingService
    {
        Task<GetForging> GetForging(string secretPhrase);
        Task<TransactionCreated> LeaseBalance(int period, string recipient, CreateTransactionParameters parameters);
        Task<StartForging> StartForging(string secretPhrase);
        Task<StopForging> StopForging(string secretPhrase);
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

        public async Task<GetForging> GetForging(string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string> {{"secretPhrase", secretPhrase}};
            return await Post<GetForging>("getForging", queryParameters);
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

        public async Task<StartForging> StartForging(string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string> {{"secretPhrase", secretPhrase}};
            return await Post<StartForging>("startForging", queryParameters);
        }

        public async Task<StopForging> StopForging(string secretPhrase)
        {
            var queryParameters = new Dictionary<string, string> {{"secretPhrase", secretPhrase}};
            return await Post<StopForging>("stopForging", queryParameters);
        }
    }
}
