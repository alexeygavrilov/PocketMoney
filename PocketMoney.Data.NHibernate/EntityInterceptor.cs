using System;
using PocketMoney.Util;
using PocketMoney.Data;
using NHibernate;
using NHibernate.Type;

namespace PocketMoney.Data.NHibernate
{
    public class EntityInterceptor : EmptyInterceptor
    {
        private const string DateCreatedProperty = "DateCreated";
        private const string DateUpdatedProperty = "DateUpdated";
        private const string VersionProperty = "Version";

        public override bool OnSave(object entity, object id, object[] state,
                                    string[] propertyNames, IType[] types)
        {
            if (entity is IEntity)
            {
                DateTime now = Clock.UtcNow();
                int dateCreatedIndex = GetPropertyIndex(propertyNames, DateCreatedProperty);
                if (dateCreatedIndex != -1)
                    state[dateCreatedIndex] = now;
                int dateUpdatedIndex = GetPropertyIndex(propertyNames, DateUpdatedProperty);
                if (dateUpdatedIndex != -1)
                    state[dateUpdatedIndex] = now;
                int versionIndex = GetPropertyIndex(propertyNames, VersionProperty);
                if (versionIndex != -1)
                    state[versionIndex] = 0;
            }
            return true;
        }


        public override bool OnFlushDirty(object entity, object id, object[] currentState,
                                          object[] previousState, string[] propertyNames, IType[] types)
        {
            if (entity is IEntity)
            {
                DateTime now = Clock.UtcNow();
                int dateCreatedIndex = GetPropertyIndex(propertyNames, DateCreatedProperty);
                if (dateCreatedIndex != -1 && currentState[dateCreatedIndex] == null)
                    if (previousState[dateCreatedIndex] != null)
                        currentState[dateCreatedIndex] = previousState[dateCreatedIndex];
                    else
                        currentState[dateCreatedIndex] = now;
                int dateUpdatedIndex = GetPropertyIndex(propertyNames, DateUpdatedProperty);
                if (dateUpdatedIndex != -1)
                    currentState[dateUpdatedIndex] = now;
            }
            return true;
        }

        private static int GetPropertyIndex(string[] propertyNames, string propertyName)
        {
            return Array.IndexOf(propertyNames, propertyName);
        }
    }
}