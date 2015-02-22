namespace NxtLib.MonetarySystem
{
    public class CurrencyReply : Currency, IBaseReply
    {
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}