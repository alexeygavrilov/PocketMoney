using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class OneTimeTaskMap : SubclassMap<OneTimeTask>
    {
        public OneTimeTaskMap()
        {
            Map(x => x.Name).Not.Nullable().Length(255);
            Map(x => x.DeadlineDate).Nullable();
        }
    }
}
