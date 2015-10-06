using System.Threading.Tasks;

namespace NxtLib.ServerInfo
{
    public interface IServerInfoService
    {
        Task<EventRegisterReply> EventRegister(NxtEvent? nxtEvent = null, bool? add = null, bool? remove = null);

        Task<EventWaitReply> EventWait(long timeout);

        Task<GetBlockchainStatusReply> GetBlockchainStatus();

        Task<GetConstantsReply> GetConstants();

        Task<GetPluginsReply> GetPlugins();

        Task<GetStateReply> GetState(bool? includeCounts = null, string adminPassword = null);

        Task<GetTimeReply> GetTime();
    }
}