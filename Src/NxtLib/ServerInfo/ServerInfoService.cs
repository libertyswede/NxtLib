using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.ServerInfo
{
    public class ServerInfoService : BaseService, IServerInfoService
    {
        public ServerInfoService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public ServerInfoService(IDateTimeConverter dateTimeConverter) 
            : base(dateTimeConverter)
        {
        }

        public async Task<GetBlockchainStatusReply> GetBlockchainStatus()
        {
            return await Get<GetBlockchainStatusReply>("getBlockchainStatus");
        }

        public async Task<GetConstantsReply> GetConstants()
        {
            return await Get<GetConstantsReply>("getConstants");
        }

        public async Task<GetPluginsReply> GetPlugins()
        {
            return await Get<GetPluginsReply>("getPlugins");
        }

        public async Task<GetStateReply> GetState(bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>();
            if (includeCounts.HasValue)
            {
                queryParameters = new Dictionary<string, string> {{"includeCounts", includeCounts.Value.ToString()}};
            }
            return await Get<GetStateReply>("getState", queryParameters);
        }

        public async Task<GetTimeReply> GetTime()
        {
            return await Get<GetTimeReply>("getTime");
        }
    }
}
