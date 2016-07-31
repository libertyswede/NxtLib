using System.Threading.Tasks;

namespace NxtLib.Forging
{
    public interface IForgingService
    {
        Task<GetForgingReply> GetForging(SecretPhraseOrAdminPassword secretPhraseOrAdminPassword);

        Task<GetNextBlockGeneratorsReply> GetNextBlockGenerators(int? limit = null);

        Task<TransactionCreatedReply> LeaseBalance(int period, Account recipient, CreateTransactionParameters parameters);

        Task<StartForgingReply> StartForging(string secretPhrase);

        Task<StopForgingReply> StopForging(SecretPhraseOrAdminPassword secretPhraseOrAdminPassword);
    }
}