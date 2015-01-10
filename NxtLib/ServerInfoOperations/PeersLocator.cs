using System;

namespace NxtLib.ServerInfoOperations
{
    public class PeersLocator : LocatorBase
    {
        private PeersLocator(string key, string value) : base(key, value)
        {
        }

        public static PeersLocator ByActive()
        {
            return new PeersLocator("active", true.ToString());
        }

        public static PeersLocator ByState(GetPeerReply.PeerState state)
        {
            switch (state)
            {
                case GetPeerReply.PeerState.Connected:
                    return new PeersLocator("state", "CONNECTED");
                case GetPeerReply.PeerState.Disconnected:
                    return new PeersLocator("state", "DISCONNECTED");
                case GetPeerReply.PeerState.NonConnected:
                    return new PeersLocator("state", "NON_CONNECTED");
            }
            throw new NotSupportedException(string.Format("State {0} is not supported type", state));
        }
    }
}