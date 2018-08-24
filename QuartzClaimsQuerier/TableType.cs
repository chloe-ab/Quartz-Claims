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
        public static readonly string STAKE_DATE = "STAKE_DATE";   //THESE HAD TO BE ABOVE THE INSTANCES DEFINED BELOW
        public static readonly string EXPIRY_DATE = "EXPIRY_DAT";   //THESE HAD TO BE ABOVE THE INSTANCES DEFINED BELOW
        public static readonly string LABEL = "LABEL";

        public static readonly TableType NEW_CLAIMS_TABLE = new TableType("New Claims Staked", true, STAKE_DATE);  //in past X days
        public static readonly TableType EXPIRED_CLAIMS_TABLE = new TableType("Expired Claims", true, EXPIRY_DATE); //in past X days
        public static readonly TableType UPCOMING_EXPIRING_CLAIMS_TABLE = new TableType("Upcoming Expiring Claims", false, EXPIRY_DATE); //in next X days

        public string Name;
        public bool DaysAreInPast;
        public string DefiningDateColumn;
        
        public TableType(string name, bool daysAreInPast, string definingDateColumn)
        {
            this.Name = name;
            this.DaysAreInPast = daysAreInPast;
            this.DefiningDateColumn = definingDateColumn;
        }
    }
}
