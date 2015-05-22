using System.Threading.Tasks;

namespace NxtLib.ServerInfo
{
    public interface IServerInfoService
    {
        Task<EventRegisterReply> EventRegister(NxtEvent? nxtEvent, bool? add = null, bool? remove = null);
        Task<EventWaitReply> EventWait(long timeout);
        Task<GetBlockchainStatusReply> GetBlockchainStatus();
        Task<GetConstantsReply> GetConstants();
        Task<GetPluginsReply> GetPlugins();
        Task<GetStateReply> GetState(bool? includeCounts = null);
        Task<GetTimeReply> GetTime();
    }
}