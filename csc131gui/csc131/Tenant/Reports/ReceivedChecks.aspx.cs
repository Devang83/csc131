using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

public partial class Tenant_Reports_ReceivedChecks : System.Web.UI.Page
{
    protected string html = null;

    protected string GetTenantId()
    {
        string tenantId = "0000-0000";
        if (Request["TenantId"] != null && QuickPM.Util.TryFormatTenantId(Request["TenantId"], out tenantId))
        {
            return tenantId;
        }
        return "0000-0000";
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            for (int year = DateTime.Now.Year - 10; year <= DateTime.Now.Year + 10; year++)
            {

                ListItem item1 = new ListItem(year.ToString(), year.ToString());
                ListItem item2 = new ListItem(year.ToString(), year.ToString());
                if (year == DateTime.Now.Year)
                {
                    item1.Selected = true;
                    item2.Selected = true;
                }
                DropDownListBeginYear.Items.Add(item1);
                DropDownListEndYear.Items.Add(item2);
            }            
        }
    }

    protected QuickPM.Period GetBeginPeriod()
    {
        int beginYear;
        if (!Int32.TryParse(DropDownListBeginYear.SelectedValue, out beginYear))
        {
            Session["Error"] = "<font color = \"red\">" + "Please enter the starting year" + "</font>";
            return new QuickPM.Period();
        }
        int beginMonth;
        if (!Int32.TryParse(DropDownListBeginMonth.SelectedValue, out beginMonth))
        {
            Session["Error"] = "<font color = \"red\">" + "Please enter the beginning month" + "</font>";
            return new QuickPM.Period();
        }
        return new QuickPM.Period(beginYear, beginMonth);
    }

    protected QuickPM.Period GetEndPeriod()
    {
        int endYear;
        if (!Int32.TryParse(DropDownListEndYear.SelectedValue, out endYear))
        {
            Session["Error"] = "<font color = \"red\">" + "Please enter the ending year" + "</font>";
            return new QuickPM.Period();
        }
        int endMonth;
        if (!Int32.TryParse(DropDownListEndMonth.SelectedValue, out endMonth))
        {
            Session["Error"] = "<font color = \"red\">" + "Please enter the ending month" + "</font>";
            return new QuickPM.Period();
        }
        return new QuickPM.Period(endYear, endMonth);
        
    }

    protected void ButtonGenerate_Click(object sender, EventArgs e)
    {        
        html = GenerateReport(GetBeginPeriod(), GetEndPeriod());
    }

    protected string GenerateParams()
    {
        return "reportname=TenantReceivedChecks&" +
            "beginyear=" + GetBeginPeriod().Year + "&beginmonth=" + GetBeginPeriod().Month +
            "&endyear=" + GetEndPeriod().Year + "&endmonth=" + GetEndPeriod().Month +
            "&tenantid=" + GetTenantId();
    }

    protected string GenerateReport(QuickPM.Period beginPeriod, QuickPM.Period endPeriod){
        return QuickPMWebsite.AppCode.Reports.TenantReceivedChecks(GetTenantId(), beginPeriod, endPeriod, this.Page);
     }

}
