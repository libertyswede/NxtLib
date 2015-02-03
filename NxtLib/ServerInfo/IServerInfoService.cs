using System.Threading.Tasks;

namespace NxtLib.ServerInfo
{
    public interface IServerInfoService
    {
        Task<GetBlockchainStatusReply> GetBlockchainStatus();
        Task<GetConstantsReply> GetConstants();
        Task<GetStateReply> GetState(bool? includeCounts = null);
        Task<GetTimeReply> GetTime();
    }
}