﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class Duty : Entity<Duty, DutyId, Guid>
    {
        protected Duty()
        {
            this.Dates = new List<DutyDate>();
        }

        public Duty(User user, string name, string form, int reward, IList<DutyDate> dates)
        {
            this.Name = name;
            this.User = user;
            this.Form = form;
            this.Reward = reward;
            this.Dates = dates;
        }

        public virtual string Name { get; set; }

        public virtual string Form { get; set; }

        public virtual int Reward { get; set; }

        public virtual User User { get; set; }

        public virtual IList<DutyDate> Dates { get; set; }
    }

    [Serializable]
    public class DutyId : GuidIdentity
    {
        public DutyId()
            : base(Guid.Empty, typeof(Duty))
        {
        }

        public DutyId(string id)
            : base(id, typeof(Duty))
        {
        }

        public DutyId(Guid id)
            : base(id, typeof(Duty))
        {
        }
    }

}