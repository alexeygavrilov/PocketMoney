using PocketMoney.Data;
using System.Collections.Generic;

namespace PocketMoney.Model.Internal
{
    public class RepeatTask : Task
    {
        protected RepeatTask() : base() {
            this.Dates = new List<TaskDate>();
        }

        public RepeatTask(string name, string details, int points, User creator, string form)
            : base(TaskType.RepeatTask, details, points, creator)
        {
            this.Name = name;
            this.Form = form;
            this.Dates = new List<TaskDate>();
        }

        [Details]
        public virtual string Name { get; set; }

        [Details]
        public virtual IList<TaskDate> Dates { get; set; }

        [Details]
        public virtual string Form { get; set; }

        public override string Title()
        {
            return "Task: " + this.Name;
        }
    }
}
