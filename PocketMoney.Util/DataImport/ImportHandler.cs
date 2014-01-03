using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PocketMoney.Util.CSV;
using PocketMoney.Util.Streaming.CSV;

namespace PocketMoney.Util.DataImport
{
    public class ImportHandler : IImportHandler
    {
        private readonly IEnumerable<ColumnMetadata> columns;
        private readonly IEnumerable<IColumnMatcher> matchers;
        private readonly IRowPersister persister;
        private readonly IRowValidator validator;

        public ImportHandler(
            IEnumerable<IColumnMatcher> matchers,
            IEnumerable<ColumnMetadata> columns,
            IRowValidator validator,
            IRowPersister persister)
        {
            this.columns = columns;
            this.validator = validator;
            this.matchers = matchers;
            this.persister = persister;
        }

        #region IImportHandler Members

        public IEnumerable<string> GetColumnNames(string filePath)
        {
            using (FileStream stream = File.OpenRead(filePath))
            {
                using (var csv = new CsvReader(new StreamReader(stream), true))
                {
                    return csv.GetFieldHeaders();
                }
            }
        }

        public IDictionary<string, ColumnMetadata> GetMapping(string filePath)
        {
            var mapping = new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);

            foreach (string name in GetColumnNames(filePath))
            {
                string localName = name;
                bool matched = false;

                foreach (ColumnMetadata metadata in columns.Where(
                    metadata => matchers.Any(matcher =>
                                             matcher.Matching(localName, metadata))))
                {
                    mapping.Add(name, metadata);
                    matched = true;
                    break;
                }

                if (!matched)
                {
                    mapping.Add(name, null);
                }
            }

            return mapping;
        }

        public IEnumerable<Row> Load(string filePath, IDictionary<string, ColumnMetadata> mapping)
        {
            var rows = new List<Row>();

            using (FileStream stream = File.OpenRead(filePath))
            {
                using (var csv = new CsvReader(new StreamReader(stream), true))
                {
                    csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;

                    while (csv.ReadNextRecord())
                    {
                        var row = new Row();

                        foreach (var pair in mapping)
                        {
                            row.Data.Add(pair.Value.Name, csv[pair.Key]);
                        }

                        rows.Add(row);
                    }
                }
            }

            return rows;
        }

        public void Validate(Row row, IEnumerable<ColumnMetadata> metadata)
        {
            validator.Validate(row, metadata);
        }

        public bool Persist(Row row, IEnumerable<ColumnMetadata> metadata)
        {
            Validate(row, metadata);

            return !row.Errors.Any() && persister.Persist(row);
        }

        #endregion
    }
}