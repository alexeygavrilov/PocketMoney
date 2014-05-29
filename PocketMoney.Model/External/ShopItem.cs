using PocketMoney.Data;
using System;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External
{
    [DataContract, Serializable]
    public class ShopItem : ObjectBase
    {
        protected ShopItem() { }

        public ShopItem(string name, string qty = null)
        {
            this.ItemName = name;
            this.Qty = qty;
        }

        [DataMember, Details]
        public string ItemName { get; set; }

        [DataMember, Details]
        public string Qty { get; set; }
    }
}
