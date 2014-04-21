using PocketMoney.Data;
using PocketMoney.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketMoney.Model.Internal
{
    public class QueueOperation : Entity<QueueOperation, QueueOperationId, Guid>
    {
        protected QueueOperation()
        {
            this.Processed = false;
        }

        public QueueOperation(Guid objectId, eObjectType objectType, QueueOperationValueType valueType, int value)
        {
            this.ObjectId = objectId;
            this.ObjectType = objectType;
            this.ValueType = valueType;
            this.ChangeValue = value;
            this.ChangeDate = Clock.UtcNow();
            this.Processed = true;
        }

        public QueueOperation(Point point, int value)
            : this()
        {
            this.ObjectId = point.Parent.Id;
            this.ObjectType = point.Parent.ObjectType;
            this.ValueType = QueueOperationValueType.Point;
            this.ChangeValue = value;
            this.ChangeDate = Clock.UtcNow();
            if (point.Parent.ObjectType != eObjectType.User)
                this.Processed = true;
        }
        public QueueOperation(TaskCount count)
            : this()
        {
            this.ObjectId = count.Parent.Id;
            this.ObjectType = count.Parent.ObjectType;
            this.ValueType = QueueOperationValueType.TaskCount;
            this.ChangeValue = 1;
            this.ChangeDate = Clock.UtcNow();
            if (count.Parent.ObjectType != eObjectType.User)
                this.Processed = true;
        }

        public virtual Guid ObjectId { get; set; }
        public virtual eObjectType ObjectType { get; set; }
        public virtual QueueOperationValueType ValueType { get; set; }
        public virtual int ChangeValue { get; set; }
        public virtual DateTime ChangeDate { get; set; }
        public virtual bool Processed { get; set; }
    }

    public enum QueueOperationSourceType
    {
        User,
        Family
    }

    public enum QueueOperationValueType
    {
        Point,
        TaskCount
    }

    public class QueueOperationId : GuidIdentity
    {
        public QueueOperationId()
            : base(Guid.Empty, typeof(QueueOperation))
        {
        }

        public QueueOperationId(Guid id)
            : base(id, typeof(QueueOperation))
        {
        }
    }
}
