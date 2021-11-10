using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Fluent;
using phirSOFT.FluentPrismAdapters.Annotations;
using Prism.Ioc;
using Prism.Regions;

namespace phirSOFT.FluentPrismAdapters.RegionBehaviors
{
    internal class ContextualTabGroupBehavior : RegionBehavior
    {
        private readonly IContainerExtension _containerExtension;
        private readonly Ribbon _ribbon;
        private readonly Dictionary<ContextualTabGroupAttribute, TabGroupContext> _activeContextualGroups = new Dictionary<ContextualTabGroupAttribute, TabGroupContext>(ContextualTabGroupEqualityComparer.Instance);

        private readonly ConditionalWeakTable<RibbonTabItem, ContextualTabGroupAttribute?> _attributesCache =
            new ConditionalWeakTable<RibbonTabItem, ContextualTabGroupAttribute?>();

        public ContextualTabGroupBehavior(IContainerExtension containerExtension, Ribbon ribbon)
        {
            _containerExtension = containerExtension;
            _ribbon = ribbon;
        }

        protected override void OnAttach()
        {
            Region.Views.CollectionChanged += ViewsChanged;
        }

        private void ViewsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                AddContextualGroups(e.NewItems!.OfType<RibbonTabItem>());
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                RemoveContextualGroups(e.OldItems!.OfType<RibbonTabItem>());
            }

        }

        private void RemoveContextualGroups(IEnumerable<RibbonTabItem> oldItems)
        {
            foreach (RibbonTabItem tabItem in oldItems)
            {
                if(!_attributesCache.TryGetValue(tabItem, out ContextualTabGroupAttribute? attribute) || attribute is null)
                {
                    continue;
                }

                if (!_activeContextualGroups.TryGetValue(attribute, out TabGroupContext context))
                {
                    continue;
                }

                tabItem.Group = null;
                context.ActiveItems.Remove(tabItem);
                context.ContextualTabGroup.Visibility = context.ActiveItems.Any()
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }
        }

        private void AddContextualGroups(IEnumerable<RibbonTabItem> newItems)
        {
            static ContextualTabGroupAttribute? GetAttribute(RibbonTabItem tabItem)
            {
                return tabItem.GetType().GetCustomAttribute<ContextualTabGroupAttribute>();
            }
            foreach (RibbonTabItem tabItem in newItems)
            {
                if (!(_attributesCache.GetValue(tabItem, GetAttribute) is {} attribute))
                {
                    continue;
                }
              

                if (!_activeContextualGroups.TryGetValue(attribute, out TabGroupContext context))
                {
                    RibbonContextualTabGroup tabGroup =
                        (RibbonContextualTabGroup)_containerExtension.Resolve(attribute.ContextualTabGroupType, attribute.ResolveName);

                    context = new TabGroupContext(tabGroup);
                    _ribbon.ContextualGroups.Add(context.ContextualTabGroup);
                    _activeContextualGroups.Add(attribute, context);
                }

                tabItem.Group = context.ContextualTabGroup;
                context.ContextualTabGroup.Visibility = Visibility.Visible;
                context.ActiveItems.Add(tabItem);

            }
        }

        private readonly struct TabGroupContext
        {
            public TabGroupContext(RibbonContextualTabGroup contextualTabGroup)
            {
                ContextualTabGroup = contextualTabGroup;
                ActiveItems = new HashSet<RibbonTabItem>();
            }

            public RibbonContextualTabGroup ContextualTabGroup { get; }

            public HashSet<RibbonTabItem> ActiveItems { get; }
        }

        private class ContextualTabGroupEqualityComparer : IEqualityComparer<ContextualTabGroupAttribute>
        {
            public static readonly ContextualTabGroupEqualityComparer Instance = new ContextualTabGroupEqualityComparer();
            public bool Equals(ContextualTabGroupAttribute x, ContextualTabGroupAttribute y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                if (x.GetType() != y.GetType())
                {
                    return false;
                }

                return x.ContextualTabGroupType == y.ContextualTabGroupType && x.ResolveName == y.ResolveName;
            }

            public int GetHashCode(ContextualTabGroupAttribute obj)
            {
#if NETCOREAPP || NET5_0
                return HashCode.Combine(obj.ContextualTabGroupType, obj.ResolveName);
#else
                return (7573 * obj.ContextualTabGroupType.GetHashCode()) ^ (obj.ResolveName?.GetHashCode() ?? 0);
#endif
            }
        }
    }
}
