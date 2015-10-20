using System;

namespace AssetActivity
{
    internal class ProgramOptions
    {
        public ulong AssetId { get; set; }
        public Range Range { get; set; }
        public DateTime StartDate { get; set; }
    }

    internal enum Range
    {
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Undefined
    }
}