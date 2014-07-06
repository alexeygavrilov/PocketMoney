using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Model.Internal
{
    public class Family : Entity<Family, FamilyId, Guid>, IFamily, IObject
    {
        public const int TOKEN_KEY_LENGTH = 8;

        protected Family()
        {
            this.Description = string.Empty;
            this.Members = new List<User>();
            this.TaskCount = new ActionCount(this);
            this.Points = new Point(this);
        }

        public Family(string name, Country country)
            : this()
        {
            this.Name = name;
            this.Country = country;
            this.TokenKey = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789".Generation(TOKEN_KEY_LENGTH);
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

        public virtual string TokenKey { get; set; }

        [Details]
        public virtual ActionCount TaskCount { get; set; }

        [Details]
        public virtual Point Points { get; set; }

        public virtual int CountryCode 
        {
            get { return Country.Id; }
        }

        public virtual bool IsAnonymous
        {
            get { return false; }
        }

        IList<IUser> IFamily.Members
        {
            get { return this.Members.Select(u => (IUser)u).ToList(); }
        }

        public virtual eObjectType ObjectType
        {
            get { return eObjectType.Family; }
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
