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
    }
}
