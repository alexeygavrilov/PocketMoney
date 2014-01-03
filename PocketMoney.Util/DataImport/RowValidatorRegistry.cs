using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketMoney.Util.DataImport
{
    public class RowValidatorRegistry
    {
        private readonly Func<IEnumerable<ImportDefinition>> definitions;

        public RowValidatorRegistry(Func<IEnumerable<ImportDefinition>> definitions)
        {
            this.definitions = definitions;
        }

        public IDictionary<string, IRowValidator> Registrations
        {
            get { return definitions().ToDictionary(d => d.Name, d => d.Validator); }
        }
    }
}