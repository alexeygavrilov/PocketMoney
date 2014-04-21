using FluentNHibernate.Mapping;
using NHibernate.Type;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class QueueOperationMap : ClassMap<QueueOperation>
    {
        public QueueOperationMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.ObjectId).Not.Nullable().Index("IX_QueueOperation");
            Map(x => x.ObjectType).Not.Nullable().Index("IX_QueueOperation");
            Map(x => x.ValueType).Not.Nullable().Index("IX_QueueOperation");
            Map(x => x.ChangeValue).Not.Nullable().Index("IX_QueueOperation");
            Map(x => x.ChangeDate).Not.Nullable().CustomType<UtcDateTimeType>().Index("IX_QueueOperation");
            Map(x => x.Processed).Not.Nullable();

        }
    }
}
