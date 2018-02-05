using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Markup;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.FluentPrismAdapters
{
    public class RelativePositionContraint : ITopologicalComparer
    {
        private static readonly SortedDictionary<Guid, SortedSet<Guid>> RelativeContraints = new SortedDictionary<Guid, SortedSet<Guid>>();
        private static readonly ConditionalWeakTable<object, object> Guids = new ConditionalWeakTable<object, object>();

        public static Guid GetGuid(object target)
        {
            return Guids.TryGetValue(target, out var guid) ? (Guid) guid : Guid.Empty;
        }

        public static void SetGuid(object target, Guid value)
        {
            Guids.Remove(target);
            Guids.Add(target, value);
        }

        private Guid ResolveGuid(object x)
        {
            if (Guids.TryGetValue(x, out var guid))
                return (Guid) guid;

            var type = x.GetType();
            var att = type.GetCustomAttribute<GuidAttribute>();
            if (att == null)
            {
                return Guid.Empty;
            }

            guid = new Guid(type.GetCustomAttribute<GuidAttribute>().Value);

            foreach (var beforeAttribute in type.GetCustomAttributes<BeforeAttribute>())
            {
                if(!RelativeContraints.ContainsKey(beforeAttribute.Guid))
                    RelativeContraints.Add(beforeAttribute.Guid, new SortedSet<Guid>());
                RelativeContraints[beforeAttribute.Guid].Add((Guid) guid);
            }

            if (!RelativeContraints.ContainsKey((Guid) guid)) 
                RelativeContraints.Add((Guid)guid, new SortedSet<Guid>());

            foreach (var afterAttribute in type.GetCustomAttributes<AfterAttribute>())
            {
                RelativeContraints[(Guid) guid].Add(afterAttribute.Guid);
            }

            return (Guid)guid;
        }

        public int Compare(object x, object y)
        {
            var xGuid = ResolveGuid(x);
            var yGuid = ResolveGuid(y);

            if (xGuid == yGuid)
                return 0;

            if (RelativeContraints.ContainsKey(xGuid))
                return -1;
            return 1;
        }

        public bool CanCompare(object x, object y)
        {
            var xGuid = ResolveGuid(x);
            var yGuid = ResolveGuid(y);

            if (xGuid == Guid.Empty || yGuid == Guid.Empty)
                return false;

            if (xGuid == yGuid)
                return true;


            return (RelativeContraints.TryGetValue(xGuid, out var greaterX) && greaterX.Contains(yGuid))
                   || (RelativeContraints.TryGetValue(yGuid, out var greaterY) && greaterY.Contains(xGuid));
        }
    }


}
