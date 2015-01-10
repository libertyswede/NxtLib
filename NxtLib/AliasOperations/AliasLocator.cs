namespace NxtLib.AliasOperations
{
    public class AliasLocator : LocatorBase
    {
        private AliasLocator(string key, string value) : base(key, value)
        {
        }

        public static AliasLocator ByName(string name)
        {
            return new AliasLocator("aliasName", name);
        }

        public static AliasLocator ById(ulong id)
        {
            return new AliasLocator("alias", id.ToString());
        }
    }
}