using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteerTracker
{
    public class Period
    {
        int year;
        int month;

        public static bool operator <(Period p1, Period p2)
        {
            if (p1.Year < p2.Year)
            {
                return true;
            }
            if (p1.Year > p2.Year)
                return false;
            return p1.Month < p2.Month;
        }

        public override string ToString()
        {
            return VolunteerTracker.Util.ConvertMonthToString(month) + " " + year.ToString();
        }

        public static bool operator >(Period p1, Period p2)
        {
            if (p1.Year > p2.Year)
            {
                return true;
            }
            if (p1.Year < p2.Year)
                return false;
            return p1.Month > p2.Month;
        }

        public override bool Equals(object o)
        {
            if (o is Period)
            {
                return (year == ((Period)o).Year && month == ((Period)o).Month);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return 100 * year + month;
        }

        public static bool operator ==(Period p1, Period p2){
            return p1.Equals(p2);
        }

        public static bool operator !=(Period p1, Period p2)
        {
            return !(p1 == p2);
        }

        public static bool operator <=(Period p1, Period p2)
        {
            return p1 < p2 || (p1.Year == p2.Year && p1.Month == p2.Month);
        }

        public static bool operator >=(Period p1, Period p2)
        {
            return p1 > p2 || (p1.Year == p2.Year && p1.Month == p2.Month);
        }

        public Period(int year, int month)
        {
            this.year = year;
            this.month = month;
        }

        public Period()
        {
            this.year = DateTime.Now.Year;
            this.month = DateTime.Now.Month;
        }

        public Period AddMonths(int numMonths)
        {
            Period period = new Period(year, month);
            if (numMonths >= 0)
            {
                for (int i = 0; i < numMonths; i++)
                {
                    period = period.AddMonth();
                }
            }
            if (numMonths < 0)
            {
                for (int i = 0; i < -numMonths; i++)
                {
                    period = period.SubtractMonth();
                }
            }
            return period;
        }

        public Period AddMonth()
        {
            Period period = new Period(year, month);
            if (period.Month < 12)
            {
                period.Month += 1;
            }
            else
            {
                period.Month = 1;
                period.Year += 1;
            }
            return period;
        }

        public Period SubtractMonth()
        {
            Period period = new Period(year, month);
            if (period.Month > 1)
            {
                period.Month--;
                return period;
            }
            period.Month = 12;
            period.Year--;
            return period;
        }

        public Period AddYears(int numYears)
        {
            Period p = new Period(year, month);
            p.Year += numYears;
            return p;
        }
        
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }

        public int Month
        {
            get
            {
                return month;
            }
            set
            {
                month = value;
            }
        }
    }
}
