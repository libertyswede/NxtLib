using System;

namespace NxtLib.Networking
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

        public static PeersLocator ByState(PeerReply.PeerState state)
        {
            switch (state)
            {
                case PeerReply.PeerState.Connected:
                    return new PeersLocator("state", "CONNECTED");
                case PeerReply.PeerState.Disconnected:
                    return new PeersLocator("state", "DISCONNECTED");
                case PeerReply.PeerState.NonConnected:
                    return new PeersLocator("state", "NON_CONNECTED");
            }
            throw new NotSupportedException(string.Format("State {0} is not supported type", state));
        }
    }
}