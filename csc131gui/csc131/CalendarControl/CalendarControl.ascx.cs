using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CalendarControl_CalendarControl : System.Web.UI.UserControl
{

    public VolunteerTracker.Period GetCurrentPeriod()
    {
        VolunteerTracker.Period period = new VolunteerTracker.Period(DateTime.Now.Year, DateTime.Now.Month);
        if (GetDefaultPeriod != null)
        {
            period = GetDefaultPeriod();
        }
        int year, month;
        if (Request["Year"] != null && Int32.TryParse(Request["Year"], out year))
        {
            period.Year = year;
        }

        if (Request["Month"] != null && Int32.TryParse(Request["Month"], out month))
        {
            period.Month = month;
        }
        return period;
    }

    public static VolunteerTracker.Period GetDefault()
    {
        return new VolunteerTracker.Period(DateTime.Now.Year, DateTime.Now.Month);
    }

    
    public delegate VolunteerTracker.Period GetDefaultPeriodMethod();

    public GetDefaultPeriodMethod GetDefaultPeriod = GetDefault;

    protected void Page_Load(object sender, EventArgs e)
    {
        VolunteerTracker.Period period = GetCurrentPeriod();
        if (!IsPostBack)
        {
            DropDownListSelectMonth.Items.Clear();
            foreach (string month in VolunteerTracker.Constants.months)
            {
                ListItem item = new ListItem(month + " " + period.Year, VolunteerTracker.Util.ConvertMonthToInt(month) + ":" + period.Year);
                item.Selected = VolunteerTracker.Util.ConvertMonthToInt(month) == period.Month ? true : false;
                DropDownListSelectMonth.Items.Add(item);
            }
        }

        if (Request.Form["__EVENTTARGET"] == "PreviousPeriod")
        {
            if (PeriodChanged != null)
            {
                PeriodChanged();
            }

            Response.Redirect(GetPreviousMonthLink());
        }

        if (Request.Form["__EVENTTARGET"] == "NextPeriod")
        {
            if (PeriodChanged != null)
            {
                PeriodChanged();
            }
            Response.Redirect(GetNextMonthLink());
        }
    }

    protected string GetPreviousMonthLink()
    {
        return Request.Path + "?" + GetPeriodQuery(GetCurrentPeriod().SubtractMonth());
    }

    protected string GetNextMonthLink()
    {
        return Request.Path + "?" + GetPeriodQuery(GetCurrentPeriod().AddMonth());
    }

    protected string GetPeriodQuery(VolunteerTracker.Period period)
    {
        string queryString = "";
        bool hasMonth = false;
        bool hasYear = false;
        foreach (string query in Request.QueryString.Keys)
        {
            if (query.ToLower() == "month")
            {
                queryString += "month=" + period.Month + "&";
                hasMonth = true;
            }
            else if (query.ToLower() == "year")
            {
                queryString += "year=" + period.Year + "&";
                hasYear = true;
            }
            else
            {
                queryString += query + "=" + Request.QueryString[query] + "&";
            }
        }
        if (!hasMonth)
        {
            queryString += "month=" + period.Month + "&";
        }
        if (!hasYear)
        {
            queryString += "year=" + period.Year + "&";
        }
        queryString = queryString.Trim(new char[] { '&' });
        return queryString;
    }

    public delegate void PeriodChangedMethod();

    public PeriodChangedMethod PeriodChanged = null;


    protected void SelectMonth_TextChanged(object sender, EventArgs e)
    {
        string value = DropDownListSelectMonth.SelectedValue;
        if (value == null)
        {
            return;
        }
        string sMonth = value.Split(new char[] { ':' })[0];
        string sYear = value.Split(new char[] { ':' })[1];
        VolunteerTracker.Period period = new VolunteerTracker.Period(Int32.Parse(sYear), Int32.Parse(sMonth));
        if (PeriodChanged != null)
        {
            PeriodChanged();
        }
        Response.Redirect(Request.Path + "?" + GetPeriodQuery(period));
    }
}
