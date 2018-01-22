using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Markup;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.FluentPrismAdapters
{
    public class RelativePositionContraint : ITopologicalComparer
    {
        private static readonly SortedDictionary<Guid, SortedSet<Guid>> RelativeContraints = new SortedDictionary<Guid, SortedSet<Guid>>();

        public static IAddChild GetElementsBefore(Control control)
        {
            return new BeforeWrapper(control);
        }

        public static IAddChild GetElementsAfter(Control control)
        {
            var guid = ControlMarker.GetMarker(control);

            if (!RelativeContraints.ContainsKey(guid))
                RelativeContraints.Add(guid, new SortedSet<Guid>());

            return new AfterWrapper(RelativeContraints[guid]);
        }

        public int Compare(object x, object y)
        {
            var xGuid = ControlMarker.GetMarker(x as Control);
            var yGuid = ControlMarker.GetMarker(y as Control);

            if (xGuid == yGuid)
                return 0;

            if (RelativeContraints.ContainsKey(xGuid))
                return -1;
            return 1;
        }

        public bool CanCompare(object x, object y)
        {
            var xGuid = ControlMarker.GetMarker(x as Control);
            var yGuid = ControlMarker.GetMarker(y as Control);

            if (xGuid == Guid.Empty || yGuid == Guid.Empty)
                return false;

            if (xGuid == yGuid)
                return true;


            return (RelativeContraints.TryGetValue(xGuid, out var greaterX) && greaterX.Contains(yGuid))
                   || (RelativeContraints.TryGetValue(yGuid, out var greaterY) && greaterY.Contains(xGuid));
        }

        private class BeforeWrapper : IAddChild
        {
            private readonly Guid _control;

            public BeforeWrapper(Control control)
            {
                _control = ControlMarker.GetMarker(control);
            }

            public void AddChild(object value)
            {
                var guid = ControlMarker.GetMarker(value as Control);
                if (guid != Guid.Empty)
                {
                    if (!RelativeContraints.ContainsKey(guid))
                        RelativeContraints.Add(guid, new SortedSet<Guid>());
                    RelativeContraints[guid].Add(_control);
                }
            }

            public void AddText(string text)
            {
                throw new NotImplementedException();
            }
        }

        private class AfterWrapper : IAddChild
        {
            private readonly ISet<Guid> _set;


            public AfterWrapper(ISet<Guid> set)
            {
                _set = set;
            }

            public void AddChild(object value)
            {
                var guid = ControlMarker.GetMarker(value as Control);
                if (guid != Guid.Empty)
                {
                    _set.Add(guid);
                }
            }

            public void AddText(string text)
            {
                throw new NotImplementedException();
            }
        }
    }


}
