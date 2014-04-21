using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using System;

namespace PocketMoney.Model.Internal
{
    public class Point
    {
        public Point()
        {
            this.Value = 0;
            this.Parent = null;
        }

        public Point(IObject parent, int value)
        {
            this.Value = value;
            this.Parent = parent;
        }

        public IObject Parent { get; set; }

        public int Value { get; set; }

        public void Add(Point point)
        {
            this.Value += point.Value;
            if (this.Parent != null)
            {
                var historyRepository = ServiceLocator.Current.GetInstance<IRepository<QueueOperation, QueueOperationId, Guid>>();
                historyRepository.Add(new QueueOperation(this, point.Value));
            }
        }

        public static Point operator +(Point x, Point y)
        {
            return new Point(x.Parent, x.Value + y.Value);
        }

        public static Point operator -(Point x, Point y)
        {
            return new Point(x.Parent, x.Value - y.Value);
        }

        public static bool operator >(Point x, Point y)
        {
            return x.Value > y.Value;
        }

        public static bool operator >=(Point x, Point y)
        {
            return x.Value >= y.Value;
        }

        public static bool operator <(Point x, Point y)
        {
            return x.Value < y.Value;
        }

        public static bool operator <=(Point x, Point y)
        {
            return x.Value <= y.Value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
