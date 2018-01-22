using System;

namespace phirSOFT.FluentPrismAdapters
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = true)]
    public class AfterAttribute : Attribute
    {
        public Guid Guid { get; }

        public AfterAttribute(Guid guid)
        {
            Guid = guid;
        }
    }
}