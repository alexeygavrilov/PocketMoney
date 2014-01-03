using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model
{
    public static class IFamilyExtensions
    {
        public static Family To(this IFamily family)
        {
            if (family == null) return null;
            if (family is Family)
                return (Family)family;
            else
            {
                var familyRepository = ServiceLocator.Current.GetInstance<IRepository<Family, FamilyId, Guid>>();
                var company = familyRepository.One(new FamilyId(family.Id));
                if (company == null)
                    throw new InvalidDataException("Family not found");
                return company;
            }
        }

        public static WrapperFamily From(this IFamily family)
        {
            if (family == null) return null;
            if (family is WrapperFamily)
                return (WrapperFamily)family;
            else
                return new WrapperFamily(family.Id, family.Name);
        }


    }
}
