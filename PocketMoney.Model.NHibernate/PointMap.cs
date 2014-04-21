using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class PointMap : ComponentMap<Point>
    {
        public PointMap()
        {
            Map(x => x.Value).Column("Points").Not.Nullable();

            ParentReference(x => x.Parent);
        }
    }
}
