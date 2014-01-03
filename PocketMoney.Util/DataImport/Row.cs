using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PocketMoney.Util.DataImport
{
    [Serializable]
    public class Row
    {
        private IDictionary<string, string> data;
        private IDictionary<string, IList<string>> errors;

        public IDictionary<string, string> Data
        {
            get { return data ?? (data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)); }

            set { data = value; }
        }

        public IDictionary<string, IList<string>> Errors
        {
            get { return errors ?? (errors = new Dictionary<string, IList<string>>(StringComparer.OrdinalIgnoreCase)); }

            set { errors = value; }
        }

        public virtual void AddError(string column, string message)
        {
            IList<string> messages;

            if (!Errors.TryGetValue(column, out messages))
            {
                messages = new List<string>();
                Errors.Add(column, messages);
            }

            messages.Add(message);
        }

        public virtual bool HasError(string column)
        {
            IList<string> messages;

            return Errors.TryGetValue(column, out messages) && messages.Any();
        }

        public T GetModel<T>() where T : new()
        {
            T model = new T();

            var properties = typeof(T).GetProperties()
                .Where(p => p.CanWrite && !p.GetIndexParameters().Any())
                .ToList();

            foreach (var pair in this.Data.Where(p => !string.IsNullOrWhiteSpace(p.Value)))
            {
                PropertyInfo property =
                    properties.First(p => p.Name.Equals(pair.Key, StringComparison.OrdinalIgnoreCase));
                object value = Convert.ChangeType(pair.Value, property.PropertyType);

                property.SetValue(model, value, null);
            }

            return model;
        }


    }
}