using System;
using System.Collections.Generic;
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

public partial class Tenant_AgedARReport : System.Web.UI.Page
{
    protected QuickPM.Tenant tenant;
    protected List<Guid> tableIds = new List<Guid>();
    protected string html = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["TenantId"] == null)
        {
            Session["Error"] = "<font color=\"red\">" + "No tenant id" + "</font>";
        }
        else
        {
            tenant = new QuickPM.Tenant(Request["TenantId"]);
        }
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

    protected void ButtonGenerate_Click(object sender, EventArgs e)
    {
        int beginYear;
        if (!Int32.TryParse(DropDownListBeginYear.SelectedValue, out beginYear))
        {
            Session["Error"] = "<font color = \"red\">" + "Please enter the starting year" + "</font>";
            return;
        }
        int endYear;
        if (!Int32.TryParse(DropDownListEndYear.SelectedValue, out endYear))
        {
            Session["Error"] = "<font color = \"red\">" + "Please enter the ending year" + "</font>";
            return;
        }
        int beginMonth;
        if (!Int32.TryParse(DropDownListBeginMonth.SelectedValue, out beginMonth))
        {
            Session["Error"] = "<font color = \"red\">" + "Please enter the beginning month" + "</font>";
            return;
        }
        int endMonth;
        if (!Int32.TryParse(DropDownListEndMonth.SelectedValue, out endMonth))
        {
            Session["Error"] = "<font color = \"red\">" + "Please enter the ending month" + "</font>";
            return;
        }
        html = QuickPMWebsite.AppCode.Reports.TenantAgedAR(tenant, new QuickPM.Period(beginYear, beginMonth), new QuickPM.Period(endYear, endMonth));            
    }

    protected string GenerateParams()
    {
        int beginYear;
        if (!Int32.TryParse(DropDownListBeginYear.SelectedValue, out beginYear))
        {            
            return "";
        }
        int endYear;
        if (!Int32.TryParse(DropDownListEndYear.SelectedValue, out endYear))
        {            
            return "";
        }
        int beginMonth;
        if (!Int32.TryParse(DropDownListBeginMonth.SelectedValue, out beginMonth))
        {            
            return "";
        }
        int endMonth;
        if (!Int32.TryParse(DropDownListEndMonth.SelectedValue, out endMonth))
        {            
            return "";
        }

        return "reportName=TenantAgedAR" + "&" + "tenantid=" + tenant.TenantId + "&beginyear=" + beginYear + "&beginmonth=" + beginMonth + "&endyear=" + endYear + "&endmonth=" + endMonth;
    }
}
