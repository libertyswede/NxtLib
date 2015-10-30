using NxtLib.Internal;

namespace NxtLib.Aliases
{
    public class AliasLocator : LocatorBase
    {
        public readonly string Name;
        public readonly ulong? Id;

        private AliasLocator(string name)
            : base(Parameters.AliasName, name)
        {
            Name = name;
        }

        private AliasLocator(ulong id)
            : base(Parameters.Alias, id.ToString())
        {
            Id = id;
        }

        public static implicit operator AliasLocator(string name)
        {
            return ByName(name);
        }

        public static implicit operator AliasLocator(ulong id)
        {
            return ById(id);
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