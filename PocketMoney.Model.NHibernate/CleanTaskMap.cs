using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class CleanTaskMap : SubclassMap<CleanTask>
    {
        public CleanTaskMap()
        {
            Map(x => x.DaysOfWeek).Not.Nullable();
        }
    }
}
