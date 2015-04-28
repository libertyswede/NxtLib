using System;

namespace NxtLib
{
    [AttributeUsage(AttributeTargets.All)]
    public class NxtApiAttribute : Attribute
    {
        public string Name { get; private set; }

        internal NxtApiAttribute(string name)
        {
            Name = name;
        }
    }
}

