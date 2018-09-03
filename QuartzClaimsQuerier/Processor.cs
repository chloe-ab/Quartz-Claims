using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace NewQuartzClaimsQuerier
{
    class Processor
    {
        private static readonly string REMOTE_URI = "ftp://ftp.geomaticsyukon.ca/GeoYukon/Mining/Quartz_Claims_50k/";
        private static readonly string FILE_NAME = "Quartz_Claims_50k.shp.zip";
        private static readonly string DBF_FILE_NAME = "Quartz_Claims_50k.dbf";
        private static readonly string ATTACHMENT_NAME = "Filtered Claims.xlsx";

        public static void Run()
        {
            // Concatenate the domain with the Web resource filename:
            string webResource = REMOTE_URI + FILE_NAME;

            //note that paths can be relative or absolute
            //string initialDownloadFolder = Directory.GetCurrentDirectory();
          
            Tuple<string, DataTable> tuple = FileFetcher.GetFileDownload(webResource, DBF_FILE_NAME);

            string formattedDateTime = tuple.Item1;
            DataTable fullDataTable = tuple.Item2;

            //the path to the archive that is to be extracted:
            //string zipPath = Directory.GetCurrentDirectory() + formattedDateTime + zipFileName;  //actual file name at the end

            //the path to the directory in which to place the extracted files:
            //string extractPath = @"./../ExtractedFiles/" + formattedDateTime + "Extracted-Files"; //need a unique folder with the date and time stamp as well
            //string extractPath = Directory.GetCurrentDirectory() + formattedDateTime + "Extracted-Files"; //need a unique folder with the date and time stamp as well

            //FileFetcher.UnzipFile(zipPath, extractPath);

            //string dbfPath = extractPath;

            //the dbf file from the extracted files:
            //string dbfFile = extractPath + @"/" + dbfFileName;

            //Console.WriteLine("\n THE DBF FILE PATH IS:   \n" + dbfFile);

            //string excelPath = @"./../ExcelWorkbooks/";
            //string excelPath = Directory.GetCurrentDirectory();

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

            string attachmentNameWithDate = DateTime.Now.ToShortDateString() + " " + ATTACHMENT_NAME;

            EmailSender.SendEmail(ExcelWriter.WriteExcelFile(filteredClaimsTables), claimsTableNames, attachmentNameWithDate);
        }
    }
}
