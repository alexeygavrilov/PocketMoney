using PocketMoney.Data.NHibernate;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class ShopItemMap : VersionedClassMap<ShopItem>
    {
        public ShopItemMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.Name).Not.Nullable().Length(255);
            Map(x => x.Qty).Nullable().Length(50);

            References<ShopTask>(x => x.Task).Column("ShopTaskId").Not.Nullable().ForeignKey("FK_Task_ShopItem");

        }
    }
}
