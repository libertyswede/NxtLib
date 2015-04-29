using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.ServerInfo
{
    public interface IServerInfoService
    {
        Task<object> EventRegister(List<string> events, string add, string remove);
        Task<object> EventWait(long timeout);
        Task<GetBlockchainStatusReply> GetBlockchainStatus();
        Task<GetConstantsReply> GetConstants();
        Task<GetPluginsReply> GetPlugins();
        Task<GetStateReply> GetState(bool? includeCounts = null);
        Task<GetTimeReply> GetTime();
    }
}