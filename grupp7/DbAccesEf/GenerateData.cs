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
        public DataTable ExcelToDataTable(string fileName)
        {
            //temporary testing
            /*fileName = Path.GetDirectoryName(Environment.CurrentDirectory);
            fileName = fileName.Remove(fileName.Length - 21);
            fileName += @"\DbAccesEf\Resources\Produkter.xlsx";*/

            string filePath = Path.GetDirectoryName(Environment.CurrentDirectory);
            filePath = filePath.Remove(filePath.Length - 21);
            filePath += @"\DbAccesEf\Resources\" + fileName;

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
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

                    DataTable resultTable = result.Tables[0];
                    return resultTable;
                }
            }
        }
    }
}
