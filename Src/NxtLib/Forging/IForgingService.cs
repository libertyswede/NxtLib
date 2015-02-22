using System.Threading.Tasks;

namespace NxtLib.Forging
{
    public interface IForgingService
    {
        Task<GetForgingReply> GetForging(SecretPhraseOrAdminPassword secretPhraseOrAdminPassword);
        Task<TransactionCreatedReply> LeaseBalance(int period, string recipient, CreateTransactionParameters parameters);
        Task<StartForgingReply> StartForging(string secretPhrase);
        Task<StopForgingReply> StopForging(SecretPhraseOrAdminPassword secretPhraseOrAdminPassword);
    }
}