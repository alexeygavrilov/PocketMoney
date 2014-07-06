using PocketMoney.Data;
using PocketMoney.FileSystem;
using System;
using System.Collections.Generic;

namespace PocketMoney.Model.Internal
{
    public class Attainment : Entity<Attainment, AttainmentId, Guid>, IObject
    {
        protected Attainment()
        {
            this.Processed = false;
            this.Reward = new Reward(this, 0);
        }

        public Attainment(string text, User user)
            : this()
        {
            this.Text = text;
            this.Creator = user;
            this.Family = user.Family;
        }

        [Details]
        public virtual string Text { get; set; }

        [Details]
        public virtual bool Processed { get; set; }

        [Details]
        public virtual Reward Reward { get; set; }

        [Details]
        public virtual User Creator { get; set; }

        [Details]
        public virtual Family Family { get; set; }

        public virtual Attainment Process(Reward reward)
        {
            this.Reward = reward;
            this.Processed = true;
            return this;
        }

        public virtual eObjectType ObjectType
        {
            get { return eObjectType.Attainment; }
        }

        public virtual string Name
        {
            get { return this.Text; }
        }
    }

    public class AttainmentId : GuidIdentity
    {
        public AttainmentId(Guid goalId)
            : base(goalId, typeof(Attainment))
        {
        }

        public AttainmentId()
            : base(Guid.Empty, typeof(Attainment))
        {
        }
    }

}
