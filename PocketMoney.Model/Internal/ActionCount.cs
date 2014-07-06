using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketMoney.Model.Internal
{
    public class ActionCount : ObjectBase
    {
        private IList<int> _taskTypeCounts;

        public ActionCount()
        {
            this.GoodWorks = 0;
            this.CompletedGoals = 0;
            this.CompletedTasks = 0;
            this.GrabbedTasks = 0;
            _taskTypeCounts = new List<int>();
        }

        public ActionCount(IObject parent)
            : this()
        {
            this.Parent = parent;
        }

        public ActionCount(IObject parent, int completedTasks, int grabbedTasks, int completedGoals, int goodWorks)
            : this(parent)
        {
            this.CompletedTasks = completedTasks;
            this.GrabbedTasks = grabbedTasks;
            this.CompletedGoals = CompletedGoals;
            this.GoodWorks = goodWorks;
        }


        public void Add(ActionCount count)
        {
            this.CompletedTasks += count.CompletedTasks;
            this.GrabbedTasks += count.GrabbedTasks;
            this.CompletedGoals += count.CompletedGoals;
            this.GoodWorks += count.GoodWorks;
        }

        public void CompleteTask(TaskType type, Action<ActionCount> addLog)
        {
            if (type == TaskType.Goal)
                return;

            this.CompletedTasks += 1;
            int definedCount = _taskTypeCounts.FirstOrDefault(x => Math.IEEERemainder(x, 100) == type.Id);
            if (definedCount == 0)
                _taskTypeCounts.Add(type.Id + 100);
            else
            {
                int index = _taskTypeCounts.IndexOf(definedCount);
                definedCount += 100;
                _taskTypeCounts[index] = definedCount;
            }
            addLog(this);
        }

        public void CompleteGoal(Action<ActionCount> addLog)
        {
            this.CompletedGoals += 1;
            int definedCount = _taskTypeCounts.FirstOrDefault(x => Math.IEEERemainder(x, 100) == TaskType.Goal.Id);
            if (definedCount == 0)
                _taskTypeCounts.Add(TaskType.Goal.Id + 100);
            else
            {
                int index = _taskTypeCounts.IndexOf(definedCount);
                definedCount += 100;
                _taskTypeCounts[index] = definedCount;
            }
            addLog(this);
        }

        public void GrabTask(Action<ActionCount> addLog)
        {
            this.GrabbedTasks += 1;
            addLog(this);
        }

        public void GoodDeed(Action<ActionCount> addLog)
        {
            this.GoodWorks += 1;
            addLog(this);
        }

        public int Sum()
        {
            return this.CompletedTasks + this.GrabbedTasks + this.GoodWorks + this.CompletedGoals;
        }

        [Details]
        public IObject Parent { get; set; }

        [Details]
        public int CompletedTasks { get; set; }

        [Details]
        public int GrabbedTasks { get; set; }

        [Details]
        public int CompletedGoals { get; set; }

        [Details]
        public int GoodWorks { get; set; }

        public IDictionary<TaskType, int> DefinedTaskCounts
        {
            get
            {
                IDictionary<TaskType, int> result = new Dictionary<TaskType, int>();
                foreach (var definedCount in _taskTypeCounts)
                {
                    int typeIndex;
                    int count = Math.DivRem(definedCount, 100, out typeIndex);
                    result.Add(new TaskType(typeIndex), count);
                }
                return result;
            }
        }

        public static ActionCount operator +(ActionCount x, ActionCount y)
        {
            return new ActionCount(x.Parent, x.CompletedTasks + y.CompletedTasks, x.GrabbedTasks + y.GrabbedTasks, x.CompletedGoals + y.CompletedGoals, x.GoodWorks + y.GoodWorks);
        }

        public static ActionCount operator -(ActionCount x, ActionCount y)
        {
            return new ActionCount(x.Parent, x.CompletedTasks - y.CompletedTasks, x.GrabbedTasks - y.GrabbedTasks, x.CompletedGoals - y.CompletedGoals, x.GoodWorks - y.GoodWorks);
        }

        public static bool operator >(ActionCount x, ActionCount y)
        {
            return x.Sum() > y.Sum();
        }

        public static bool operator >=(ActionCount x, ActionCount y)
        {
            return x.Sum() >= y.Sum();
        }

        public static bool operator <(ActionCount x, ActionCount y)
        {
            return x.Sum() < y.Sum();
        }

        public static bool operator <=(ActionCount x, ActionCount y)
        {
            return x.Sum() <= y.Sum();
        }
    }
}
