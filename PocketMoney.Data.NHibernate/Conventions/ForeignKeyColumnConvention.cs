using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace PocketMoney.Data.NHibernate.Conventions
{
    public class ForeignKeyConvention : IReferenceConvention, IHasManyToManyConvention,
                                        IJoinedSubclassConvention, IJoinConvention, ICollectionConvention
    {
        #region ICollectionConvention Members

        public void Apply(ICollectionInstance instance)
        {
            string columnName = GetKeyName(null, instance.EntityType);

            instance.Key.Column(columnName);
        }

        #endregion

        #region IHasManyToManyConvention Members

        public void Apply(IManyToManyCollectionInstance instance)
        {
            string keyColumn = GetKeyName(null, instance.EntityType);
            string childColumn = GetKeyName(null, instance.ChildType);

            if (instance.Key.Columns.IsEmpty())
            {
                instance.Key.Column(keyColumn);
            }

            if (instance.Relationship.Columns.IsEmpty())
            {
                instance.Relationship.Column(childColumn);
            }
        }

        #endregion

        #region IJoinConvention Members

        public void Apply(IJoinInstance instance)
        {
            string columnName = GetKeyName(null, instance.EntityType);

            instance.Key.Column(columnName);
        }

        #endregion

        #region IJoinedSubclassConvention Members

        public void Apply(IJoinedSubclassInstance instance)
        {
            string columnName = GetKeyName(null, instance.Type.BaseType);

            instance.Key.Column(columnName);
        }

        #endregion

        #region IReferenceConvention Members

        public void Apply(IManyToOneInstance instance)
        {
            string columnName = GetKeyName(instance.Property, instance.Class.GetUnderlyingSystemType());

            instance.Column(columnName);
        }

        #endregion

        private static string GetKeyName(Member property, Type type)
        {
            string columnName = (property != null ? property.Name : type.Name) + "Id";

            return columnName;
        }
    }
}