using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace PocketMoney.Data.NHibernate.Conventions
{
    public class PrimaryKeyConvention : IIdConvention, IIdConventionAcceptance
    {
        #region IIdConvention Members

        public void Apply(IIdentityInstance instance)
        {
            instance.GeneratedBy.GuidComb();
        }

        #endregion

        #region IIdConventionAcceptance Members

        public void Accept(IAcceptanceCriteria<IIdentityInspector> criteria)
        {
            criteria.Expect(c => c.Generator, Is.Not.Set);
        }

        #endregion
    }
}