using System;
using NxtLib.Internal;

namespace NxtLib.Blocks
{
    public class BlockLocator : LocatorBase
    {
        private BlockLocator(string key, string value) : base(key, value)
        {
        }

        public static BlockLocator BlockId(ulong blockId)
        {
            return new BlockLocator("block", blockId.ToString());
        }

        public static BlockLocator Height(int height)
        {
            return new BlockLocator("height", height.ToString());
        }

        public static BlockLocator Timestamp(DateTime timestamp)
        {
            var nxtTimestamp = new DateTimeConverter().GetEpochTime(timestamp);
            return new BlockLocator("timestamp", nxtTimestamp.ToString());
        }
    }
}