using PocketMoney.Data;
using PocketMoney.Model.Internal;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class TaskListResult : ResultList<TaskView>
    {
        public TaskListResult() { }

        public TaskListResult(TaskView[] taskList, int count) : base(taskList, count) { }
    }

    [DataContract]
    public abstract class TaskView : RewardView
    {
        protected TaskView(Guid taskId, TaskType type, string text, Reward reward, int? reminderTime, IDictionary<Guid, string> assignedTo) : base(reward)
        {
            this.TaskId = taskId;
            this.TaskType = type.Id;
            this.Text = text;
            this.ReminderTime = reminderTime.HasValue ? new TimeSpan?(TimeSpan.FromMinutes(reminderTime.Value)) : null;
            this.AssignedTo = assignedTo;
        }

        protected TaskView(Task task) : base(task.Reward)
        {
            this.TaskId = task.Id;
            this.TaskType = task.Type.Id;
            this.Text = task.Details;
            this.ReminderTime = task.Reminder.HasValue ? new TimeSpan?(TimeSpan.FromMinutes(task.Reminder.Value)) : null;
            this.AssignedTo = task.AssignedTo
                .Where(p => p.Active)
                .ToDictionary(k => k.User.Id, e => e.User.FullName(), EqualityComparer<Guid>.Default);
        }

        [DataMember, Details]
        public Guid TaskId { get; set; }

        [DataMember, Details]
        public int TaskType { get; set; }

        [DataMember, Details]
        public string Title { get { return this.GetTitle(); } }

        [DataMember, Details]
        public string Text { get; set; }

        [DataMember, Details]
        public IDictionary<Guid, string> AssignedTo { get; set; }

        [DataMember, Details]
        public TimeSpan? ReminderTime { get; set; }

        public string Responsibility
        {
            get
            {
                return string.Join(", ", this.AssignedTo.Select(x => x.Value).ToArray());
            }
        }

        public abstract string GetTitle();
    }
}
