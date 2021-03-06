﻿using NxtLib.Internal;

namespace NxtLib.Debug
{
    public class HeightOrNumberOfBlocksLocator : LocatorBase
    {
        public readonly int? Height;
        public readonly int? NumBlocks;

        private HeightOrNumberOfBlocksLocator(string key, int value)
            : base(key, value.ToString())
        {
            if (key.Equals(Parameters.Height))
            {
                Height = value;
            }
            else if (key.Equals(Parameters.NumBlocks))
            {
                NumBlocks = value;
            }
        }

        public static HeightOrNumberOfBlocksLocator ByHeight(int height)
        {
            return new HeightOrNumberOfBlocksLocator(Parameters.Height, height);
        }

        public static HeightOrNumberOfBlocksLocator ByNumBlocks(int numBlocks)
        {
            return new HeightOrNumberOfBlocksLocator(Parameters.NumBlocks, numBlocks);
        }
    }
}