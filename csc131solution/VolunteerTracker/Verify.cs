using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VolunteerTracker
{
    public class Verify
    {
        public static bool VerifyNumber(string str){
            if (str == "")
            {
                str = "0";
            }
            try
            {
                Convert.ToDouble(str);
            }
            catch (Exception e)
            {                
                return e != null; //just another way of saying false.
            }
            return true;
        }


        public static bool VerifyMoney(string s)
        {
            try
            {
                Decimal.Parse(s, System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (Exception e)
            {                
                return e != null; //just another way of saying false.
            }
        }

        public static bool VerifyAddRentSchedule(string TenantId, DateTime beginDate, DateTime endDate)
        {
            //The rent schedule is ordered by date.
            /*Hashtable[] rentSchedule = CMD.Util.QueryRentSchedule(TenantId, null);
            Hashtable endingSchedule = rentSchedule[rentSchedule.Length - 1];
            if (((DateTime)endingSchedule["EndDate"]).AddDays(1).ToString("yyyy-MM-dd") != beginDate.ToString("yyyy-MM-dd"))
            {
                return false;
            }
            if (endDate <= beginDate)
                return false;*/
            return true;
        }
    }
}
