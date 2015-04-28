using System.Diagnostics;

namespace NxtLib
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum CurrencyType
    {
        [NxtApi("EXCHANGEABLE")]
        Exchangeable = 0x01,
        [NxtApi("CONTROLLABLE")]
        Controllable = 0x02,
        [NxtApi("RESERVABLE")]
        Reservable = 0x04,
        [NxtApi("CLAIMABLE")]
        Claimable = 0x08,
        [NxtApi("MINTABLE")]
        Mintable = 0x10,
        [NxtApi("NON_SHUFFLEABLE")]
        NonShuffleable = 0x20
    }
}