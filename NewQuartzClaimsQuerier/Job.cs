using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewQuartzClaimsQuerier
{
    public class Job : IJob  //custom job class
    {          
       public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("job beginning execution");
            Console.ReadLine();

            //the job to be performed:
            //-----------------------------------

            //note that paths can be relative or absolute
            string initialDownloadFolder = @"ZipDownloads\";  //startpath

            //the path to the directory to be archived:
            string formattedDateTime = FileFetcher.GetFileDownload(initialDownloadFolder);

            string zipFileName = "Quartz_Claims_50k.shp.zip";

            //the path to the archive that is to be extracted:
            string zipPath = @"ZipDownloads\" + formattedDateTime + zipFileName;  //actual file name at the end

            //the path to the directory in which to place the extracted files:
            string extractPath = @"ExtractedFiles\" + formattedDateTime + "Extracted-Files"; //need a unique folder with the date and time stamp as well

            //redundant method? simply include method contents right here?
            FileFetcher.UnzipFile(zipPath, extractPath);


            string dbfFileName = "Quartz_Claims_50k.dbf";
            string dbfPath = extractPath;


            //the dbf file from the extracted files:
            string dbfFile = extractPath + @"\" + dbfFileName;

            Console.WriteLine("\n THE DBF FILE PATH IS:   \n" + dbfFile);

            string query = "SELECT * FROM " + dbfFile;
            Console.WriteLine("query is: " + query);

            DataTable fullDataTable = FileFetcher.GetDataTableFromDbf(dbfFile);


            string excelPath = @"ExcelWorkbooks\";

            //Create a DataTable with only the relevant data:
            DataTable newClaimsTable = DataSorter.GetNewClaimsTable(fullDataTable, 7);

            Console.WriteLine("Number of new claims is: " + newClaimsTable.Rows.Count);
            Console.ReadLine();

            //Create the new worksheet with the selected data:
            ExcelWriter.WriteExcelFile("New", excelPath, newClaimsTable);

            DataTable recentExpiredClaimsTable = DataSorter.GetPastExpiredClaimsTable(fullDataTable);
            ExcelWriter.WriteExcelFile("RecentlyExpired", excelPath, recentExpiredClaimsTable);

            DataTable upcomingExpiredClaimsTable = DataSorter.GetUpcomingExpiredClaimsTable(fullDataTable);
            ExcelWriter.WriteExcelFile("UpcomingExpiring", excelPath, upcomingExpiredClaimsTable);


            using (StreamWriter streamWriter = new StreamWriter(@"Logs\Log.txt", true))
            {
                streamWriter.WriteLine(DateTime.Now.ToString());
            }

            Console.WriteLine("Job has executed");
            Console.ReadLine();
        }

        Task IJob.Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}

