using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class CleanTask : Task
    {
        protected CleanTask() : base() { }

        public CleanTask(string details, int points, User creator, eDaysOfWeek daysOfWeek)
            : base(TaskType.CleanTask, details, points, true, creator)
        {
            this.DaysOfWeek = daysOfWeek;
        }

        [Details]
        public virtual eDaysOfWeek DaysOfWeek { get; set; }
    }
}
