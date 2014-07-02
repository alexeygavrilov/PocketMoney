using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using System;

namespace PocketMoney.Model.Internal
{
    public class Point 
    {
        public Point()
        {
            this.Points = 0;
            this.Parent = null;
        }

        public Point(IObject parent, int value)
        {
            this.Points = value;
            this.Parent = parent;
        }

        public IObject Parent { get; set; }

        public int Points { get; set; }

        public void Add(Point point)
        {
            this.Points += point.Points;
            if (this.Parent != null)
            {
                var historyRepository = ServiceLocator.Current.GetInstance<IRepository<QueueOperation, QueueOperationId, Guid>>();
                historyRepository.Add(new QueueOperation(this, point.Points));
            }
        }

        public static Point operator +(Point x, Point y)
        {
            return new Point(x.Parent, x.Points + y.Points);
        }

        public static Point operator -(Point x, Point y)
        {
            return new Point(x.Parent, x.Points - y.Points);
        }

        public static bool operator >(Point x, Point y)
        {
            return x.Points > y.Points;
        }

        public static bool operator >=(Point x, Point y)
        {
            return x.Points >= y.Points;
        }

        public static bool operator <(Point x, Point y)
        {
            return x.Points < y.Points;
        }

        public static bool operator <=(Point x, Point y)
        {
            return x.Points <= y.Points;
        }

        public override string ToString()
        {
            return this.Points.ToString();
        }
    }
}
