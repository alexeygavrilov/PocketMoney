using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util.DataImport
{
    public class RowValidator : IRowValidator
    {
        private static readonly TypeConverter stringConverter = TypeDescriptor.GetConverter(typeof (string));

        #region IRowValidator Members

        public void Validate(Row row, IEnumerable<ColumnMetadata> columns)
        {
            // Clear the previous errors
            row.Errors.Clear();

            foreach (RowValidationContext context in row.Data.Select(
                pair =>
                new RowValidationContext(
                    columns.Single(c => c.Name.Equals(pair.Key)),
                    pair.Value, row)))
            {
                ValidateRequired(context);
                ValidateLength(context);
                ValidateDataType(context);
                ValidateAdditional(context);
            }

            ValidateRow(row);
        }

        #endregion

        protected virtual void ValidateRequired(RowValidationContext context)
        {
            if (context.Metadata.Required &&
                string.IsNullOrWhiteSpace(context.Value))
            {
                context.Row.AddError(
                    context.Metadata.Name,
                    context.Metadata.Name + " cannot be blank.");
            }
        }

        protected virtual void ValidateLength(RowValidationContext context)
        {
            if (typeof(string) == context.Metadata.DataType &&
                !string.IsNullOrWhiteSpace(context.Value) &&
                ((context.Metadata.Length > 0) && context.Value.Length > context.Metadata.Length))
            {
                context.Row.AddError(
                    context.Metadata.Name,
                    context.Metadata.Name +
                    " cannot exceed " +
                    context.Metadata.Length +
                    " characters.");
            }
        }

        protected virtual void ValidateDataType(RowValidationContext context)
        {
            if (typeof(string) == context.Metadata.DataType ||
                string.IsNullOrWhiteSpace(context.Value))
            {
                return;
            }

            Action<string> addError = message =>
                                      context.Row.AddError(context.Metadata.Name, message);

            if (stringConverter.CanConvertTo(context.Metadata.DataType))
            {
                try
                {
                    stringConverter.ConvertTo(context.Value, context.Metadata.DataType);
                }
                catch(Exception ex)
                {
                    ex.LogError();
                    addError(
                        context.Metadata.Name +
                        " cannot be converted to " +
                        context.Metadata.DataType.Name + ".");
                    return;
                }
            }

            TypeConverter dataTypeConverter = TypeDescriptor.GetConverter(
                context.Metadata.DataType);

            if (!dataTypeConverter.CanConvertFrom(typeof (string)))
            {
                addError(
                    context.Metadata.Name +
                    " cannot be converted to " +
                    context.Metadata.DataType.Name + ".");
                return;
            }

            try
            {
                dataTypeConverter.ConvertFrom(context.Value);
            }
            catch (Exception ex)
            {
                ex.LogError();
                addError(
                    context.Metadata.Name +
                    " cannot be converted to {0}.".Interpolate(context.Metadata.DataTypeName));
            }
        }

        protected virtual void ValidateAdditional(RowValidationContext context)
        {
        }

        protected virtual void ValidateRow(Row row)
        {
        }
    }
}