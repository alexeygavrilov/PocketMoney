using PocketMoney.Data;
using System.Collections.Generic;

namespace PocketMoney.Model.Internal
{
    public class HomeworkTask : Task
    {
        protected HomeworkTask()
            : base()
        {
            this.Dates = new List<TaskDate>();
        }

        public HomeworkTask(string details, int points, User creator, string form)
            : base(TaskType.HomeworkTask, details, points, creator)
        {
            this.Form = form;
            this.Dates = new List<TaskDate>();
        }

        [Details]
        public virtual IList<TaskDate> Dates { get; set; }

        [Details]
        public virtual string Form { get; set; }

        public override string Title()
        {
            return "Homework";
        }
    }
}
