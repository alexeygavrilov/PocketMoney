using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace PocketMoney.Util.CSV
{

    public class CsvStreamWriter
    {
        private CsvWriter csvWriter;

        public CsvStreamWriter(TextWriter textWriter)
        {
            csvWriter = new CsvWriter(textWriter);
        }

        public CsvStreamWriter(TextWriter textWriter, Char delimiter)
        {
            CsvConfiguration config = new CsvConfiguration();
            config.Delimiter = delimiter;
            csvWriter = new CsvWriter(textWriter, config);
        }

        public void Write<T>(IList<T> records)  where T : class
        {
            csvWriter.WriteRecords<T>(records);
        }

        public void WriteRow(IList<String> row)
        {
            foreach (String field in row)
            {
                csvWriter.WriteField(field);
            }
            csvWriter.NextRecord();
        }
    }
}
