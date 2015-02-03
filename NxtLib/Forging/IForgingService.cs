using System.Threading.Tasks;

namespace NxtLib.Forging
{
    public interface IForgingService
    {
        Task<GetForgingReply> GetForging(string secretPhrase);
        Task<TransactionCreated> LeaseBalance(int period, string recipient, CreateTransactionParameters parameters);
        Task<StartForgingReply> StartForging(string secretPhrase);
        Task<StopForgingReply> StopForging(string secretPhrase);
    }
}