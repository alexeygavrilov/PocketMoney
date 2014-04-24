using PocketMoney.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            : base(TaskType.HomeworkTask, details, points, true, creator)
        {
            this.Form = form;
            this.Dates = new List<TaskDate>();
        }

        [Details]
        public virtual IList<TaskDate> Dates { get; set; }

        [Details]
        public virtual string Form { get; set; }
        
    }
}
