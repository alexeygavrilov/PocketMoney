using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class DutyType : Entity<DutyType, DutyTypeId, int>
    {
        protected DutyType()
        {
        }

        public DutyType(string name, Country country)
        {
            this.Name = name;
            this.Country = country;
        }

        public virtual string Name { get; set; }

        public virtual Country Country { get; set; }
    }

    [Serializable]
    public class DutyTypeId : IntIdentity
    {
        public DutyTypeId()
            : base(0)
        {
        }

        public DutyTypeId(string id)
            : base(id)
        {
        }

        public DutyTypeId(int id)
            : base(id)
        {
        }
    }

}
