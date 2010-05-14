using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for SimpleDateParser
/// </summary>
/// 
public class SimpleDate 
{
    /// <summary>
    /// The year.
    /// </summary>
    public int Year
    {
        get;
        set;
    }

    /// <summary>
    /// The month. Acceptable values are 1 <= Month <= 12
    /// </summary>
    public int Month
    {
        get;
        set;
    }

    /// <summary>
    /// The day of the month. Acceptable values are 1 <= Day <= 31.
    /// </summary>
    public int Day
    {
        get;
        set;
    }

    public SimpleDate(int year, int month, int day)
    {
        if (month < 1 || month > 12)
        {
            throw new Exception("Invalid value for month. Month:" + month + " acceptable values are 1 <= month <= 12");
        }
        if (day < 1 || day > 31)
        {
            throw new Exception("Invalid value for day. Day:" + month + " acceptable values are 1 <= day <= 31");
        }
        Year = year;
        Month = month;
        Day = day;
    }
}
public class SimpleDateParser
{
    /// <summary>
    /// Returns a SimpleDate on success and null on failure.
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static SimpleDate Parse(string date)
    {

        Match m = Regex.Match(date, "([0-9]{1,2})(/|-)([0-9]{1,2})(/|-)([0-9]+)");
        if (m.Success)
        {
            int month = int.Parse(m.Groups[1].Value);
            int day = int.Parse(m.Groups[3].Value);
            if (month < 1 || month > 12 || day < 1 || day > 31)
            {
                return null;
            }
            return new SimpleDate(int.Parse(m.Groups[5].Value), month, day);
        }
        return null;        
    }
}
