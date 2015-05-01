using System.Reflection;

namespace NxtLib.Networking
{
    public class PeersLocator : LocatorBase
    {
        public readonly bool? Active;
        public readonly PeerInfo.PeerState? State;

        private PeersLocator()
            :base("active", true.ToString())
        {
            Active = true;
        }

        private PeersLocator(PeerInfo.PeerState state, string value)
            : base("state", value)
        {
            State = state;
        }

        public static PeersLocator ByActive()
        {
            return new PeersLocator();
        }

        public static PeersLocator ByState(PeerInfo.PeerState state)
        {
            var value = state
                .GetType()
                .GetTypeInfo()
                .GetDeclaredField(state.ToString())
                .GetCustomAttribute<NxtApiAttribute>().Name;

            return new PeersLocator(state, value);
        }
    }
}