using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace xlsh
{
    public class ExcelHelper
    {
        public FileStream ExcelFile { get; set; }

        public ExcelPackage CreateDocument()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            var excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("QueryResult");
            return excel;
        }

        private const string FileName = @"d:\q.xlsx";


        public void AddRows(QueryResult queryResult)
        {
            using (var excel = CreateDocument())
            {
                var excelWorksheet = excel.Workbook.Worksheets["QueryResult"];

                string columnsRange = "A1:" + char.ConvertFromUtf32(queryResult.HeaderRow.Count + 64) + "1";

                var list = queryResult.Lines.Select(l => l.ToArray().ToArray());

                excelWorksheet.Cells[columnsRange].LoadFromArrays(list);

                var excelFile = new FileInfo(FileName);

                excel.SaveAs(excelFile);
            }
        }
    }
}