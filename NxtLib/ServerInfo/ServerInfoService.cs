using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.ServerInfo
{
    public interface IServerInfoService
    {
        Task<PeerReply> AddPeer(string peer);
        Task<DoneReply> BlacklistPeer(string peer);
        Task<GetBlockchainStatusReply> GetBlockchainStatus();
        Task<GetConstantsReply> GetConstants();
        Task<GetMyInfoReply> GetMyInfo();
        Task<PeerReply> GetPeer(string peer);
        Task<GetPeersReply> GetPeers(PeersLocator locator = null);
        Task<GetStateReply> GetState(bool? includeCounts = null);
        Task<GetTimeReply> GetTime();
    }

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

        public async Task<PeerReply> AddPeer(string peer)
        {
            var queryParameters = new Dictionary<string, string> {{"peer", peer}};
            return await Get<PeerReply>("addPeer", queryParameters);
        }

        public async Task<DoneReply> BlacklistPeer(string peer)
        {
            var queryParameters = new Dictionary<string, string> {{"peer", peer}};
            return await Get<DoneReply>("blacklistPeer", queryParameters);
        }

        public async Task<GetBlockchainStatusReply> GetBlockchainStatus()
        {
            return await Get<GetBlockchainStatusReply>("getBlockchainStatus");
        }

        public async Task<GetConstantsReply> GetConstants()
        {
            return await Get<GetConstantsReply>("getConstants");
        }

        public async Task<GetMyInfoReply> GetMyInfo()
        {
            return await Get<GetMyInfoReply>("getMyInfo");
        }

        public async Task<PeerReply> GetPeer(string peer)
        {
            var queryParameters = new Dictionary<string, string> {{"peer", peer}};
            return await Get<PeerReply>("getPeer", queryParameters);
        }

        public async Task<GetPeersReply> GetPeers(PeersLocator locator = null)
        {
            if (locator != null)
            {
                return await Get<GetPeersReply>("getPeers", locator.QueryParameters);
            }
            return await Get<GetPeersReply>("getPeers");
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
