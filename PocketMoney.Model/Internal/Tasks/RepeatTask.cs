using PocketMoney.Data;
using System.Collections.Generic;

namespace PocketMoney.Model.Internal
{
    public class RepeatTask : Task
    {
        protected RepeatTask()
            : base()
        {
            this.Dates = new List<TaskDate>();
        }

        public RepeatTask(string name, string details, Reward reward, User creator, string form)
            : base(TaskType.RepeatTask, details, reward, creator)
        {
            this.RepeatName = name;
            this.Form = form;
            this.Dates = new List<TaskDate>();
        }

        [Details]
        public virtual string RepeatName { get; set; }

        [Details]
        public virtual IList<TaskDate> Dates { get; set; }

        [Details]
        public virtual string Form { get; set; }

        [Details]
        public override string Name
        {
            get { return FormatTitle(this.RepeatName); }
        }

        public static string FormatTitle(string name)
        {
            return string.Format(TITLE_FORMAT, name);
        }

    }
}
