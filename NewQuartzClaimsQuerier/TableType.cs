using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewQuartzClaimsQuerier
{
    class TableType
    {
        //enums in c# don't allow properties so instead need to attach instances STATICLY to the class type 
        //would have had Days as int parameter if days were unchanging
        public static readonly TableType NEW_CLAIMS_TABLE = new TableType("New Claims Staked", true);  //in past X days
        public static readonly TableType EXPIRED_CLAIMS_TABLE = new TableType("Expired Claims", true); //in past X days
        public static readonly TableType UPCOMING_EXPIRING_CLAIMS_TABLE = new TableType("Upcoming Expiring Claims", false); //in next X days

        public string Name;
        public bool DaysAreInPast;
        
        public TableType(string name, bool daysAreInPast)
        {
            this.Name = name;
            this.DaysAreInPast = daysAreInPast;
        }

    }
}
