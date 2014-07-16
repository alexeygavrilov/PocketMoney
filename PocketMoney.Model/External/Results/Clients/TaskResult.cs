using PocketMoney.Data;
using PocketMoney.Model.Internal;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Results.Clients
{
    [DataContract]
    public class GoalView : ObjectBase
    {
        public GoalView(Goal goal)
        {
            this.Id = goal.Id;
            this.Text = goal.Details;
            this.Reward = goal.Reward.ToString();
        }

        protected GoalView(Guid id, string text, Reward reward)
        {
            this.Id = id;
            this.Text = text;
            this.Reward = reward.ToString();
        }

        [DataMember, Details]
        public Guid Id { get; set; }

        [DataMember, Details]
        public string Text { get; set; }

        [DataMember, Details]
        public string Reward { get; set; }
    }

    [DataContract]
    public abstract class TaskView : GoalView
    {
        protected TaskView(Guid taskId, TaskType type, string text, Reward reward, eDateType dateType)
            : base(taskId, text, reward)
        {
            this.TaskType = type.Id;
            this.DateType = dateType;
        }

        [DataMember, Details]
        public int TaskType { get; set; }

        [DataMember, Details]
        public eDateType DateType { get; set; }

        public abstract string Title { get; }
    }

    [DataContract]
    public class CleanTaskView : TaskView
    {
        public CleanTaskView(string roomName, Guid taskId, string text, Reward reward, eDateType dateType)
            : base(taskId, Internal.TaskType.CleanTask, text, reward, dateType)
        {
            this.RoomName = roomName;
        }

        [DataMember, Details]
        public string RoomName { get; set; }

        [DataMember, Details]
        public override string Title
        {
            get
            {
                return CleanTask.FormatTitle(this.RoomName);
            }
        }
    }

    [DataContract]
    public class HomeworkTaskView : TaskView
    {
        public HomeworkTaskView(string lesson, Guid taskId, string text, Reward reward, eDateType dateType)
            : base(taskId, Internal.TaskType.HomeworkTask, text, reward, dateType)
        {
            this.Lesson = lesson;
        }

        [DataMember, Details]
        public string Lesson { get; set; }

        [DataMember, Details]
        public override string Title
        {
            get
            {
                return HomeworkTask.FormatTitle(this.Lesson);
            }
        }
    }

    [DataContract]
    public class OneTimeTaskView : TaskView
    {
        public OneTimeTaskView(string name, Guid taskId, string text, Reward reward, eDateType dateType)
            : base(taskId, Internal.TaskType.OneTimeTask, text, reward, dateType)
        {
            this.Name = name;
        }

        [DataMember, Details]
        public string Name { get; set; }

        [DataMember, Details]
        public override string Title
        {
            get
            {
                return OneTimeTask.FormatTitle(this.Name);
            }
        }
    }

    [DataContract]
    public class RepeatTaskView : TaskView
    {
        public RepeatTaskView(string name, Guid taskId, string text, Reward reward, eDateType dateType)
            : base(taskId, Internal.TaskType.RepeatTask, text, reward, dateType)
        {
            this.Name = name;
        }

        [DataMember, Details]
        public string Name { get; set; }

        [DataMember, Details]
        public override string Title
        {
            get
            {
                return RepeatTask.FormatTitle(this.Name);
            }
        }
    }

    public class ShoppingTaskView : TaskView
    {
        public ShoppingTaskView(string shopName, Guid taskId, string text, Reward reward, eDateType dateType)
            : base(taskId, Internal.TaskType.ShoppingTask, text, reward, dateType)
        {
            this.ShopName = shopName;
        }

        [DataMember, Details]
        public string ShopName { get; set; }

        [DataMember, Details]
        public override string Title
        {
            get
            {
                return ShopTask.FormatTitle(this.ShopName);
            }
        }
    }

    [DataContract]
    public class TaskListResult : ResultList<TaskView>
    {
        public TaskListResult() { }

        public TaskListResult(TaskView[] taskList, int count) : base(taskList, count) { }
    }

    [DataContract]
    public class GoalListResult : ResultList<GoalView>
    {
        public GoalListResult() { }

        public GoalListResult(GoalView[] goalList, int count) : base(goalList, count) { }
    }

    public class TaskViewInQuery : ObjectBase
    {
        public Guid Id { get; set; }
        public string Desctiption { get; set; }
        public Reward Reward { get; set; }
        public TaskType TaskType { get; set; }
        public bool HasDates { get; set; }

        // clean
        public string Clean_RoomName { get; set; }
        public eDaysOfWeek Clean_DaysOfWeek { get; set; }

        // shop
        public string Shop_Name { get; set; }
        public DateTime? Shop_DeadlineDate { get; set; }

        // repeat
        public string Repeat_Name { get; set; }

        // single
        public string OneTime_Name { get; set; }
        public DateTime? OneTime_DeadlineDate { get; set; }

        // homework
        public string Homework_LessonName { get; set; }

        public TaskView[] Create(Func<DayOfOne, bool> actionExists, Func<DayOfOne, bool> dateExists, CurrentDates dates)
        {
            IList<TaskView> result = new List<TaskView>();

            if (this.TaskType == TaskType.CleanTask)
            {
                if ((this.Clean_DaysOfWeek == eDaysOfWeek.None || this.Clean_DaysOfWeek.HasFlag(dates.YesterdayDate.DayOfWeek.To()))
                    && !actionExists(dates.Yesterday))
                {
                    result.Add(new CleanTaskView(this.Clean_RoomName, this.Id, this.Desctiption, this.Reward, eDateType.Yesterday));
                }
                if ((this.Clean_DaysOfWeek == eDaysOfWeek.None || this.Clean_DaysOfWeek.HasFlag(dates.TodayDate.DayOfWeek.To()))
                    && !actionExists(dates.Today))
                {
                    result.Add(new CleanTaskView(this.Clean_RoomName, this.Id, this.Desctiption, this.Reward, eDateType.Today));
                }
                if ((this.Clean_DaysOfWeek == eDaysOfWeek.None || this.Clean_DaysOfWeek.HasFlag(dates.TomorrowDate.DayOfWeek.To()))
                    && !actionExists(dates.Tomorrow))
                {
                    result.Add(new CleanTaskView(this.Clean_RoomName, this.Id, this.Desctiption, this.Reward, eDateType.Tomorrow));
                }
            }
            else if (this.TaskType == TaskType.HomeworkTask)
            {
                if (dateExists(dates.Yesterday) && !actionExists(dates.Yesterday))
                {
                    result.Add(new HomeworkTaskView(this.Homework_LessonName, this.Id, this.Desctiption, this.Reward, eDateType.Yesterday));
                }
                if (dateExists(dates.Today) && !actionExists(dates.Today))
                {
                    result.Add(new HomeworkTaskView(this.Homework_LessonName, this.Id, this.Desctiption, this.Reward, eDateType.Today));
                }
                if (dateExists(dates.Tomorrow) && !actionExists(dates.Tomorrow))
                {
                    result.Add(new HomeworkTaskView(this.Homework_LessonName, this.Id, this.Desctiption, this.Reward, eDateType.Tomorrow));
                }
            }
            else if (this.TaskType == TaskType.OneTimeTask)
            {
                if ((!this.OneTime_DeadlineDate.HasValue || this.OneTime_DeadlineDate.Value >= dates.YesterdayDate) && !actionExists(dates.Yesterday))
                {
                    result.Add(new OneTimeTaskView(this.OneTime_Name, this.Id, this.Desctiption, this.Reward, eDateType.Yesterday));
                }
                if ((!this.OneTime_DeadlineDate.HasValue || this.OneTime_DeadlineDate.Value >= dates.TodayDate) && !actionExists(dates.Today))
                {
                    result.Add(new OneTimeTaskView(this.OneTime_Name, this.Id, this.Desctiption, this.Reward, eDateType.Today));
                }
                if ((!this.OneTime_DeadlineDate.HasValue || this.OneTime_DeadlineDate.Value >= dates.TomorrowDate) && !actionExists(dates.Tomorrow))
                {
                    result.Add(new OneTimeTaskView(this.OneTime_Name, this.Id, this.Desctiption, this.Reward, eDateType.Tomorrow));
                }
            }
            else if (this.TaskType == TaskType.RepeatTask)
            {
                if (dateExists(dates.Yesterday) && !actionExists(dates.Yesterday))
                {
                    result.Add(new RepeatTaskView(this.Repeat_Name, this.Id, this.Desctiption, this.Reward, eDateType.Yesterday));
                }
                if (dateExists(dates.Today) && !actionExists(dates.Today))
                {
                    result.Add(new RepeatTaskView(this.Repeat_Name, this.Id, this.Desctiption, this.Reward, eDateType.Today));
                }
                if (dateExists(dates.Tomorrow) && !actionExists(dates.Tomorrow))
                {
                    result.Add(new RepeatTaskView(this.Repeat_Name, this.Id, this.Desctiption, this.Reward, eDateType.Tomorrow));
                }
            }
            else if (this.TaskType == TaskType.ShoppingTask)
            {
                if ((!this.Shop_DeadlineDate.HasValue || this.Shop_DeadlineDate.Value >= dates.YesterdayDate) && !actionExists(dates.Yesterday))
                {
                    result.Add(new ShoppingTaskView(this.Shop_Name, this.Id, this.Desctiption, this.Reward, eDateType.Yesterday));
                }
                if ((!this.Shop_DeadlineDate.HasValue || this.Shop_DeadlineDate.Value >= dates.TodayDate) && !actionExists(dates.Today))
                {
                    result.Add(new ShoppingTaskView(this.Shop_Name, this.Id, this.Desctiption, this.Reward, eDateType.Today));
                }
                if ((!this.Shop_DeadlineDate.HasValue || this.Shop_DeadlineDate.Value >= dates.TomorrowDate) && !actionExists(dates.Tomorrow))
                {
                    result.Add(new ShoppingTaskView(this.Shop_Name, this.Id, this.Desctiption, this.Reward, eDateType.Tomorrow));
                }
            }
            return result.ToArray();
        }
    }
}
