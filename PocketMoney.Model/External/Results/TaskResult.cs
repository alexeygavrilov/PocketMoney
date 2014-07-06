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
    public sealed class CleanTaskView : TaskView
    {
        public CleanTaskView(string roomName, bool everyDay, int[] daysOfWeek, Guid taskId, TaskType type, string text, Reward reward, int? reminderTime, IDictionary<Guid, string> assignedTo)
            : base(taskId, type, text, reward, reminderTime, assignedTo)
        {
            this.RoomName = roomName;
            this.EveryDay = everyDay;
            this.DaysOfWeek = daysOfWeek;
        }

        public CleanTaskView(CleanTask task)
            : base(task)
        {
            this.RoomName = task.RoomName;
            this.EveryDay = task.DaysOfWeek == eDaysOfWeek.None;
            IList<int> days = new List<int>();
            foreach (var d in EnumExtention.GetAllItems<eDaysOfWeek>())
            {
                if (task.DaysOfWeek.HasFlag(d) && d != eDaysOfWeek.None)
                    days.Add((int)d.To());
            }
            this.DaysOfWeek = days.ToArray();
        }

        [DataMember, Details]
        public string RoomName { get; set; }

        [DataMember, Details]
        public bool EveryDay { get; set; }

        [DataMember, Details]
        public int[] DaysOfWeek { get; set; }

        public override string GetTitle()
        {
            return CleanTask.FormatTitle(this.RoomName);
        }
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
    public sealed class HomeworkTaskView : TaskView
    {
        public HomeworkTaskView(string lesson, HomeworkForm form, Guid taskId, TaskType type, string text, Reward reward, int? reminderTime, IDictionary<Guid, string> assignedTo)
            : base(taskId, type, text, reward, reminderTime, assignedTo)
        {
            this.Lesson = lesson;
            this.Form = form;
        }

        public HomeworkTaskView(HomeworkTask task)
            : base(task)
        {
            this.Lesson = task.Lesson;
            this.Form = (HomeworkForm)BinarySerializer.Deserialaize(Convert.FromBase64String(task.Form), typeof(HomeworkForm));
        }

        [DataMember, Details]
        public string Lesson { get; set; }

        [DataMember, Details]
        public HomeworkForm Form { get; set; }

        public override string GetTitle()
        {
            return HomeworkTask.FormatTitle(this.Lesson);
        }
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
    public sealed class OneTimeTaskView : TaskView
    {
        public OneTimeTaskView(string name, DateTime? deadline, Guid taskId, TaskType type, string text, Reward reward, int? reminderTime, IDictionary<Guid, string> assignedTo)
            : base(taskId, type, text, reward, reminderTime, assignedTo)
        {
            this.Name = name;
            this.DeadlineDate = deadline;
        }

        public OneTimeTaskView(OneTimeTask task)
            : base(task)
        {
            this.Name = task.OneTimeName;
            this.DeadlineDate = task.DeadlineDate;
        }

        [DataMember, Details]
        public string Name { get; set; }

        [DataMember, Details]
        public DateTime? DeadlineDate { get; set; }

        public override string GetTitle()
        {
            return OneTimeTask.FormatTitle(this.Name);
        }
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
    public sealed class RepeatTaskView : TaskView
    {
        public RepeatTaskView(string name, RepeatForm form, Guid taskId, TaskType type, string text, Reward reward, int? reminderTime, IDictionary<Guid, string> assignedTo)
            : base(taskId, type, text, reward, reminderTime, assignedTo)
        {
            this.Name = name;
            this.Form = form;
        }

        public RepeatTaskView(RepeatTask task)
            : base(task)
        {
            this.Name = task.RepeatName;
            this.Form = (RepeatForm)BinarySerializer.Deserialaize(Convert.FromBase64String(task.Form), typeof(RepeatForm));
        }

        [DataMember, Details]
        public string Name { get; set; }

        [DataMember, Details]
        public RepeatForm Form { get; set; }

        public override string GetTitle()
        {
            return RepeatTask.FormatTitle(this.Name);
        }
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
    public sealed class ShoppingTaskView : TaskView
    {
        public ShoppingTaskView(string shopName, DateTime? deadline, ShopItem[] shoppingList, Guid taskId, TaskType type, string text, Reward reward, int? reminderTime, IDictionary<Guid, string> assignedTo)
            : base(taskId, type, text, reward, reminderTime, assignedTo)
        {
            this.ShopName = shopName;
            this.DeadlineDate = deadline;
            this.ShoppingList = shoppingList;
        }

        public ShoppingTaskView(ShopTask task)
            : base(task)
        {
            this.ShopName = task.ShopName;
            this.DeadlineDate = task.DeadlineDate;
            this.ShoppingList = task.ShoppingList.Select(x => new External.ShopItem(x.OrderNumber, x.Name, x.Qty)).ToArray();
        }

        [DataMember, Details]
        public string ShopName { get; set; }

        [DataMember, Details]
        public DateTime? DeadlineDate { get; set; }

        [DataMember, Details]
        public ShopItem[] ShoppingList { get; set; }

        public override string GetTitle()
        {
            return ShopTask.FormatTitle(this.ShopName);
        }


    }
}
