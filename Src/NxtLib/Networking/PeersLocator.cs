using System;
using System.Linq;
using System.Reflection;
using NxtLib.Internal;

namespace NxtLib.Networking
{
    public class PeersLocator : LocatorBase
    {
        public readonly bool? Active;
        public readonly PeerInfo.PeerState? State;

        private PeersLocator()
            :base(Parameters.Active, true.ToString())
        {
            Active = true;
        }

        private PeersLocator(PeerInfo.PeerState state, string value)
            : base(Parameters.State, value)
        {
            State = state;
        }

        public static PeersLocator ByActive()
        {
            return new PeersLocator();
        }

        public static PeersLocator ByState(PeerInfo.PeerState state)
        {
            var type = typeof(PeerInfo.PeerState);
            var name = Enum.GetName(type, state);
            var value = type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<NxtApiAttribute>()
                .SingleOrDefault().Name;

            return new PeersLocator(state, value);
        }
    }
}