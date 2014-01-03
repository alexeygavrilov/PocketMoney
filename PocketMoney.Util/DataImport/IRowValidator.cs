using System.Collections.Generic;

namespace PocketMoney.Util.DataImport
{
    public interface IRowValidator
    {
        void Validate(Row row, IEnumerable<ColumnMetadata> columns);
    }
}