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

        public static XLWorkbook WriteExcelFile(List<FilteredClaimsTable> filteredClaimsTables, bool sortByDate = true, bool sortByLabel = true)  //the path you want to save the WorkBook to
        {
            XLWorkbook wb = new XLWorkbook();

            foreach (FilteredClaimsTable table in filteredClaimsTables)
            {
                SortByDateAndLabel(table);

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

                //SortByLabel(workSheet, table);
            }
         
            //wb.SaveAs(workbookFileName);
            return wb;
        }


        private static void SortByDateAndLabel(FilteredClaimsTable table)
        {
            //DataView view = new DataView(table.DataTable);
            DataView view = table.DataTable.DefaultView;

            string sortingDateColumn = table.TableType.DefiningDateColumn;

            string combinedSortingColumns = String.Join(", ", sortingDateColumn, TableType.LABEL);
            //Console.WriteLine("combined sorting columns: " + combinedSortingColumns);
            //Console.ReadLine();

            view.Sort = combinedSortingColumns;  
            //Console.WriteLine("view.sort is: " + view.Sort);
            //Console.ReadLine();

            //table.DataTable = view.ToTable();  //SET table.DataTable AT THE END OF METHOD INSTEAD ////////////////////////////////////////////
            table.DataTable = view.ToTable();
            //DataTable fullTableToSortByLabel = view.ToTable();  //using this, going to break into small subtables which will then be sorted by views.

            //now it's sorted by date, but still need to sort by label within this:
            //DataView view2 = fullTableToSortByLabel.DefaultView;                      //IS this THE SAME AS VIEW? GET RID OF REDUNDANCIES

        //    ///TEST::::::::::::::::::::::
        //    DataRow row1 = fullTableToSortByLabel.Rows[1];   //or 2 cuz of headers?
        //                                                     //DateTime currentDate = row1.Field<DateTime>(sortingDateColumn);


        //    DataTable subTableToSortByLabel = new DataTable();  //need this .
        //    DataRow headers = fullTableToSortByLabel.Rows[0];               //ARE THESE THE HEADERS? OR IS IT 1????????????

        //    //foreach (DataColumn dc in fullTableToSortByLabel)
        //    //{
        //    //    subTableToSortByLabel.Columns.Add(dc.ColumnName, dc.DataType);
        //    //}

        //    //subTableToSortByLabel.Columns. = fullTableToSortByLabel.Columns.;
        //    subTableToSortByLabel.Rows.Add(headers.ItemArray);
        //    subTableToSortByLabel.Rows.Add(row1.ItemArray);
        //    //the 2 below lines didnt' work, replacing with above 2 lines
        //    //subTableToSortByLabel.Rows.Add(headers); //set the headers
        //    //subTableToSortByLabel.Rows.Add(row1);

        //    DataTable masterSortedTable = new DataTable();
        //    masterSortedTable.Rows.Add(headers); //set the headers

        //    for (int i = 2; i <= fullTableToSortByLabel.Rows.Count; i++) //(DataRow row in table.DataTable.Rows)  //start at 3 cuz of headers?
        //    {
        //        //need a subset of view2

        //        DataRow currentRow = fullTableToSortByLabel.Rows[i];
        //        DataRow prevRow = fullTableToSortByLabel.Rows[i-1];

        //        //looking at specific row i now:
        //        if (currentRow.Field<DateTime>(sortingDateColumn) == prevRow.Field<DateTime>(sortingDateColumn))  //TRYING DATETIME BUT MIGHT NEED TO CHANGE BACK to string
        //        {
        //            //then add the row to the current subtable
        //            subTableToSortByLabel.Rows.Add(currentRow);
        //        }
        //        else
        //        {
        //            //cement the subtable up to this point as it's own view, sort, and then make a new datatable. 
        //            DataView subview = subTableToSortByLabel.DefaultView;  //this is a temporary view, and that's fine. it's cemented in the table which we create below, subSortedTable.
        //            subview.Sort = "LABEL";
        //            DataTable subSortedTable = subview.ToTable();
        //            //now add subSortedTable to the master sorted table.
        //            for (int j = 1; j <= subSortedTable.Rows.Count; j++)
        //            {
        //                masterSortedTable.Rows.Add(subSortedTable.Rows[j]);  //this contains all rows, sorted by label.
        //            }

        //            //now also need to begin the next datatable: 
        //            subTableToSortByLabel.Clear(); //THIS SHOULD KEEP THE HEADERS, BUT DOUBLECHECK!!!                                  
        //            subTableToSortByLabel.Rows.Add(currentRow);
        //        }
        //    }
        //    Console.WriteLine("length of master table: " + masterSortedTable.Rows.Count.ToString());
        //    Console.ReadLine();
        //    //
        //    Console.WriteLine("\ntest: the name of this table's DataTable is: " + table.TableType.Name);
        //    Console.WriteLine("\n and the defining column to sort by is: " + table.TableType.DefiningDateColumn);
        //    Console.ReadLine();

        //    //FINALLY AT END OF METHOD:
        //    table.DataTable = masterSortedTable;
            

        //    //each TableType has a defining column that it gets filtered by and sorted by afterwards
        //    //workSheet.Sort(table.TableType.DefiningDateColumn);
        }

        //private static void SortByLabel(IXLWorksheet workSheet, FilteredClaimsTable table)
        //{
        //    //workSheet.Sort(7); //"LABEL"
        //    workSheet.Sort("LABEL");
        //}

        //public static DataTable dt = new DataTable();// = new DataTable;

        //private static void AddData(DataTable dt)
        //{
        //      dt.Columns.Add("CustLName", typeof(String));
        //    DataRow row;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        row = dt.NewRow();
        //        row["id"] = i;
        //        row["item"] = "item " + i.ToString();
        //        dt.Rows.Add(row);
        //    }
        //}

        //public FilteredClaimsTable test = new FilteredClaimsTable(TableType.NEW_CLAIMS_TABLE, 10, dt);

        //public static XLWorkbook WriteExcelFileTest(string workbookFolderPath, List<FilteredClaimsTable> filteredClaimsTables)  //the path you want to save the WorkBook to
        //{

        //    XLWorkbook wb = new XLWorkbook();

        //    foreach (FilteredClaimsTable table in filteredClaimsTables)
        //    {
        //        DataTable dt = table.DataTable;
        //        string wsName = table.GetDisplayName();

        //        var workSheet = wb.Worksheets.Add(wsName);
        //        //var test = dt.Rows[0][0];

        //        //set the column headers:
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            string s = dt.Columns[j].ColumnName.ToString();
        //            workSheet.Cell(1, j + 1).SetValue<string>(s);
        //        }

        //        //insert all elements for rows after the column headers:
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                var element = dt.Rows[i][dt.Columns[j]].ToString();
        //                workSheet.Cell(i + 2, j + 1).SetValue<string>(element);
        //            }
        //        }
        //        workSheet.Sort(1);
        //    }

        //    string workbookFileName = "Filtered Claims TEST.xlsx";
        //    WorkbookPath = workbookFolderPath + " " + workbookFileName;
        //    wb.SaveAs(WorkbookPath);
        //    return wb;

        //}
    }
}

