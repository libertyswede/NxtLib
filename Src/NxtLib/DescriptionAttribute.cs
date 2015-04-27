using System;

namespace NxtLib
{
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute
    {
        public string Name { get; private set; }

        internal DescriptionAttribute(string name)
        {
            Name = name;
        }
    }
}

