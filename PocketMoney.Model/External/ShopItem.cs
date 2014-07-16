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

        public ShopItem(Internal.ShopItem item)
        {
            this.ItemName = item.Name;
            this.Qty = item.Qty;
            this.OrderNumber = item.OrderNumber;
            this.Processed = item.Processed;
        }

        [DataMember, Details]
        public string ItemName { get; set; }

        [DataMember, Details]
        public string Qty { get; set; }

        [DataMember, Details]
        public int OrderNumber { get; set; }

        [DataMember, Details]
        public bool Processed { get; set; }
    }
}

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class ShopItemResult : ResultData<ShopItem>
    {
        public ShopItemResult() { }

        public ShopItemResult(ShopItem item) : base(item) { }
    }

    [DataContract]
    public class ShopItemListResult : ResultList<ShopItem>
    {
        public ShopItemListResult() { }

        public ShopItemListResult(ShopItem[] shoppingList, int count) : base(shoppingList, count) { }
    }

}

