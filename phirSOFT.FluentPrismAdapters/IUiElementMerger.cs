using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace phirSOFT.FluentPrismAdapters
{
    interface IUiElementMerger<in T> where T : UIElement
    {
        void Merge(T target, T other);

        void Split(T target, T other);
    }
}
