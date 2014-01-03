using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketMoney.Util.DataImport
{
    public class ColumnMatcherRegistry
    {
        private readonly Func<IEnumerable<ImportDefinition>> definitions;

        public ColumnMatcherRegistry(Func<IEnumerable<ImportDefinition>> definitions)
        {
            this.definitions = definitions;
        }

        public IDictionary<string, IEnumerable<IColumnMatcher>> Registrations
        {
            get { return definitions().ToDictionary(d => d.Name, d => d.Matchers, StringComparer.OrdinalIgnoreCase); }
        }
    }
}