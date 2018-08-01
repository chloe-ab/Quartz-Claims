using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewQuartzClaimsQuerier
{
    class ExcelWriter
    {
        //Using DocumentFormat.OpenXml and ClosedXML Nuget packages

        public static string WorkbookPath;

        public static XLWorkbook WriteExcelFile(string workbookFolderPath, List<SortedClaimsTable> sortedClaimsTables)  //the path you want to save the WorkBook to
        {
            XLWorkbook wb = new XLWorkbook();

            foreach (SortedClaimsTable table in sortedClaimsTables)
            {
                DataTable dt = table.DataTable;
                string wsName = table.GetDisplayName();

                var workSheet = wb.Worksheets.Add(wsName);
                //var test = dt.Rows[0][0];

                //set the column headers:
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string s = dt.Columns[j].ColumnName.ToString();
                    workSheet.Cell(1, j + 1).SetValue<string>(s);
                }

                //insert all elements for rows after the column headers:
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        var element = dt.Rows[i][dt.Columns[j]].ToString();
                        workSheet.Cell(i + 2, j + 1).SetValue<string>(element);
                    }
                }             
            }

            string workbookFileName = "Filtered Claims.xlsx";
            WorkbookPath = workbookFolderPath + " " + workbookFileName;
            wb.SaveAs(WorkbookPath);
            return wb;
        }
    }
}

