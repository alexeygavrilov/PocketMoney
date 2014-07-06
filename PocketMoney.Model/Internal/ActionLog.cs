using PocketMoney.Data;
using PocketMoney.Util;
using System;

namespace PocketMoney.Model.Internal
{
    public class ActionLog : Entity<ActionLog, ActionLogId, Guid>
    {
        protected ActionLog()
        {
            this.Processed = false;
        }

        public ActionLog(Guid objectId, string objectName, eObjectType objectType, ActionValueType valueType, int value, Guid targetId, string targetName)
        {
            this.ObjectId = objectId;
            this.ObjectName = objectName;
            this.ObjectType = objectType;
            this.ValueType = valueType;
            this.TargetId = targetId;
            this.TargetName = targetName;
            this.ChangeValue = value;
            this.ChangeDate = Clock.UtcNow();
            this.Processed = objectType != eObjectType.User;
        }

        public ActionLog(IObject target, Point point, int value) :
            this(point.Parent.Id, point.Parent.Name, point.Parent.ObjectType, ActionValueType.Point, value, target.Id, target.Name)
        {
        }

        public ActionLog(Point point, int value) :
            this(point.Parent.Id, point.Parent.Name, point.Parent.ObjectType, ActionValueType.Point, value, Guid.Empty, string.Empty)
        {
        }


        public ActionLog(IObject target, ActionCount count, ActionValueType actionType) :
            this(count.Parent.Id, count.Parent.Name, count.Parent.ObjectType, actionType, 1, target.Id, target.Name)
        {
        }

        public virtual Guid ObjectId { get; set; }
        public virtual string ObjectName { get; set; }
        public virtual eObjectType ObjectType { get; set; }
        public virtual ActionValueType ValueType { get; set; }
        public virtual Guid TargetId { get; set; }
        public virtual string TargetName { get; set; }
        public virtual int ChangeValue { get; set; }
        public virtual DateTime ChangeDate { get; set; }
        public virtual bool Processed { get; set; }

        public virtual string GetText()
        {
            switch (this.ValueType)
            {
                case ActionValueType.Point:
                    if (this.ChangeValue > 0)
                    {
                        return string.Format("{0}: {1}'s balance has been increased by {2} points.", this.ChangeDate, this.ObjectName, this.ChangeValue);
                    }
                    else
                    {
                        return string.Format("{0}: {1}'s balance has been decreased by {2} points.", this.ChangeDate, this.ObjectName, -this.ChangeValue);
                    }
                case ActionValueType.CompleteTask:
                    return string.Format("{0}: {1} has completed task: {2}", this.ChangeDate, this.ObjectName, this.TargetName);
                case ActionValueType.CompleteGoal:
                    return string.Format("{0}: {1} has reached the goal: {2}", this.ChangeDate, this.ObjectName, this.TargetName);
                case ActionValueType.GrabTask:
                    return string.Format("{0}: {1} has appointed task to itself: {2}", this.ChangeDate, this.ObjectName, this.TargetName);
                case ActionValueType.GoodDeed:
                    return string.Format("{0}: {1} done the good deed: {2}", this.ChangeDate, this.ObjectName, this.TargetName);
                default:
                    return string.Empty;
            }


        }
    }

    public enum QueueOperationSourceType
    {
        User,
        Family
    }

    public enum ActionValueType
    {
        Point,
        CompleteTask,
        CompleteGoal,
        GrabTask,
        GoodDeed
    }

    public class ActionLogId : GuidIdentity
    {
        public ActionLogId()
            : base(Guid.Empty, typeof(ActionLog))
        {
        }

        public ActionLogId(Guid id)
            : base(id, typeof(ActionLog))
        {
        }
    }
}
