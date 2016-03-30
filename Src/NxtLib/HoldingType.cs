namespace NxtLib
{
    public enum HoldingType
    {
        [NxtApi("NXT")]
        Nxt = 0,
        [NxtApi("ASSET")]
        Asset = 1,
        [NxtApi("CURRENCY")]
        Currency = 2
    }
}