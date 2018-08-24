using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace NewQuartzClaimsQuerier
{
    class Program
    {
        static void Main()
        {
            //string excelPath = Directory.GetCurrentDirectory();
            //DataTable fullDataTable = new DataTable();

            //var filteredClaimsTables = new List<FilteredClaimsTable>();
            //filteredClaimsTables.Add(DataFilterer.GetNewClaimsTable(fullDataTable, 14));
            ////filteredClaimsTables.Add(DataFilterer.GetPastExpiredClaimsTable(fullDataTable, 90));
            //filteredClaimsTables.Add(DataFilterer.GetUpcomingExpiringClaimsTable(fullDataTable, 90));
            //filteredClaimsTables.Add(DataFilterer.GetUpcomingExpiringClaimsTable(fullDataTable, 180));
            ////now update ExcelWriter so that all files are in one workbook:
            //Console.WriteLine(filteredClaimsTables.Count.ToString());
            //EmailSender.SendEmail(ExcelWriter.WriteExcelFile(excelPath, filteredClaimsTables));

            //Console.WriteLine("stop here");
            //Console.ReadLine();
            //-------------------------------------------------------^delete the above



            //string initialDownloadFolder = @".\.\.\..\ZipDownloads\";  //startpath
            //Console.WriteLine(Directory.GetFiles(initialDownloadFolder));
            //Console.ReadLine();


            //Processor.Run();
            //instead of processor, changing back to jobscheduler
            JobScheduler.Start();
            Console.ReadLine();
            //







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
