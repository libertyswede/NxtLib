namespace Dividends
{
    public enum Mode
    {
        All,
        Account,
        Transaction,
        Asset
    }

    public class ProgramOptions
    {
        public Mode Mode { get; set; }
        public ulong Id { get; set; }
    }
}