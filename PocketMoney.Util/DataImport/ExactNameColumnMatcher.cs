using System;

namespace PocketMoney.Util.DataImport
{
    public class ExactNameColumnMatcher : IColumnMatcher
    {
        #region IColumnMatcher Members

        public virtual bool Matching(string column, ColumnMetadata metadata)
        {
            return column.Equals(metadata.Name, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}