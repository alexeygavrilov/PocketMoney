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

        public HomeworkTask(string details, Reward reward, User creator, string lesson, string form)
            : base(TaskType.HomeworkTask, details, reward, creator)
        {
            this.Form = form;
            this.Lesson = lesson;
            this.Dates = new List<TaskDate>();
        }

        [Details]
        public virtual IList<TaskDate> Dates { get; set; }

        [Details]
        public virtual string Lesson { get; set; }

        [Details]
        public virtual string Form { get; set; }

    }
}
