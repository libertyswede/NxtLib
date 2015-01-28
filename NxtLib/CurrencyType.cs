using System.ComponentModel;

namespace NxtLib
{
    public enum CurrencyType
    {
        [Description("EXCHANGEABLE")]
        Exchangeable = 0x01,
        [Description("CONTROLLABLE")]
        Controllable = 0x02,
        [Description("RESERVABLE")]
        Reservable = 0x04,
        [Description("CLAIMABLE")]
        Claimable = 0x08,
        [Description("MINTABLE")]
        Mintable = 0x10,
        [Description("NON_SHUFFLEABLE")]
        NonShuffleable = 0x20
    }
}