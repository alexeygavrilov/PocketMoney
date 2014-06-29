using PocketMoney.Data;
using PocketMoney.Model.Internal;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Results
{
    public class TaskViewFactory : ObjectBase
    {
        public Guid TaskId { get; set; }
        public TaskType TaskType { get; set; }
        public string Details { get; set; }
        public Point Points { get; set; }
        public int? Reminder { get; set; }
        public string RepeatName { get; set; }
        public string OneTimeName { get; set; }
        public string RoomName { get; set; }
        public string ShopName { get; set; }

        public TaskView Create(IDictionary<Guid, string> assignedTo)
        {
            if (this.TaskType == TaskType.CleanTask)
            {
                return new CleanTaskView(this.RoomName, false, null, this.TaskId, this.TaskType, this.Details, this.Points, this.Reminder, assignedTo);
            }
            else if (this.TaskType == TaskType.HomeworkTask)
            {
                return new HomeworkTaskView(null, this.TaskId, this.TaskType, this.Details, this.Points, this.Reminder, assignedTo);
            }
            else if (this.TaskType == TaskType.OneTimeTask)
            {
                return new OneTimeTaskView(this.OneTimeName, null, this.TaskId, this.TaskType, this.Details, this.Points, this.Reminder, assignedTo);
            }
            else if (this.TaskType == TaskType.RepeatTask)
            {
                return new RepeatTaskView(this.RepeatName, null, this.TaskId, this.TaskType, this.Details, this.Points, this.Reminder, assignedTo);
            }
            else if (this.TaskType == TaskType.ShoppingTask)
            {
                return new ShoppingTaskView(this.ShopName, null, null, this.TaskId, this.TaskType, this.Details, this.Points, this.Reminder, assignedTo);
            }
            else
            {
                return null;
            }
        }
    }

    public class TaskViewFactoryEx : TaskViewFactory
    {
        public TaskViewFactoryEx()
            : base()
        {
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string AdditionalName { get; set; }
    }
}
