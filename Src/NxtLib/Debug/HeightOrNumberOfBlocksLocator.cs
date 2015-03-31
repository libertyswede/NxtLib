namespace NxtLib.Debug
{
    public class HeightOrNumberOfBlocksLocator : LocatorBase
    {
        public readonly int? Height;
        public readonly int? NumBlocks;

        private HeightOrNumberOfBlocksLocator(string key, int value)
            : base(key, value.ToString())
        {
            if (key.Equals("height"))
            {
                Height = value;
            }
            else if (key.Equals("numBlocks"))
            {
                NumBlocks = value;
            }
        }

        public static HeightOrNumberOfBlocksLocator ByHeight(int height)
        {
            return new HeightOrNumberOfBlocksLocator("height", height);
        }

        public static HeightOrNumberOfBlocksLocator ByNumBlocks(int numBlocks)
        {
            return new HeightOrNumberOfBlocksLocator("numBlocks", numBlocks);
        }
    }
}