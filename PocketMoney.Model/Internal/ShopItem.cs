using PocketMoney.Data;
using System;

namespace PocketMoney.Model.Internal
{
    public class ShopItem : Entity<ShopItem, ShopItemId, Guid> 
    {
        protected ShopItem() { }

        public ShopItem(ShopTask task, string name, string qty)
        {
            this.Task = task;
            this.Name = name;
            this.Qty = qty;
        }

        [Details]
        public virtual string Name { get; set; }

        [Details]
        public virtual string Qty { get; set; }

        [Details]
        public virtual ShopTask Task { get; set; }
    }


    [Serializable]
    public class ShopItemId : GuidIdentity
    {
        public ShopItemId()
            : base(Guid.Empty, typeof(ShopItem))
        {
        }

        public ShopItemId(string id)
            : base(id, typeof(ShopItem))
        {
        }

        public ShopItemId(Guid emailId)
            : base(emailId, typeof(ShopItem))
        {
        }
    }
}
