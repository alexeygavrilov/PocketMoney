using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class Family : Entity<Family, FamilyId, Guid>, IFamily
    {
        protected Family()
        {
            this.Description = string.Empty;
            this.Members = new List<User>();
        }

        public Family(string name, Country country)
        {
            this.Name = name;
            this.Country = country;
        }

        [Details]
        public virtual string Name { get; set; }

        [Details]
        public virtual string Description { get; set; }

        [Details]
        public virtual Country Country { get; set; }

        [Details]
        public virtual string Culture { get; set; }

        public virtual IList<User> Members { get; set; }

        public virtual bool IsAnonymous
        {
            get { return false; }
        }

        IList<IUser> IFamily.Members
        {
            get { return this.Members.Select(u => (IUser)u).ToList(); }
        }
    }

    public class FamilyId : GuidIdentity
    {
        public FamilyId(Guid familyyId)
            : base(familyyId, typeof(Family))
        {
        }

        public FamilyId()
            : base(Guid.Empty, typeof(Family))
        {
        }
    }
}
