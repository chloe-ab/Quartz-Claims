using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewQuartzClaimsQuerier  
{
    class DataFilterer  
    {
        public static FilteredClaimsTable GetNewClaimsTable(DataTable fullTable, int daysInPastToCheckForNewClaims = 30)  //default is 30
        {
            DateTime startOfDateRange = DateTime.Now.AddDays(-daysInPastToCheckForNewClaims);
            DateTime endOfDateRange = DateTime.Now;

            DataTable newClaimsTable = Filter(fullTable, TableType.STAKE_DATE, startOfDateRange, endOfDateRange);
            //Console.WriteLine("Recent claim was staked on: " + finalDT);
            FilteredClaimsTable table = new FilteredClaimsTable(TableType.NEW_CLAIMS_TABLE, daysInPastToCheckForNewClaims, newClaimsTable);

            Console.WriteLine("\nNumber of new claims staked in last " + Convert.ToString(daysInPastToCheckForNewClaims) + " days is: " + newClaimsTable.Rows.Count);
            return table;                              
        }

        public static FilteredClaimsTable GetPastExpiredClaimsTable(DataTable fullTable, int daysInPastToCheckForExpiredClaims = 30)
        {
            DateTime startOfDateRange = DateTime.Now.AddDays(-daysInPastToCheckForExpiredClaims);
            DateTime endOfDateRange = DateTime.Now;

            DataTable pastExpiredClaimsTable = Filter(fullTable, TableType.EXPIRY_DATE, startOfDateRange, endOfDateRange);
            //Console.WriteLine("Claim was recently expired on: " + finalDT);
            FilteredClaimsTable table = new FilteredClaimsTable(TableType.EXPIRED_CLAIMS_TABLE, daysInPastToCheckForExpiredClaims, pastExpiredClaimsTable);

            Console.WriteLine("\nNumber of claims that expired in last " + Convert.ToString(daysInPastToCheckForExpiredClaims) + " days is: " + Convert.ToString(pastExpiredClaimsTable.Rows.Count));
            return table;
        }
   
        public static FilteredClaimsTable GetUpcomingExpiringClaimsTable(DataTable fullTable, int daysInFutureToCheckForExpiry = 30)
        {
            DateTime startOfDateRange = DateTime.Now;
            DateTime endOfDateRange = DateTime.Now.AddDays(daysInFutureToCheckForExpiry);
        
            DataTable upcomingExpiringClaimsTable = Filter(fullTable, TableType.EXPIRY_DATE, startOfDateRange, endOfDateRange);
            //Console.WriteLine("Upcoming claim will expire on: " + finalDT);                                                       
            FilteredClaimsTable table = new FilteredClaimsTable(TableType.UPCOMING_EXPIRING_CLAIMS_TABLE, daysInFutureToCheckForExpiry, upcomingExpiringClaimsTable);

            Console.WriteLine("\nNumber of claims that will expire in next " + Convert.ToString(daysInFutureToCheckForExpiry) + " days is: " + Convert.ToString(upcomingExpiringClaimsTable.Rows.Count.ToString()));
            return table;
        }

        private static DataTable Filter(DataTable fullTable, string filteringColumn, DateTime startOfDateRange, DateTime endOfDateRange)
        {
            //Create a DataColumn object for the column I want to filter with:
            DataColumn stakeDate = fullTable.Columns[filteringColumn];

            //create a new clone of the full dataset to be filled with only the relevant data:
            DataTable table = fullTable.Clone();

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

                        //if date in question is within range
                        if (Convert.ToInt32(finalDT) >= Convert.ToInt32(startOfDateRange.ToString("yyyyMMdd")) && Convert.ToInt32(finalDT) <= Convert.ToInt32(endOfDateRange.ToString("yyyyMMdd")))
                        {
                            //include row in the filtered datatable
                            table.ImportRow(row);
                            //Console.WriteLine("Claim " + "staked//expired//expiring on: " + finalDT);
                            //add sorting functionality here: if a boolean is true, then sort
                        }
                    }
                }
            }
            return table; //returns the newly filtered DataTable
        }
    }
}








