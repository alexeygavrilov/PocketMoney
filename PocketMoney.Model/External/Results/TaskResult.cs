using PocketMoney.Data;
using PocketMoney.Model.Internal;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class CleanTaskResult : ResultData<CleanTaskView>
    {
        public CleanTaskResult() { }

        public CleanTaskResult(String errorMessage)
            : base(errorMessage)
        {
        }

        public CleanTaskResult(CleanTaskView data) : base(data) { }
    }

    [DataContract]
    public class CleanTaskView : TaskView
    {
        public CleanTaskView(CleanTask task)
            : base(task)
        {
            this.RoomName = task.RoomName;
            this.EveryDay = task.DaysOfWeek != eDaysOfWeek.None;
            IList<int> days = new List<int>();
            foreach (var d in EnumExtention.GetAllItems<eDaysOfWeek>())
            {
                if (task.DaysOfWeek.HasFlag(d))
                    days.Add((int)d);
            }
            this.DaysOfWeek = days.ToArray();
        }

        [DataMember, Details]
        public string RoomName { get; set; }

        [DataMember, Details]
        public bool EveryDay { get; set; }

        [DataMember, Details]
        public int[] DaysOfWeek { get; set; }
    }

    [DataContract]
    public class HomeworkTaskResult : ResultData<HomeworkTaskView>
    {
        public HomeworkTaskResult() { }

        public HomeworkTaskResult(String errorMessage)
            : base(errorMessage)
        {
        }
        public HomeworkTaskResult(HomeworkTaskView data) : base(data) { }
    }

    [DataContract]
    public class HomeworkTaskView : TaskView
    {
        public HomeworkTaskView(HomeworkTask task)
            : base(task)
        {
            this.Form = (HomeworkForm)BinarySerializer.Deserialaize(Convert.FromBase64String(task.Form), typeof(HomeworkForm));
        }

        [DataMember, Details]
        public HomeworkForm Form { get; set; }
    }

    [DataContract]
    public class OneTimeTaskResult : ResultData<OneTimeTaskView>
    {
        public OneTimeTaskResult() { }

        public OneTimeTaskResult(String errorMessage)
            : base(errorMessage)
        {
        }

        public OneTimeTaskResult(OneTimeTaskView data) : base(data) { }
    }

    [DataContract]
    public class OneTimeTaskView : TaskView
    {
        public OneTimeTaskView(OneTimeTask task)
            : base(task)
        {
            this.Name = task.Name;
            this.DeadlineDate = task.DeadlineDate;
        }

        [DataMember, Details]
        public string Name { get; set; }

        [DataMember, Details]
        public DateTime? DeadlineDate { get; set; }
    }

    [DataContract]
    public class RepeatTaskResult : ResultData<RepeatTaskView>
    {
        public RepeatTaskResult() { }

        public RepeatTaskResult(String errorMessage)
            : base(errorMessage)
        {
        }

        public RepeatTaskResult(RepeatTaskView data) : base(data) { }
    }

    [DataContract]
    public class RepeatTaskView : TaskView
    {
        public RepeatTaskView(RepeatTask task)
            : base(task)
        {
            this.Name = task.Name;
            this.Form = (RepeatForm)BinarySerializer.Deserialaize(Convert.FromBase64String(task.Form), typeof(RepeatForm));
        }

        [DataMember, Details]
        public string Name { get; set; }

        [DataMember, Details]
        public RepeatForm Form { get; set; }
    }

    [DataContract]
    public class ShoppingTaskResult : ResultData<ShoppingTaskView>
    {
        public ShoppingTaskResult() { }

        public ShoppingTaskResult(String errorMessage)
            : base(errorMessage)
        {
        }

        public ShoppingTaskResult(ShoppingTaskView data) : base(data) { }
    }

    [DataContract]
    public class ShoppingTaskView : TaskView
    {
        public ShoppingTaskView(ShopTask task)
            : base(task)
        {
            this.ShopName = task.ShopName;
            this.DeadlineDate = task.DeadlineDate;
            this.ShoppingList = task.ShoppingList.Select(x => new External.ShopItem(x.Name, x.Qty)).ToArray();
        }

        [DataMember, Details]
        public string ShopName { get; set; }

        [DataMember, Details]
        public DateTime? DeadlineDate { get; set; }

        [DataMember, Details]
        public ShopItem[] ShoppingList { get; set; }
    }
}
