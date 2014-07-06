using PocketMoney.Data;
using System;
using System.Collections.Generic;

namespace PocketMoney.Model.Internal
{
    public class ShopTask : Task
    {
        protected ShopTask()
            : base()
        {
            this.ShoppingList = new List<ShopItem>();
        }

        public ShopTask(string shop, string details, Reward reward, DateTime? deadlineDate, User creator)
            : base(TaskType.ShoppingTask, details, reward, creator)
        {
            this.ShopName = shop;
            this.DeadlineDate = deadlineDate;
            this.ShoppingList = new List<ShopItem>();
        }

        [Details]
        public virtual string ShopName { get; set; }

        [Details]
        public virtual DateTime? DeadlineDate { get; set; }

        [Details]
        public virtual IList<ShopItem> ShoppingList { get; set; }
        
        [Details]
        public override string Name
        {
            get { return FormatTitle(this.ShopName); }
        }

        public static string FormatTitle(string shopName)
        {
            if (string.IsNullOrEmpty(shopName))
                return "Shopping";
            else
                return "Shopping at " + shopName;

        }
    }
}
