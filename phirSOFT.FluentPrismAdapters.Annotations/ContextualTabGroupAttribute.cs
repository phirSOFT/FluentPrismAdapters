using System;
using System.Collections.Generic;
using System.Text;

namespace phirSOFT.FluentPrismAdapters.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ContextualTabGroupAttribute : Attribute
    {
        public Type ContextualTabGroupType { get; }
        public string? ResolveName { get; }

        public ContextualTabGroupAttribute(Type contextualTabGroupType, string? resolveName = null)
        {
            ContextualTabGroupType = contextualTabGroupType;
            ResolveName = resolveName;
        }
    }
}
