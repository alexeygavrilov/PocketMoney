using PocketMoney.Data;
using System;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External
{
    [DataContract, Serializable]
    public class ShopItem : ObjectBase
    {
        protected ShopItem() { }

        public ShopItem(int orderNumber, string name, string qty = null)
        {
            this.ItemName = name;
            this.Qty = qty;
            this.OrderNumber = orderNumber;
        }

        [DataMember, Details]
        public string ItemName { get; set; }

        [DataMember, Details]
        public string Qty { get; set; }

        [DataMember, Details]
        public int OrderNumber { get; set; }
    }
}
