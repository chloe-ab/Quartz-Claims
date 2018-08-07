using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace NewQuartzClaimsQuerier
{
    class Processor
    {
        //public static DataTable dt = new DataTable();// = new DataTable;
        public static void Run()
        {

            //note that paths can be relative or absolute
            //string initialDownloadFolder = @"C:Users\Chloe\source\repos\MyPersonalProjects\NewQuartzClaimsQuerier\ZipDownloads\";  //startpath
            string initialDownloadFolder = Directory.GetCurrentDirectory();

            //the path to the directory to be archived:
            string formattedDateTime = FileFetcher.GetFileDownload(initialDownloadFolder);

            string zipFileName = "Quartz_Claims_50k.shp.zip";

            //the path to the archive that is to be extracted:
            //string zipPath = @"./../ZipDownloads/" + formattedDateTime + zipFileName;  //actual file name at the end
            string zipPath = Directory.GetCurrentDirectory() + formattedDateTime + zipFileName;  //actual file name at the end

            //the path to the directory in which to place the extracted files:
            //string extractPath = @"./../ExtractedFiles/" + formattedDateTime + "Extracted-Files"; //need a unique folder with the date and time stamp as well
            string extractPath = Directory.GetCurrentDirectory() + formattedDateTime + "Extracted-Files"; //need a unique folder with the date and time stamp as well

            //redundant method? simply include method contents right here?
            FileFetcher.UnzipFile(zipPath, extractPath);


            string dbfFileName = "Quartz_Claims_50k.dbf";
            string dbfPath = extractPath;


            //the dbf file from the extracted files:
            string dbfFile = extractPath + @"/" + dbfFileName;

            Console.WriteLine("\n THE DBF FILE PATH IS:   \n" + dbfFile);

            string query = "SELECT * FROM " + dbfFile;
            Console.WriteLine("query is: " + query);

            DataTable fullDataTable = FileFetcher.GetDataTableFromDbf(dbfFile);


            //string excelPath = @"./../ExcelWorkbooks/";
            string excelPath = Directory.GetCurrentDirectory();

            var filteredClaimsTables = new List<FilteredClaimsTable>();
            filteredClaimsTables.Add(DataFilterer.GetNewClaimsTable(fullDataTable)); //default is 30 which is what they want for now
                                                                                     //filteredClaimsTables.Add(DataFilterer.GetPastExpiredClaimsTable(fullDataTable, 90));
            filteredClaimsTables.Add(DataFilterer.GetUpcomingExpiringClaimsTable(fullDataTable)); //default is 30 which is what they want for now
            filteredClaimsTables.Add(DataFilterer.GetUpcomingExpiringClaimsTable(fullDataTable, 90));

            var claimsTableNames = new List<string>();
            //could also make this list a property in ExcelWriter?
            foreach (FilteredClaimsTable table in filteredClaimsTables)
            {
                claimsTableNames.Add(table.GetDisplayName(true));

            }

            EmailSender.SendEmail(ExcelWriter.WriteExcelFile(excelPath, filteredClaimsTables), claimsTableNames);
        }


        //public void Demo ()
        //{
        //    DataTable dt = null;
        //    DataTable newClaimsSortedTable = DataSorter.sort(new NewClaimsSorter(dt, 30));
        //    DataTable pastExpiredClaimesSortedTable = DataSorter.sort(new PastExpiredClaimsSorter(dt, 45));
        //    DataTable upcomingExpiredClaimsSortedTable = DataSorter.sort(new UpcomingExpiringClaimsSorter(dt, 70));
        //}







        //AddData(dt);       

    }
    //    private static void AddData(DataTable dt)
    //    {
    //    dt.Columns.Add("CustLName", typeof(String));
    //    dt.Columns.Add("item", typeof(String));
    //    DataRow row;
    //    DataRow row2;
    //    DataRow row3;
    //        //for (int i = 0; i < 10; i++)
    //        //{
    //        //    row = dt.NewRow();
    //        //    row["CustLName"] = i;
    //        //    row["item"] = "item " + i.ToString();
    //        //    dt.Rows.Add(row);
    //        //}
    //        row = dt.NewRow();
    //        row["CustLName"] = "2thisISTest";
    //        row["item"] = "2itemTEST";
    //        dt.Rows.Add(row);
    //        row2 = dt.NewRow();
    //        row["CustLName"] = "1comesfirst";
    //        row["item"] = "1firstTEST";
    //        dt.Rows.Add(row2);
    //        row3 = dt.NewRow();
    //        row["CustLName"] = "3";
    //        row["item"] = "3";
    //        dt.Rows.Add(row3);
    //        //now try with data view:

    //        // Create DataView
    //        DataView view = new DataView(dt);

    //        // Sort by State and ZipCode column in descending order
    //        view.Sort = "item";
    //        DataTable newDt = view.ToTable();

    //    string excelPath = Directory.GetCurrentDirectory();
    //    var filteredClaimsTables = new List<FilteredClaimsTable>();
    //    var ft = new FilteredClaimsTable(TableType.NEW_CLAIMS_TABLE, 30, newDt);
    //    filteredClaimsTables.Add(ft); // TEEEEEEEST
    //    var claimsTableNames = new List<string>();
    //    claimsTableNames.Add("testtable");
    //    EmailSender.SendEmail(ExcelWriter.WriteExcelFileTest(excelPath, filteredClaimsTables), claimsTableNames);
    //}

}
