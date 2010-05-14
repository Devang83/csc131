using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Web;
using VolunteerTracker.Exceptions;

namespace VolunteerTracker
{
    public class Util
    {
        public static string computer = System.Environment.MachineName;
        public static int numDigitsCenter = 4;
        public static int numDigitsTenant = 4;

        /*public static void SetConnectionStringHost(string host){
            computer = host;
            Util.user = user;
            dataSource = computer + "\\SQLEXPRESS";
            connectionString = "Integrated Security=SSPI;" +
                               "Initial Catalog=" + databaseName + ";" +
                               "Data Source=" + computer + "\\SQLEXPRESS;";
            
        }*/


		public static string FormatNumberAsOrdinal(int number)
    	{
        	if (number < 1)
        	{
            	throw new Exception("Error, cannot format " + number + " as an ordinal because it is less than 1");
        	}
        	if (number == 1)
        	{
            	return "1st";
        	}
        	else if (number == 2)
        	{
            	return "2nd";
        	}
        	else if (number == 3)
        	{
            	return "3rd";
        	}
        	else
        	{
            	return number.ToString() + "th";
        	}
    	}


        public static string Pluralize(string noun)
        {
            if (noun == "InsurancePolicy")
            {
                return "InsurancePolicies";
            }
            if (noun == "Person")
            {
                return "People";
            }
            if (noun == "Property")
            {
                return "Properties";
            }
			if (noun == "LeaseOptionSummary") 
			{
				return "LeaseOptionSummaries";
			}
            return noun + "s";
        }


        public static string HtmlHeader(string title)
        {
            return "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">\n"
                            + "<html xmlns=\"http://www.w3.org/1999/xhtml\">\n"
                            + "<head>\n"
                            + "<title>" + title + "</title>\n"
                            + "</head>\n"
                            + "<body>\n";
        }


        /// <summary>
        /// Convert a month name to the corresponding integer. Jan -> 1, Feb -> 2, ..., Dec -> 12
        /// </summary>
        /// <param name="month">The month name, for January valid month names are: Jan, Jan., or January. The format for other months 
        /// is the same.</param>
        /// <returns></returns>
        public static int ConvertMonthToInt(string month)
        {
            month = month.ToLower();
            if (month == "jan" || month == "jan." || month == "january")
                return 1;            
            if (month == "feb" || month == "feb." || month == "february")
                return 2;
            if (month == "mar" || month == "mar." || month == "march")
                return 3;
            if (month == "apr" || month == "apr." || month == "april")
                return 4;
            if (month == "may" || month == "may.")
                return 5;
            if (month == "jun" || month == "jun." || month == "june")
                return 6;
            if (month == "jul" || month == "jul." || month == "july")
                return 7;
            if (month == "aug" || month == "aug." || month == "august")
                return 8;
            if (month == "sep" || month == "sep." || month == "september")
                return 9;
            if (month == "oct" || month == "oct." || month == "october")
                return 10;
            if (month == "nov" || month == "nov." || month == "november")
                return 11;
            if (month == "dec" || month == "dec." || month == "december")
                return 12;
            throw new Exception("Error, invalid month string.");
        }

        /// <summary>
        /// Convert the given month integer to the corresponding string, examples 1->Jan, 12 -> Dec.
        /// </summary>
        /// <param name="month">Integer between 1 and 12</param>
        /// <returns>Three character string for the month.</returns>
        public static string ConvertMonthToString(int month)
        {
            if (month == 1)
                return "January";
            if (month == 2)
                return "February";
            if (month == 3)
                return "March";
            if (month == 4)
                return "April";
            if (month == 5)
                return "May";
            if (month == 6)
                return "June";
            if (month == 7)
                return "July";
            if (month == 8)
                return "August";
            if (month == 9)
                return "September";
            if (month == 10)
                return "October";
            if (month == 11)
                return "November";
            if (month == 12)
                return "December";

            throw new Exception("Error, month < 1 or month > 12, month:" + month.ToString());            
        }

		
		public static string CreateAssociatedId(VolunteerTracker.ActiveRecord obj)
		{
			return obj != null ? obj.GetType().Name + " " + obj.Id : "";			
		}
		
		public static VolunteerTracker.ActiveRecord GetAssociatedObject(string associatedId)
		{
			if (associatedId == null)
			{
				return null;
			}
			if (associatedId.Trim() == "")
			{
				return null;
			}
			string[] vals = associatedId.Split(new char[] {' '});
			if (vals.Length != 2)
			{
				return null;
			}
			string typeName = vals[0];
			long id = long.Parse(vals[1]);
			Type t = Type.GetType("VolunteerTracker." + typeName);
			System.Reflection.ConstructorInfo info = t.GetConstructor(new Type[] {typeof(long)});
			object[] param = new object[1];
			param[0] = id;						
			return (VolunteerTracker.ActiveRecord)info.Invoke(param);			
		}
		public static string GetSimpleTimeDifference(DateTime current, DateTime previous)
		{
			TimeSpan s = current - previous;
			int years = 0;
			if (s.Days > 365)
			{
				years = (int)(s.Days/365);
				s = new TimeSpan(s.Days % 365, s.Hours, s.Minutes, s.Seconds);				
			}
			string timestring = "";
			if (years != 0)
			{
				if (years > 1) 
					timestring += years + " years ";
				else 
					timestring += years + " year ";
			}
			if (s.Days != 0)
			{
				if (s.Days > 1)
				    timestring += s.Days + " days ";
				else
					timestring += s.Days + " day ";
			}
			if (years == 0 && s.Hours != 0)
			{
				if (s.Hours > 1)
					timestring += s.Hours + " hours ";
				else
					timestring += s.Hours + " hour ";
			}
			if (years == 0 && s.Days == 0 && s.Minutes != 0)
			{
				if (s.Minutes > 1)
					timestring += s.Minutes + " minutes ";
				else
					timestring += s.Minutes + " minute ";
			}
			if (years == 0 && s.Days == 0 && s.Hours == 0 && s.Seconds != 0)
			{
				if (s.Seconds > 1)
					timestring += s.Seconds + " seconds";
				else 
					timestring += s.Seconds + " second";
			}
			timestring += " ago";
			return timestring;
		}
		
        /// <summary>
        /// Make a US formatted date string, with day = 1.
        /// </summary>
        /// <param name="year">Date year.</param>
        /// <param name="month">Date month.</param>
        /// <returns>The date for the first day of the given month and year e.g. year = 2005, month = 2 yields
        /// "02/01/2005". </returns>
        public static string MakeDateString(int year, int month)
        {
            return MakeDateString(year, month, 1);
        }

        /// <summary>
        /// Make a standard US formatted date (i.e. year=2005, month=1 (Jan.), day = 2) yields 01/02/2005
        /// </summary>
        /// <param name="year">Date year.</param>
        /// <param name="month">Date month, Jan. = 1, Feb. = 2, etc.</param>
        /// <param name="day">Date day.</param>
        /// <returns>Returns a US formatted string for the date.</returns>
        public static string MakeDateString(int year, int month, int day)
        {
            return month.ToString() + "/" + day.ToString() + "/" + year.ToString();
        }

        public static DateTime ParseDate(string dateString)
        {
            //Assume US date format "mon/day/year".
            //where mon is two digits, day is two digits, and year is either two or four digits.
            string[] sep = dateString.Split(new char[] { '/' });
            if (sep.Length != 3)
                throw new Exception("Error wrong dateString format, string:" + dateString);
            int month = Convert.ToInt16(sep[0]);
            int day = Convert.ToInt16(sep[1]);
            int year = 0;
            if (sep[2].Length == 2)
                year = Convert.ToInt16("20" + sep[2]);
            else
                year = Convert.ToInt16(sep[2]);            
            return new DateTime(year, month, day);
        }

		

        static public List<long> SplitIds(string ids)
        {
            if (ids == "")
            {
                return new List<long>();
            }
            string[] _ids = ids.Split(new char[] { '|' });
            List<long> dIds = new List<long>();
            for (int i = 0; i < _ids.Length; i++)
            {
                dIds.Add(long.Parse(_ids[i]));
            }
            return dIds;
        }

        static public string StringifyIds(List<long> ids)
        {
            string sIds = "";
            if (ids == null)
            {
                return sIds;
            }
            foreach (long id in ids)
            {
                sIds += id.ToString() + "|";
            }
            sIds = sIds.Length > 0 ? sIds.Substring(0, sIds.Length - 1) : sIds;
            return sIds;
        }

        


		
		
        public static Decimal GetColumn(IDataReader reader, string column)
        {
            if (reader[column] == null || reader[column].GetType().ToString() == "System.DBNull")
            {
                return new Decimal(0);
            }
            object tmp = reader[column];
            System.Console.Write((Decimal)tmp);
            return (Decimal)(reader[column]);
        }


        /// <summary>
        /// Split a url that been used for an html GET posting into it's component variables.
        /// </summary>
        /// <param name="url">url with the incoded variables</param>
        /// <returns>hashtable of the variables</returns>
        public static Hashtable ParseUrl(string url)
        {
            Hashtable nameValuePairs = new Hashtable();
            string[] s = url.Split(new char[] { '?' });
            if (s.Length != 2)
                throw new Exception("Malformed url for parsing:" + url);
            string[] vars = s[1].Split(new char[] { '&' });
            for (int i = 0; i < vars.Length; i++)
            {
                string[] nameValue = vars[i].Split(new char[] { '=' });
                if (nameValue.Length != 2)
                {
                    throw new Exception("Malformed url for parsing:" + url);    
                }
                nameValuePairs[ParseUrlValue(nameValue[0])] = ParseUrlValue(nameValue[1]);
            }
            return nameValuePairs;
        }

        /// <summary>
        /// Decode a string that has been incoded for a html GET style form.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ParseUrlValue(string value)
        {           
            return System.Web.HttpUtility.UrlDecode(value);
        }

        /// <summary>
        /// Parse field string in the format as stored in the database.
        /// Example1: "Minimum Rent:900.00,Additional Rent:0.00"
        /// </summary>
        /// <param name="adjustments">The fields string</param>
        /// <returns>Dictionary of fields where key is the rent type and value
        /// is the amount.</returns>
        public static Dictionary<string, decimal> ParseFieldString(string fields)
        {
            Dictionary<string, decimal> field_values = new Dictionary<string, decimal>();
            if (fields.Trim() == "")
            {
                return field_values;
            }
            string[] ads = fields.Split(new char[] { ',' });
            for (int i = 0; i < ads.Length; i++)
            {
                string[] field = ads[i].Split(new char[] { ':' });
                field_values.Add(field[0].Trim(), Convert.ToDecimal(field[1]));
            }
            return field_values;
        }


        public static decimal CurrencyStringToDecimal(string money)
        {
            return Decimal.Parse(money, System.Globalization.NumberStyles.Any);
        }


       

      
    }
}
