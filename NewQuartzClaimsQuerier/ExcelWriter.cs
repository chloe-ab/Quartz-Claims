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

            //set the column headers:
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string s = dt.Columns[j].ColumnName.ToString();
                ws.Cell(1, j + 1).SetValue<string>(s);
            }              

            Console.WriteLine("num of columns = " + dt.Columns.Count.ToString() + ", num of rows = " + dt.Rows.Count.ToString());
            Console.ReadLine();     

            //insert all elements for rows after the column headers:
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //Console.WriteLine("num of final cols is: " + dt.Columns.Count.ToString() + ", num of rows is: " + dt.Rows.Count.ToString());
                    //Console.ReadLine();

                    var element = dt.Rows[i][dt.Columns[j]].ToString();  //378, 582
                    //Console.WriteLine("row " + i +": " + dt.Rows[i].ToString() + ", column " + j + ": " +  dt.Columns[j].ToString());
                    //Console.ReadLine();

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

