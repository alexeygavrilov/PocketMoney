using FluentNHibernate;
using FluentNHibernate.Mapping;
using PocketMoney.Data;

namespace PocketMoney.Data.NHibernate.Mappings
{
   
    public class UserComponentMap : ComponentMap<IUser>
    {
        public UserComponentMap()
        {
            Map(x => x.Id).Column("UserId").Index("IX_UserId");
        }
    }
}
