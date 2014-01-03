using System.Collections.Generic;

namespace PocketMoney.Util.DataImport
{
    public abstract class ImportDefinition
    {
        private static readonly IRowValidator defaultValidator = new RowValidator();

        public abstract string Name { get; }

        public abstract IEnumerable<ColumnMetadata> Columns { get; }

        public abstract IRowPersister Persister { get; }

        public virtual IEnumerable<IColumnMatcher> Matchers
        {
            get
            {
                yield return new ExactNameColumnMatcher();
                yield return new MultiWordColumnMatcher();
            }
        }

        public virtual IRowValidator Validator
        {
            get { return defaultValidator; }
        }
    }
}