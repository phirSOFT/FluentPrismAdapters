using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace phirSOFT.FluentPrismAdapters
{
    public class ControlMarker
    {
        private static readonly ConditionalWeakTable<Control, object> Markers = new ConditionalWeakTable<Control, object>();

        public static Guid GetMarker(Control control)
        {
            if (Markers.TryGetValue(control, out var guid))
                return (Guid)guid;
            return Guid.Empty;
        }

        public static void SetMarker(Control control, Guid value)
        {
            Markers.Remove(control);
            Markers.Add(control, value);
        }
    }
}
