using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Configuration.Web
{
    public class TypeMappingRegistry<TType1, TType2>
    {
        private readonly IDictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

        protected IDictionary<Type, Type> Mappings
        {
            get { return _mappings; }
        }

        public virtual bool IsRegistered(Type type)
        {
            return Mappings.ContainsKey(type);
        }

        public virtual void Register(Type type1, Type type2)
        {
            EnsureType(typeof(TType1), type1, "type1");
            EnsureType(typeof(TType2), type2, "type2");

            Mappings.Add(type1, type2);
        }

        public virtual Type Matching(Type type)
        {
            return IsRegistered(type) ? Mappings[type] : null;
        }

        private static void EnsureType(Type parent, Type child, string parameterName)
        {
            if (!parent.IsAssignableFrom(child))
            {
                throw new InvalidOperationException(
                    string.Format("Incorrect type, \"{1}\" must be descended of \"{0}\".", child.Name, parent.Name)).LogError();
            }
        }
    }

    public static class ModelBinderMappingRegistryExtensions
    {
        public static TypeMappingRegistry<object, IModelBinder> Register<TModel, TModelBinder>(
            this TypeMappingRegistry<object, IModelBinder> instance) where TModelBinder : IModelBinder
        {
            instance.Register(typeof(TModel), typeof(TModelBinder));

            return instance;
        }
    }
}