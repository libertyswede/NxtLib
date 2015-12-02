namespace NxtLib.Shuffling
{
    public enum ShufflingParticipantState
    {
        [NxtApi("REGISTERED")]
        Registered = 0,
        [NxtApi("PROCESSED")]
        Processed = 1,
        [NxtApi("VERIFIED")]
        Verified = 2,
        [NxtApi("CANCELLED")]
        Cancelled = 3
    }
}