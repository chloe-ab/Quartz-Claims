using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewQuartzClaimsQuerier
{
    class ExcelWriter
    {
        //Using DocumentFormat.OpenXml and ClosedXML Nuget packages:
        //tableType is the worksheet name
        public static void WriteExcelFile(string tableType, string path, DataTable dt)
        {
            XLWorkbook wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(tableType);
            //var test = dt.Rows[0][0];

            //set the column headers:
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string s = dt.Columns[j].ColumnName.ToString();
                ws.Cell(1, j + 1).SetValue<string>(s);
            }                

            //insert all elements for rows after the column headers:
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    var element = dt.Rows[i][dt.Columns[j]].ToString(); 
                    ws.Cell(i + 2, j + 1).SetValue<string>(element);      
                }  
            }

            string wbFileName = tableType + "Claims.xlsx";
            string wbFolder = path;
            string wbPath = wbFolder + wbFileName;
            wb.SaveAs(wbPath);           
        }
    }
}

