using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public abstract class Task : Entity<Task, TaskId, Guid>
    {

    }

    public class TaskId : GuidIdentity
    {
        public TaskId(Guid taskId)
            : base(taskId, typeof(Task))
        {
        }

        public TaskId()
            : base(Guid.Empty, typeof(Task))
        {
        }
    }

}
