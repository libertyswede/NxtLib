using System;

namespace NxtLib.Internal
{
    [AttributeUsage(AttributeTargets.All)]
    internal class DescriptionAttribute : Attribute
    {
        internal string Name { get; private set; }

        internal DescriptionAttribute(string name)
        {
            Name = name;
        }
    }
}

