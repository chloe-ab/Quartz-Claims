using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewQuartzClaimsQuerier  
{
    class DataSorter  
    {
        public static DataTable GetNewClaimsTable(DataTable fullTable, int daysInPastToCheckForNewClaims = 30)  //default is 30
        {
            //Create a DataColumn object for the column I want to filter with:
            DataColumn stakeDate = fullTable.Columns["STAKE_DATE"];

            //create a new clone of the full dataset to be filled with only the relevant data:
            DataTable newClaimsTable = fullTable.Clone();

            //this method using foreach may be a slower process for such a large dbf file. but it works for now.
            foreach (DataRow row in fullTable.Rows)
            {

                string nullableStakeDate = row.Field<DateTime?>(stakeDate).ToString();
                if (nullableStakeDate != null)
                {
                    DateTime parsedDT;
                    if (DateTime.TryParse(nullableStakeDate, out parsedDT))  //this ensures that only properly formatted dates make it in to the result set
                    {
                        DateTime newDT = DateTime.Parse(nullableStakeDate);
                        string finalDT = newDT.ToString("yyyyMMdd");   //deal with case where it's not null but not in the right format either?

                        if (Convert.ToInt32(finalDT) >= Convert.ToInt32(DateTime.Now.AddDays(-daysInPastToCheckForNewClaims).ToString("yyyyMMdd")))
                        {
                            //include row in the filtered datatable
                            newClaimsTable.ImportRow(row);
                            Console.WriteLine("Recent claim was staked on: " + finalDT);

                        }
                    }
                }
            }
            Console.WriteLine("\nNumber of new claims staked in last " + Convert.ToString(daysInPastToCheckForNewClaims) + " days is: " + newClaimsTable.Rows.Count);
            Console.ReadLine();
            return newClaimsTable;
        }

        public static DataTable GetPastExpiredClaimsTable(DataTable fullTable, int daysInPast = 30)
        {
            //Create a DataColumn object for the column I want to filter with:
            DataColumn expiryDate = fullTable.Columns["EXPIRY_DAT"];

            //create a new clone of the full dataset to be filled with only the relevant data:
            DataTable expiredClaimsTable = fullTable.Clone();

            foreach (DataRow row in fullTable.Rows)
            {

                string nullableExpDate = row.Field<DateTime?>(expiryDate).ToString();
                if(nullableExpDate != null)
                {
                    DateTime parsedDT;
                    if (DateTime.TryParse(nullableExpDate, out parsedDT))  //this ensures that only properly formatted dates make it in to the result set
                    {
                        DateTime newDT = DateTime.Parse(nullableExpDate);
                        string finalDT = newDT.ToString("yyyyMMdd");   //deal with case where it's not null but not in the right format either?

                        if (Convert.ToInt32(finalDT) >= Convert.ToInt32(DateTime.Now.AddDays(-daysInPast).ToString("yyyyMMdd")) && Convert.ToInt32(finalDT) <= Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")))
                        {
                            //include row in the filtered datatable
                            expiredClaimsTable.ImportRow(row);
                            Console.WriteLine("Claim was recently expired on: " + finalDT);

                        }
                    }
                }
            }
            Console.WriteLine("\nNumber of claims that expired in last " + Convert.ToString(daysInPast) + " days is: " + Convert.ToString(expiredClaimsTable.Rows.Count));
            Console.ReadLine();
            return expiredClaimsTable;      
        }

        public static DataTable GetUpcomingExpiredClaimsTable(DataTable fullTable, int daysInFutureToCheckForExpiry = 30)
        {
            //Create a DataColumn object for the column I want to filter with:
            DataColumn expiryDate = fullTable.Columns["EXPIRY_DAT"];

            //create a new clone of the full dataset to be filled with only the relevant data:
            DataTable upcomingExpiringClaimsTable = fullTable.Clone();

            foreach (DataRow row in fullTable.Rows)
            {
                string nullableExpDate = row.Field<DateTime?>(expiryDate).ToString();
                if (nullableExpDate != null)
                {
                    DateTime parsedDT;
                    if (DateTime.TryParse(nullableExpDate, out parsedDT))  //this ensures that only properly formatted dates make it in to the result set
                    {
                        DateTime newDT = DateTime.Parse(nullableExpDate);
                        string finalDT = newDT.ToString("yyyyMMdd");

                        if (Convert.ToInt32(finalDT) <= Convert.ToInt32(DateTime.Now.AddDays(daysInFutureToCheckForExpiry).ToString("yyyyMMdd")) && Convert.ToInt32(finalDT) > Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")))
                        {
                            //include row in the filtered datatable
                            upcomingExpiringClaimsTable.ImportRow(row);
                            Console.WriteLine("Upcoming claim will expire on: " + finalDT);
                        }
                    }
                }
            }
            Console.WriteLine("\nNumber of claims that will expire in next " + Convert.ToString(daysInFutureToCheckForExpiry) + " days is: " + Convert.ToString(upcomingExpiringClaimsTable.Rows.Count.ToString()));
            Console.ReadLine();
            return upcomingExpiringClaimsTable;
        }
    }
}








