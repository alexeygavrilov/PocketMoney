using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class Reward : Point
    {
        public Reward()
            : base()
        {
            this.Gift = null;
        }

        public Reward(IObject parent, int points)
            : base(parent, points)
        {
            this.Gift = null;
        }

        public Reward(IObject parent, string gift)
            : base(parent, 0)
        {
            this.Gift = gift;
        }

        public Reward(IObject parent, int points, string gift)
            : base(parent, 0)
        {
            if (!string.IsNullOrEmpty(gift))
                this.Gift = gift;
            else
                this.Points = points;
        }

        public Reward(int points, string gift)
            : this(null, points, gift)
        {
        }

        public string Gift { get; set; }

        public bool IsPoint { get { return string.IsNullOrEmpty(this.Gift); } }

        public override string ToString()
        {
            return !IsPoint ? this.Gift : this.Points > 0 ? this.Points.ToString() + " points" : "Unassigned";
        }

    }
}
