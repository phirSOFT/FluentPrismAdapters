using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phirSOFT.FluentPrismAdapters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BeforeAttribute : Attribute
    {
        public Guid Guid { get; }

        public BeforeAttribute(Guid guid)
        {
            Guid = guid;
        }
    }
}
