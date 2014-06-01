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
    public class TaskView : ObjectBase
    {
        public TaskView(Task task)
        {
            this.TaskId = task.Id;
            this.TaskType = task.Type.Id;
            this.Title = task.Title();
            this.Text = task.Details;
            this.Points = task.Points.Value;
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
        public string Title { get; set; }

        [DataMember, Details]
        public string Text { get; set; }

        [DataMember, Details]
        public IDictionary<Guid, string> AssignedTo { get; set; }

        [DataMember, Details]
        public int Points { get; set; }

        [DataMember, Details]
        public TimeSpan? ReminderTime { get; set; }

    }
}
