using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;

namespace DbAccesEf
{
    public class GenerateData
    {

        
        private DataTable ExcekToDataTable(string fileName)
        {
            using(var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    DataTableCollection table = result.Tables;
                    DataTable resultTable = table["sheet1"];
                    return resultTable;
                }
            }
        }
    }
}
