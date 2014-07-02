using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{

    public class RewardMap : ComponentMap<Reward>
    {
        public RewardMap()
        {
            Map(x => x.Points).Not.Nullable();
            Map(x => x.Gift).Nullable().Length(500);

            ParentReference(x => x.Parent);
        }
    }

    public class PointMap : ComponentMap<Point>
    {
        public PointMap()
        {
            Map(x => x.Points).Not.Nullable();

            ParentReference(x => x.Parent);
        }
    }
}
