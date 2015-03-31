namespace NxtLib.Aliases
{
    public class AliasLocator : LocatorBase
    {
        public readonly string Name;
        public readonly ulong? Id;

        private AliasLocator(string name)
            : base("aliasName", name)
        {
            Name = name;
        }

        private AliasLocator(ulong id)
            : base("alias", id.ToString())
        {
            Id = id;
        }

        public static AliasLocator ByName(string name)
        {
            return new AliasLocator(name);
        }

        public static AliasLocator ById(ulong id)
        {
            return new AliasLocator(id);
        }
    }
}