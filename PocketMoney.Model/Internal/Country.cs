using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class Country : Entity<Country, CountryId, int>
    {
        protected Country()
        {
        }

        public Country(int code, string name)
        {
            this.Id = code;
            this.Name = name;
        }

        public virtual string Name { get; set; }

    }

    [Serializable]
    public class CountryId : IntIdentity
    {
        public CountryId()
            : base(0)
        {
        }

        public CountryId(string id)
            : base(id)
        {
        }

        public CountryId(int id)
            : base(id)
        {
        }
    }
}
