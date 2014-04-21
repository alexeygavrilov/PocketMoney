using System;
using System.Linq;

namespace PocketMoney.Model.Internal
{
    public class TaskType : System.Object, System.IEquatable<TaskType>
    {
        public static TaskType Empty = new TaskType(0, string.Empty); //, new TaskItemType[0]);

        protected TaskType()
        {
        }

        public TaskType(int id)
        {
            this.Id = id;
        }

        public TaskType(int id, string name) // TaskItemType[] itemType)
        {
            _id = id;
            this.Name = name;
//            this.ItemType = itemType;
        }

        private int _id;

        public virtual int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                if (_id != TaskType.Empty.Id)
                {
                    var tt = TaskType.All.FirstOrDefault(x => x.Id == value);
                    if (tt == null)
                        throw new ArgumentException("Unknown Task Type");
                    this.Name = tt.Name;
                    //this.ItemType = tt.ItemType;
                }
                else
                    this.Name = string.Empty;
            }
        }

        //public TaskItemType[] ItemType { get; set; }

        public virtual string Name { get; private set; }

        public static TaskType OneTimeTask = new TaskType(1, "Onetime Task"); //, new TaskItemType[1] { TaskItemType.Person });

        public static TaskType[] All = new TaskType[1] { TaskType.OneTimeTask };

        public bool Equals(TaskType other)
        {
            if (other != null)
                return this.Id.Equals(other.Id);
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is TaskType)
                return ((TaskType)obj).Id.Equals(this.Id);
            else
                return false;
        }

        public static bool operator ==(TaskType a, TaskType b)
        {
            if (a == (object)null && b == (object)null)
                return true;
            if (a == (object)null || b == (object)null)
                return false;
            var ta = a as TaskType;
            var tb = b as TaskType;
            if (ta != (object)null && tb != (object)null)
                return ta.Id.Equals(b.Id);
            else
                return false;
        }

        public static bool operator !=(TaskType a, TaskType b)
        {
            if (a == (object)null && b == (object)null)
                return false;
            if (a == (object)null || b == (object)null)
                return true;
            var ta = a as TaskType;
            var tb = b as TaskType;
            if (ta != (object)null && tb != (object)null)
                return !ta.Id.Equals(b.Id);
            else
                return true;
        }

        public override int GetHashCode()
        {
            return _id;
        }

    }
}
