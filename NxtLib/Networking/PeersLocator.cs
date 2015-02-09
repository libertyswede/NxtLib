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

        public static PeersLocator ByState(PeerInfo.PeerState state)
        {
            switch (state)
            {
                case PeerInfo.PeerState.Connected:
                    return new PeersLocator("state", "CONNECTED");
                case PeerInfo.PeerState.Disconnected:
                    return new PeersLocator("state", "DISCONNECTED");
                case PeerInfo.PeerState.NonConnected:
                    return new PeersLocator("state", "NON_CONNECTED");
            }
            throw new NotSupportedException(string.Format("State {0} is not supported type", state));
        }
    }
}