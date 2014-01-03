using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketMoney.Util.DataImport
{
    public class RowPersisterRegistry
    {
        private readonly Func<IEnumerable<ImportDefinition>> definitions;

        public RowPersisterRegistry(Func<IEnumerable<ImportDefinition>> definitions)
        {
            this.definitions = definitions;
        }

        public IDictionary<string, IRowPersister> Registrations
        {
            get { return definitions().ToDictionary(d => d.Name, d => d.Persister); }
        }
    }
}