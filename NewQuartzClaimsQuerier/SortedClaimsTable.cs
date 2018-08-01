using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewQuartzClaimsQuerier
{
    class SortedClaimsTable
    {
        public int DaysSortedOn;
        public TableType TableType;
        public DataTable DataTable;

        public SortedClaimsTable(TableType tableType, int daysSortedOn, DataTable dataTable)
        {
            this.TableType = tableType;
            this.DaysSortedOn = daysSortedOn;
            this.DataTable = dataTable;
        }

        public string GetDisplayName(bool getFullDisplayName=false)
        {
            string pastOrFuture = TableType.DaysAreInPast ? "Last" : "Next";

            string displayNameIdentifier = DaysSortedOn.ToString() + " Days";  //need # of days to uniquely identify worksheet name in cases where more than 1 of the same table type are produced
            string displayNameStarter = TableType.Name + " in " + pastOrFuture + " ";
            string displayName = displayNameStarter + displayNameIdentifier;

            if (getFullDisplayName)  //the full name describing the claims table, regardless of how long it is, used to display in places where length doesn't matter
            {
                return displayName;
            }

            else if (displayName.Length <= 31)
            {
                //Console.WriteLine(displayName);
                //Console.ReadLine();
                return displayName;
            }
            else  //if character count too long: chop out the middle part. display name must be unique, hence keeping # of days at the end of the string.
            {
                //Console.WriteLine(displayName.Substring(0, 17) + "..." + displayNameIdentifier);
                //Console.ReadLine();
                return displayName.Substring(0, 17) + "..." + displayNameIdentifier;
            }
        }
        //public string GetFullDisplayName()
        //{
        //    string pastOrFuture = TableType.DaysAreInPast ? "Past" : "Future";

        //    string displayNameIdentifier = DaysSortedOn.ToString() + " Days";  //need # of days to uniquely identify worksheet name in cases where more than 1 of the same table type are produced
        //    string displayNameStarter = TableType.Name + " in " + pastOrFuture + " ";
        //    string displayName = displayNameStarter + displayNameIdentifier;

        //    if (displayName.Length <= 31)
        //    {
        //        //Console.WriteLine(displayName);
        //        //Console.ReadLine();
        //        return displayName;
        //    }
        //    else  //if character count too long: chop out the middle part. display name must be unique, hence keeping # of days at the end of the string.
        //    {
        //        //Console.WriteLine(displayName.Substring(0, 17) + "..." + displayNameIdentifier);
        //        //Console.ReadLine();
        //        return displayName.Substring(0, 17) + "..." + displayNameIdentifier;
        //    }
        //}
    }
}
