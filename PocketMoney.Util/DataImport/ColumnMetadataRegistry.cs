using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketMoney.Util.DataImport
{
    public class ColumnMetadataRegistry
    {
        private readonly Func<IEnumerable<ImportDefinition>> definitions;

        public ColumnMetadataRegistry(Func<IEnumerable<ImportDefinition>> definitions)
        {
            this.definitions = definitions;
        }

        public IDictionary<string, IEnumerable<ColumnMetadata>> Registrations
        {
            get { return definitions().ToDictionary(d => d.Name, d => d.Columns, StringComparer.OrdinalIgnoreCase); }
        }
    }
}