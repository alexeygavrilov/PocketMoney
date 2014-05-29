using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class ShopTaskMap : SubclassMap<ShopTask>
    {
        public ShopTaskMap()
        {
            Map(x => x.ShopName).Nullable().Length(255);
            Map(x => x.DeadlineDate).Nullable();

            HasMany(x => x.ShoppingList).Not.LazyLoad().ForeignKeyConstraintName("FK_Task_ShopItem");
        }
    }
}
