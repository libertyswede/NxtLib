using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Networking
{
    public class NetworkingService : BaseService, INetworkingService
    {
        public NetworkingService(string baseAddress = Constants.DefaultNxtUrl)
            : base(baseAddress)
        {
        }

        public async Task<PeerReply> AddPeer(string peer)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Peer, peer}};
            return await Get<PeerReply>("addPeer", queryParameters);
        }

        public async Task<DoneReply> BlacklistPeer(string peer)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Peer, peer}};
            return await Get<DoneReply>("blacklistPeer", queryParameters);
        }

        public async Task<GetPeersReply> GetInboundPeers()
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.IncludePeerInfo, false.ToString()}};
            return await Get<GetPeersReply>("getInboundPeers", queryParameters);
        }

        public async Task<GetPeersIncludeInfoReply> GetInboundPeersIncludeInfo()
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.IncludePeerInfo, true.ToString()}};
            return await Get<GetPeersIncludeInfoReply>("getInboundPeers", queryParameters);
        }

        public async Task<GetMyInfoReply> GetMyInfo()
        {
            return await Get<GetMyInfoReply>("getMyInfo");
        }

        public async Task<PeerReply> GetPeer(string peer)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Peer, peer}};
            return await Get<PeerReply>("getPeer", queryParameters);
        }

        public async Task<GetPeersReply> GetPeers(PeersLocator locator = null, string service = null)
        {
            var queryParameters = locator != null ? locator.QueryParameters : new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Service, service);
            return await Get<GetPeersReply>("getPeers", queryParameters);
        }

        public async Task<GetPeersIncludeInfoReply> GetPeersIncludeInfo(PeersLocator locator = null,
            string service = null)
        {
            var queryParameters = locator != null ? locator.QueryParameters : new Dictionary<string, string>();
            queryParameters.Add(Parameters.IncludePeerInfo, true.ToString());
            queryParameters.AddIfHasValue(Parameters.Service, service);
            return await Get<GetPeersIncludeInfoReply>("getPeers", queryParameters);
        }

        public async Task<PeerReply> SetAPIProxyPeer(string peer)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Peer, peer}};
            return await Get<PeerReply>("setAPIProxyPeer", queryParameters);
        }
    }
}