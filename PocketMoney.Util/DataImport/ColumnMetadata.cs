using System;
using System.Web.Script.Serialization;

namespace PocketMoney.Util.DataImport
{
    [Serializable]
    public class ColumnMetadata
    {
        public string Name { get; set; }

        [ScriptIgnore]
        public Type DataType { get; set; }

        public string DataTypeName
        {
            get { return DataType.Name; }
        }

        public int Length { get; set; }

        public bool Required { get; set; }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}