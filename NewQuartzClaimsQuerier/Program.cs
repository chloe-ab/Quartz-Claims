using System;
using System.Collections.Generic;
using System.Data; 
using System.Linq;
using System.Net; 
using System.Text;
using System.Threading.Tasks;


namespace NewQuartzClaimsQuerier
{
    class Program
    {
        static void Main()
        {

            JobScheduler.Start();
            //----------

            ////note that paths can be relative or absolute
            //string initialDownloadFolder = @"ZipDownloads\";  //startpath

            ////the path to the directory to be archived:
            //string formattedDateTime = FileFetcher.GetFileDownload(initialDownloadFolder);

            //string zipFileName = "Quartz_Claims_50k.shp.zip";

            ////the path to the archive that is to be extracted:
            //string zipPath = @"ZipDownloads\" + formattedDateTime + zipFileName;  //actual file name at the end

            ////the path to the directory in which to place the extracted files:
            //string extractPath = @"ExtractedFiles\" + formattedDateTime + "Extracted-Files"; //need a unique folder with the date and time stamp as well

            ////redundant method? simply include method contents right here?
            //FileFetcher.UnzipFile(zipPath, extractPath);


            //string dbfFileName = "Quartz_Claims_50k.dbf";
            //string dbfPath = extractPath;


            ////the dbf file from the extracted files:
            //string dbfFile = extractPath + @"\" + dbfFileName;

            //Console.WriteLine("\n THE DBF FILE PATH IS:   \n" + dbfFile);

            //string query = "SELECT * FROM " + dbfFile;
            //Console.WriteLine("query is: " + query);

            //DataTable fullDataTable = FileFetcher.GetDataTableFromDbf(dbfFile);


            //string excelPath = @"ExcelWorkbooks\";

            ////Create a DataTable with only the relevant data:
            //DataTable newClaimsTable = DataSorter.GetNewClaimsTable(fullDataTable, 7);

            //Console.WriteLine("Number of new claims is: " + newClaimsTable.Rows.Count);
            //Console.ReadLine();

            ////Create the new worksheet with the selected data:
            //ExcelWriter.WriteExcelFile("New", excelPath, newClaimsTable);

            //DataTable recentExpiredClaimsTable = DataSorter.GetPastExpiredClaimsTable(fullDataTable);
            //ExcelWriter.WriteExcelFile("RecentlyExpired", excelPath, recentExpiredClaimsTable);

            //DataTable upcomingExpiredClaimsTable = DataSorter.GetUpcomingExpiredClaimsTable(fullDataTable);
            //ExcelWriter.WriteExcelFile("UpcomingExpiring", excelPath, upcomingExpiredClaimsTable);


            //Jul 24 Scheduler stuff:
            //https://www.infoworld.com/article/3078781/application-development/how-to-work-with-quartz-net-in-c.html
            //http://www.quartz-scheduler.org/documentation/quartz-2.x/tutorials/crontrigger.html
            //quartz documentation: https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/using-quartz.html

            //2 ways to start the scheduler:
            //either use a windows service to start your scheduler. or If you are using an ASP.Net web application, you can take advantage of the Application_Start event of the 
            //Global.asax file and then make a call to JobScheduler.Start() method 

            //windows task scheduler is probably more reliable than ASP.NET
            //can use either windows task scheduler or windows service
            //task scheduler is the simplest method. as long as it works for project, use it.

            //Scheduled tasks using Windows Task Scheduler and .Net console application:



        }
    }
}
