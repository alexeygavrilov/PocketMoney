using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketMoney.Model.Internal
{
    public class TaskCount
    {
        private IList<int> _taskTypeCounts;

        public TaskCount()
        {
            this.Completed = 0;
            this.Grabbed = 0;
            _taskTypeCounts = new List<int>();
        }

        public TaskCount(IObject parent, int completed, int grabbed)
        {
            this.Parent = parent;
            this.Completed = completed;
            this.Grabbed = grabbed;
            _taskTypeCounts = new List<int>();
        }

        public void Add(TaskCount count)
        {
            this.Completed += count.Completed;
            this.Grabbed += count.Grabbed;
        }

        public void Complete(TaskType type)
        {
            this.Completed += 1;
            int definedCount = _taskTypeCounts.FirstOrDefault(x => Math.IEEERemainder(x, 100) == type.Id);
            if (definedCount == 0)
                _taskTypeCounts.Add(type.Id + 100);
            else
            {
                int index = _taskTypeCounts.IndexOf(definedCount);
                definedCount += 100;
                _taskTypeCounts[index] = definedCount;
            }
            var historyRepository = ServiceLocator.Current.GetInstance<IRepository<QueueOperation, QueueOperationId, Guid>>();
            historyRepository.Add(new QueueOperation(this));

        }

        public void Grab()
        {
            this.Grabbed += 1;
            var historyRepository = ServiceLocator.Current.GetInstance<IRepository<QueueOperation, QueueOperationId, Guid>>();
            historyRepository.Add(new QueueOperation(this));
        }


        public int Sum()
        {
            return this.Completed + this.Grabbed;
        }

        public IObject Parent { get; set; }

        [Details]
        public int Completed { get; set; }

        [Details]
        public int Grabbed { get; set; }

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

        public static TaskCount operator +(TaskCount x, TaskCount y)
        {
            return new TaskCount(x.Parent, x.Completed + y.Completed, x.Grabbed + y.Grabbed);
        }

        public static TaskCount operator -(TaskCount x, TaskCount y)
        {
            return new TaskCount(x.Parent, x.Completed - y.Completed, x.Grabbed - y.Grabbed);
        }

        public static bool operator >(TaskCount x, TaskCount y)
        {
            return x.Sum() > y.Sum();
        }

        public static bool operator >=(TaskCount x, TaskCount y)
        {
            return x.Sum() >= y.Sum();
        }

        public static bool operator <(TaskCount x, TaskCount y)
        {
            return x.Sum() < y.Sum();
        }

        public static bool operator <=(TaskCount x, TaskCount y)
        {
            return x.Sum() <= y.Sum();
        }
    }
}
