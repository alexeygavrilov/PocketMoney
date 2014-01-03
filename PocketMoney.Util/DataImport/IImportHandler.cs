using System.Collections.Generic;

namespace PocketMoney.Util.DataImport
{
    public interface IImportHandler
    {
        IEnumerable<string> GetColumnNames(string filePath);

        IDictionary<string, ColumnMetadata> GetMapping(string filePath);

        IEnumerable<Row> Load(string filePath, IDictionary<string, ColumnMetadata> mapping);

        void Validate(Row row, IEnumerable<ColumnMetadata> metadata);

        bool Persist(Row row, IEnumerable<ColumnMetadata> metadata);
    }
}