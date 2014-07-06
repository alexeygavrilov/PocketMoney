using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using System;

namespace PocketMoney.Model.Internal
{
    public class Point : ObjectBase
    {
        public Point()
        {
            this.Points = 0;
        }

        public Point(int value)
        {
            this.Points = value;
        }

        public Point(IObject parent)
            : this()
        {
            this.Parent = parent;
        }

        public Point(IObject parent, int value)
            : this(parent)
        {
            this.Points = value;
        }

        [Details]
        public IObject Parent { get; set; }

        [Details]
        public int Points { get; set; }

        public virtual void Deposit(Point point, Action<Point, int> addLog)
        {
            this.Points += point.Points;

            if (this.Parent != null)
            {
                addLog(this, point.Points);
            }
        }

        public void Deposit(Reward reward, Action<Point, int> addLog)
        {
            if (reward.IsPoint)
            {
                this.Deposit((Point)reward, addLog);
            }
        }

        public void Withdraw(Point point, Action<Point, int> addLog)
        {
            this.Points -= point.Points;
            
            if (this.Parent != null)
            {
                addLog(this, point.Points);
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
