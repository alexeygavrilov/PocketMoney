using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace PocketMoney.Data.NHibernate.Conventions
{
    public class ColumnNullabilityConvention : IPropertyConventionAcceptance, IPropertyConvention
    {
        private static readonly Type StringType = typeof (string);
        private static readonly Type NullableType = typeof (Nullable<>);

        #region IPropertyConvention Members

        public void Apply(IPropertyInstance instance)
        {
            Type propertyType = instance.Property.PropertyType;

            if (StringType.IsAssignableFrom(propertyType))
            {
                instance.Nullable();
            }
            else
            {
                if (propertyType.IsGenericType && NullableType.IsAssignableFrom(propertyType.GetGenericTypeDefinition()))
                {
                    instance.Nullable();
                }
                else
                {
                    instance.Not.Nullable();
                }
            }
        }

        #endregion

        #region IPropertyConventionAcceptance Members

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(c => c.Nullable, Is.Not.Set);
        }

        #endregion
    }
}