using System.Threading.Tasks;

namespace NxtLib.Networking
{
    public interface INetworkingService
    {
        Task<PeerReply> AddPeer(string peer);
        Task<DoneReply> BlacklistPeer(string peer);
        Task<GetMyInfoReply> GetMyInfo();
        Task<PeerReply> GetPeer(string peer);
        Task<GetPeersReply> GetPeers(PeersLocator locator = null);
    }
}