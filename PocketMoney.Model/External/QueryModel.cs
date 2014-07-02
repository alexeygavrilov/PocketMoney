using PocketMoney.Data;
using PocketMoney.Model.External.Results;
using PocketMoney.Model.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketMoney.Model.External
{
    public class UserViewInQuery : ObjectBase
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string AdditionalName { get; set; }
    }

    public class GoalViewInQuery : UserViewInQuery
    {
        public Guid Id { get; set; }
        public string Details { get; set; }
        public Reward Reward { get; set; }

        public GoalView Create(IList<UserViewInQuery> assignedTo)
        {
            return new GoalView(this.Id, this.Details, this.Reward,
                assignedTo.ToDictionary(k => k.UserId, u => User.FullName(u.UserName, u.AdditionalName), EqualityComparer<Guid>.Default));
        }
    }

    public class TaskViewInQuery : GoalViewInQuery
    {
        public TaskType TaskType { get; set; }
        public int? Reminder { get; set; }
        public string RepeatName { get; set; }
        public string OneTimeName { get; set; }
        public string RoomName { get; set; }
        public string ShopName { get; set; }
        public string LessonName { get; set; }

        public TaskView Create(IList<UserViewInQuery> assignedTo)
        {
            var user = assignedTo.ToDictionary(k => k.UserId, u => User.FullName(u.UserName, u.AdditionalName), EqualityComparer<Guid>.Default);

            if (this.TaskType == TaskType.CleanTask)
            {
                return new CleanTaskView(this.RoomName, false, null, this.Id, this.TaskType, this.Details, this.Reward, this.Reminder, user);
            }
            else if (this.TaskType == TaskType.HomeworkTask)
            {
                return new HomeworkTaskView(this.LessonName, null, this.Id, this.TaskType, this.Details, this.Reward, this.Reminder, user);
            }
            else if (this.TaskType == TaskType.OneTimeTask)
            {
                return new OneTimeTaskView(this.OneTimeName, null, this.Id, this.TaskType, this.Details, this.Reward, this.Reminder, user);
            }
            else if (this.TaskType == TaskType.RepeatTask)
            {
                return new RepeatTaskView(this.RepeatName, null, this.Id, this.TaskType, this.Details, this.Reward, this.Reminder, user);
            }
            else if (this.TaskType == TaskType.ShoppingTask)
            {
                return new ShoppingTaskView(this.ShopName, null, null, this.Id, this.TaskType, this.Details, this.Reward, this.Reminder, user);
            }
            else
            {
                return null;
            }
        }
    }

}
