using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO.Compression;
using System.IO;
using NDbfReader;

namespace NewQuartzClaimsQuerier
{
    class FileFetcher
    {
        //Downloads the file off of the Yukon government website, and returns the time of download
        public static string GetFileDownload(string newLocationFolder)
        {
            string remoteUri = "ftp://ftp.geomaticsyukon.ca/GeoYukon/Mining/Quartz_Claims_50k/";
            string fileName = "Quartz_Claims_50k.shp.zip";
            WebClient webClient = new WebClient();
            // Concatenate the domain with the Web resource filename:
            string webResource = remoteUri + fileName;
            //Console.WriteLine("Downloading File \"{0}\" from \"{1}\" ...\n\n", fileName, webResource);

            // Download the Web resource to a local file and save it into the current filesystem folder:
            string downloadedFileName; //the name of the file I'm creating to receive the data
            string timeOfDownload = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-");
            webClient.DownloadFile(webResource, downloadedFileName = timeOfDownload + fileName);  //technically this might be a second or 2 off from the previously generated string

            //Console.WriteLine("Successfully downloaded file \"{0}\" from \"{1}\"", downloadedFileName, webResource);

            //currently, at this point, the zip file is being generated in bin -> Debug. Move it to the desired folder:
            //string defaultLocation = @"C:\Users\Chloe\source\repos\MyPersonalProjects\NewQuartzClaimsQuerier\NewQuartzClaimsQuerier\bin\Debug\" + downloadedFileName;
            string defaultLocation = downloadedFileName;

            string newLocation = newLocationFolder + downloadedFileName;
            MoveFile(defaultLocation, newLocation);
            //Console.WriteLine("the start path will be:  " + newLocationFolder);
            return timeOfDownload;
        }

        public static void MoveFile(string oldPath, string newPath)
        {
            try
            {
                if (!File.Exists(oldPath))
                {
                    //ensure that the file is created, but the handle is not kept:
                    using (FileStream fs = File.Create(oldPath)) { }
                }

                //ensure that the target does not exist:
                if (File.Exists(newPath))
                    File.Delete(newPath);

                //move the file:
                File.Move(oldPath, newPath);
                //Console.WriteLine("\n {0} \n was successfully moved to: \n {1}.", oldPath, newPath);

                //ensure that the original no longer exists:
                if (File.Exists(oldPath)) Console.WriteLine("The original file still exists, which is unexpected.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        //extract the contents of the .zip file saved locally to a new directory:
        public static void UnzipFile(string startPath, string extractPath)
        {
            ZipFile.ExtractToDirectory(startPath, extractPath);
        }

        //within zip file just downloaded, create a DataTable from the dbf file:
        public static DataTable GetDataTableFromDbf(string dbfPath)
        {

            using (var table = Table.Open(dbfPath))
            {
                //
                var reader = table.OpenReader(Encoding.GetEncoding(1250));
                while (reader.Read())
                {
                    //
                }
            }
            using (var table = Table.Open(dbfPath))
                return table.AsDataTable();
        }
    }
}

