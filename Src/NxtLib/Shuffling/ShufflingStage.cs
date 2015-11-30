namespace NxtLib.Shuffling
{
    public enum ShufflingStage
    {
        [NxtApi("REGISTRATION")]
        Registration = 0,
        [NxtApi("PROCESSING")]
        Processing = 1,
        [NxtApi("VERIFICATION")]
        Verification = 2,
        [NxtApi("BLAME")]
        Blame = 3,
        [NxtApi("CANCELLED")]
        Cancelled = 4,
        [NxtApi("DONE")]
        Done = 5
    }
}