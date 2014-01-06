using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace PocketMoney.Util.ExtensionMethods
{
    public static class ObjectExtensions
    {
        [DebuggerStepThrough]
        public static bool IsNull<TInstance>(this TInstance instance) where TInstance : class
        {
            return instance == null;
        }

        public static IEnumerable<TObject> SelectTo<TObject>(this IEnumerable instance)
            where TObject : class, new()
        {
            IList<TObject> result = new List<TObject>();
            var type = typeof(TObject);
            IDictionary<string, PropertyInfo> properties = type
                .GetProperties()
                .Where(property => property.CanWrite && !property.GetIndexParameters().Any())
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            foreach (var source in instance)
            {
                TObject obj = new TObject();
                foreach (var pair in source.ToDictionary()
                    .Where(pair => properties.ContainsKey(pair.Key.Name))
                    .Where(pair => properties[pair.Key.Name].PropertyType == pair.Key.PropertyType))
                {
                    properties[pair.Key.Name].SetValue(obj, pair.Value, null);
                }
                result.Add(obj);
            }
            return result;
        }


        // [DebuggerStepThrough]
        public static void AssignFrom<TTo, TFrom>(this TTo instance, TFrom source)
            where TTo : class
            where TFrom : class
        {
            IDictionary<string, PropertyInfo> properties = instance.GetType()
                .GetProperties()
                .Where(property => property.CanWrite && !property.GetIndexParameters().Any())
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            foreach (var pair in source.ToDictionary()
                .Where(pair => properties.ContainsKey(pair.Key.Name))
                .Where(pair => properties[pair.Key.Name].PropertyType == pair.Key.PropertyType))
            {
                properties[pair.Key.Name].SetValue(instance, pair.Value, null);
            }
        }

        public static object Property(this object instance, string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            Func<PropertyInfo, bool> filter = p =>
                                              p.CanRead &&
                                              !p.GetIndexParameters().Any() &&
                                              p.Name.Equals(name);

            PropertyInfo property = instance.GetType()
                .GetProperties()
                .Where(filter)
                .FirstOrDefault();
            if (property == null) return null;
            try
            {
                return property.GetValue(instance, null);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // [DebuggerStepThrough]
        public static IDictionary<PropertyInfo, object> ToDictionary(this object instance)
        {
            return instance.GetType()
                .GetProperties()
                .Where(property => property.CanRead && !property.GetIndexParameters().Any())
                .ToDictionary(property => property, property => property.GetValue(instance, null));
        }

        public static IDictionary<string, object> AsDictionary(this object instance)
        {
            return instance.GetType()
                .GetProperties()
                .Where(property => property.CanRead && !property.GetIndexParameters().Any())
                .ToDictionary(property => property.Name, property => property.GetValue(instance, null));
        }


        [DebuggerStepThrough]
        public static Boolean IsTrue(this bool? option)
        {
            return option.HasValue && option.Value;
        }

        [DebuggerStepThrough]
        public static Boolean IsTrue(this int? option)
        {
            return option.HasValue && option.Value == 1;
        }
    }
}