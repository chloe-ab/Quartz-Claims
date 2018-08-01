using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace NewQuartzClaimsQuerier
{
    class Processor
    {
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

            //Create a DataTable with only the relevant data:
            //DataTable newClaimsTable = DataSorter.GetNewClaimsTable(fullDataTable, 7);

            ////Console.WriteLine("Number of new claims is: " + newClaimsTable.Rows.Count);
            ////Console.ReadLine();

            ////Create the new worksheet with the selected data:
            //ExcelWriter.WriteExcelFile("New", excelPath, newClaimsTable);

            //DataTable recentExpiredClaimsTable = DataSorter.GetPastExpiredClaimsTable(fullDataTable);
            //ExcelWriter.WriteExcelFile("RecentlyExpired", excelPath, recentExpiredClaimsTable);

            ////for last workbook: using to test new emailsender
            //DataTable upcomingExpiredClaimsTable = DataSorter.GetUpcomingExpiredClaimsTable(fullDataTable);
            //XLWorkbook wb = ExcelWriter.WriteExcelFile("UpcomingExpiring", excelPath, upcomingExpiredClaimsTable);
            //EmailSender.SendEmail("sample string", wb);


            var sortedClaimsTables = new List<SortedClaimsTable>();
            sortedClaimsTables.Add(DataSorter.GetNewClaimsTable(fullDataTable, 14));
            //sortedClaimsTables.Add(DataSorter.GetPastExpiredClaimsTable(fullDataTable, 90));
            sortedClaimsTables.Add(DataSorter.GetUpcomingExpiringClaimsTable(fullDataTable, 90));
            sortedClaimsTables.Add(DataSorter.GetUpcomingExpiringClaimsTable(fullDataTable, 180));

            var claimsTableNames = new List<string>();
            //could also make this list a property in ExcelWriter?
            foreach (SortedClaimsTable table in sortedClaimsTables)
            {
                claimsTableNames.Add(table.GetDisplayName(true));
            }

            EmailSender.SendEmail(ExcelWriter.WriteExcelFile(excelPath, sortedClaimsTables), claimsTableNames);
        }

        //public void Demo ()
        //{
        //    DataTable dt = null;
        //    DataTable newClaimsSortedTable = DataSorter.sort(new NewClaimsSorter(dt, 30));
        //    DataTable pastExpiredClaimesSortedTable = DataSorter.sort(new PastExpiredClaimsSorter(dt, 45));
        //    DataTable upcomingExpiredClaimsSortedTable = DataSorter.sort(new UpcomingExpiringClaimsSorter(dt, 70));
        //}
    }
}
