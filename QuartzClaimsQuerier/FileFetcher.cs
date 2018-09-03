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
        public static Tuple<string, DataTable> GetFileDownload(string remoteUriFileName, string dbfFileName)
        {

            WebClient webClient = new WebClient();
            //Console.WriteLine("Downloading File \"{0}\" from \"{1}\" ...\n\n", fileName, webResource);

            // Download the Web resource to a local file and save it into the current filesystem folder:
            //string downloadedFileName; //the name of the file I'm creating to receive the data
            string timeOfDownload = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-");

            //webClient.DownloadFile(webResource, downloadedFileName = timeOfDownload + fileName);  //technically this might be a second or 2 off from the previously generated string
            //Console.WriteLine("Successfully downloaded file \"{0}\" from \"{1}\"", downloadedFileName, webResource);

            DataTable table;

            using (MemoryStream stream = new MemoryStream(webClient.DownloadData(remoteUriFileName)))
            {

                table = GetDataTableFromStream(stream, dbfFileName);

            }

            //string defaultLocation = downloadedFileName;

            //string newLocation = newLocationFolder + downloadedFileName;
            //MoveFile(defaultLocation, newLocation);
           
            Tuple<string, DataTable> tuple = Tuple.Create(timeOfDownload, table);

            //return timeOfDownload;
            return tuple;
        }

        //public static void MoveFile(string oldPath, string newPath)
        //{
        //    try
        //    {
        //        if (!File.Exists(oldPath))
        //        {
        //            //ensure that the file is created, but the handle is not kept:
        //            using (FileStream fs = File.Create(oldPath)) { }
        //        }

        //        //ensure that the target does not exist:
        //        if (File.Exists(newPath))
        //            File.Delete(newPath);

        //        //move the file:
        //        File.Move(oldPath, newPath);
        //        //Console.WriteLine("\n {0} \n was successfully moved to: \n {1}.", oldPath, newPath);

        //        //ensure that the original no longer exists:
        //        if (File.Exists(oldPath)) Console.WriteLine("The original file still exists, which is unexpected.");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("The process failed: {0}", e.ToString());
        //    }
        //}

        //extract the contents of the .zip file saved locally to a new directory:
        //public static void UnzipFile(Stream stream)
        //{
        //    //ZipFile.ExtractToDirectory(startPath, extractPath);
        //}

        private static DataTable GetDataTableFromStream(Stream stream, string dbfFileName)
        {
            using (ZipArchive zip = new ZipArchive(stream))
            {        
                using (var unzippedStream = zip.GetEntry(dbfFileName).Open())
                {
                    var table = Table.Open(unzippedStream);
                    var dataTable = table.AsDataTable();
                    return dataTable;
                    
                }
            }

        }      
    }
}

