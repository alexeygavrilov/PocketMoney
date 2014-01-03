using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PocketMoney.Data
{
    [Serializable]
    [DataContract]
    public abstract class ObjectBase : object
    {
        #region Constants

        private const string TEMPLATE_START_TEXT = "{0} {{{1}";
        private const string TEMPLATE_END_TEXT = "}";
        private const string NULL = "NULL";
        private const string TEMPLATE_PROPERTY_TEXT = "    {0} = {1}; {2}";

        #endregion

        #region Methods

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            var type = this.GetType();

            if (type.BaseType != null && type.FullName.Contains("DynamicProx"))
            {
                type = type.BaseType;
            }

            result.AppendFormat(TEMPLATE_START_TEXT, type.Name, Environment.NewLine);

            foreach (var prop in type
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(DetailsAttribute))))
            {
                //TODO: process the Array

                object obj = prop.GetValue(this, null);

                result.AppendFormat(TEMPLATE_PROPERTY_TEXT,
                    prop.Name,
                    obj != null ? obj.ToString() : NULL,
                    Environment.NewLine);
            }
            result.Append(TEMPLATE_END_TEXT);

            return result.ToString();

        }
        #endregion
    }

    public class DetailsAttribute : Attribute
    {

    }
}
