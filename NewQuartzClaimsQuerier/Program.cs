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
            //note that paths can be relative, don't have to be absolute
            string initialDownloadFolder = @"C:\Users\Chloe\source\repos\MyPersonalProjects\NewQuartzClaimsQuerier\ZipDownloads\";  //startpath

            //the path to the directory to be archived:
            string formattedDateTime = FileOperations.GetFileDownload(initialDownloadFolder);

            string zipFileName = "Quartz_Claims_50k.shp.zip";

            //the path to the archive that is to be extracted:
            string zipPath = @"C:\Users\Chloe\source\repos\MyPersonalProjects\NewQuartzClaimsQuerier\ZipDownloads\" + formattedDateTime + zipFileName;  //actual file name at the end

            //the path to the directory in which to place the extracted files:
            string extractPath = @"C:\Users\Chloe\source\repos\MyPersonalProjects\NewQuartzClaimsQuerier\ExtractedFiles\" + formattedDateTime + "Extracted-Files"; //need a unique folder with the date and time stamp as well

            //redundant method? simply include method contents right here?
            FileOperations.UnzipFile(zipPath, extractPath);


            string dbfFileName = "Quartz_Claims_50k.dbf"; 
            string dbfPath = extractPath;

                   
            //the dbf file from the extracted files:
            string dbfFile = extractPath + @"\" + dbfFileName;

            Console.WriteLine("\n THE DBF FILE PATH IS:   \n" + dbfFile);
            
            string query = "SELECT * FROM " + dbfFile;
            Console.WriteLine("query is: " + query);       

            DataTable fullDataTable = FileOperations.GetDataTableFromDbf(dbfFile);
            

            string excelPath = @"C:\Users\Chloe\source\repos\MyPersonalProjects\NewQuartzClaimsQuerier\ExcelWorkbooks\";

            //Create a DataTable with only the relevant data:
            DataTable newClaimsTable = DataSorter.GetNewClaimsTable(fullDataTable);

            Console.WriteLine("Number of new claims is: " + newClaimsTable.Rows.Count);
            Console.ReadLine();
            //Create the new worksheet with the selected data:
            ExcelWriter.WriteExcelFile("New", excelPath, newClaimsTable);  


            DataTable recentExpiredClaimsTable = DataSorter.GetPastExpiredClaimsTable(fullDataTable);
            ExcelWriter.WriteExcelFile("RecentlyExpired", excelPath, recentExpiredClaimsTable);

            DataTable upcomingExpiredClaimsTable = DataSorter.GetUpcomingExpiredClaimsTable(fullDataTable);
            ExcelWriter.WriteExcelFile("UpcomingExpiring", excelPath, upcomingExpiredClaimsTable);
        }
    }
}
