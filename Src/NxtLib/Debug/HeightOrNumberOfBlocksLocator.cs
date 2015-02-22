namespace NxtLib.Debug
{
    public class HeightOrNumberOfBlocksLocator : LocatorBase
    {
        private HeightOrNumberOfBlocksLocator(string key, string value) : base(key, value)
        {
        }

        public static HeightOrNumberOfBlocksLocator ByHeight(int height)
        {
            return new HeightOrNumberOfBlocksLocator("height", height.ToString());
        }

        public static HeightOrNumberOfBlocksLocator ByNumBlocks(int numBlocks)
        {
            return new HeightOrNumberOfBlocksLocator("numBlocks", numBlocks.ToString());
        }
    }
}