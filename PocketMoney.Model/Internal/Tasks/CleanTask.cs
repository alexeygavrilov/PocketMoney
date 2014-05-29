using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class CleanTask : Task
    {
        protected CleanTask() : base() { }

        public CleanTask(string roomName, string details, int points, User creator, eDaysOfWeek daysOfWeek)
            : base(TaskType.CleanTask, details, points, creator)
        {
            this.RoomName = roomName;
            this.DaysOfWeek = daysOfWeek;
        }

        [Details]
        public virtual string RoomName { get; set; }

        [Details]
        public virtual eDaysOfWeek DaysOfWeek { get; set; }
    }
}
