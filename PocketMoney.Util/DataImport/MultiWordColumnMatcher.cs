using System;

namespace PocketMoney.Util.DataImport
{
    public class MultiWordColumnMatcher : IColumnMatcher
    {
        #region IColumnMatcher Members

        public virtual bool Matching(string column, ColumnMetadata metadata)
        {
            string source = column.Replace(" ", string.Empty)
                .Replace("_", string.Empty)
                .Replace("-", string.Empty)
                .Replace("'", string.Empty)
                .Replace("/", string.Empty)
                .Replace("\\", string.Empty);

            string target = metadata.Name.Replace("_", string.Empty);

            return source.Equals(target, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}