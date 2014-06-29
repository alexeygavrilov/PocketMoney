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
    public abstract class TaskView : ObjectBase
    {
        public const string TITLE_FORMAT = "Task: {0}";

        protected TaskView(Guid taskId, TaskType type, string text, Point points, int? reminderTime, IDictionary<Guid, string> assignedTo)
        {
            this.TaskId = taskId;
            this.TaskType = type.Id;
            this.Text = text;
            this.Points = points.Value;
            this.ReminderTime = reminderTime.HasValue ? new TimeSpan?(TimeSpan.FromMinutes(reminderTime.Value)) : null;
            this.AssignedTo = assignedTo;
        }

        protected TaskView(Task task)
        {
            this.TaskId = task.Id;
            this.TaskType = task.Type.Id;
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
        public string Title { get { return this.GetTitle(); } }

        [DataMember, Details]
        public string Text { get; set; }

        [DataMember, Details]
        public IDictionary<Guid, string> AssignedTo { get; set; }

        [DataMember, Details]
        public int Points { get; set; }

        [DataMember, Details]
        public TimeSpan? ReminderTime { get; set; }

        public abstract string GetTitle();
    }
}
