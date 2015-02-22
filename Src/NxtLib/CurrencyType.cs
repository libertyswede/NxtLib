using System.ComponentModel.DataAnnotations;

namespace NxtLib
{
    public enum CurrencyType
    {
        [Display(Description = "EXCHANGEABLE")]
        Exchangeable = 0x01,
        [Display(Description = "CONTROLLABLE")]
        Controllable = 0x02,
        [Display(Description = "RESERVABLE")]
        Reservable = 0x04,
        [Display(Description = "CLAIMABLE")]
        Claimable = 0x08,
        [Display(Description = "MINTABLE")]
        Mintable = 0x10,
        [Display(Description = "NON_SHUFFLEABLE")]
        NonShuffleable = 0x20
    }
}