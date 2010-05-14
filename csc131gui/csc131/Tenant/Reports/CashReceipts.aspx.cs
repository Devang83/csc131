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

public partial class Tenant_Reports_CashReceipts : System.Web.UI.Page
{
    protected string tableId = "";
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
            for (int year = DateTime.Now.Year - 5; year <= DateTime.Now.Year + 5; year++)
            {
                ListItem year1 = new ListItem(year.ToString());
                ListItem year2 = new ListItem(year.ToString());
                if (year == DateTime.Now.Year)
                {
                    year1.Selected = true;
                    year2.Selected = true;
                }
                DropDownListBeginYear.Items.Add(year1);
                DropDownListEndYear.Items.Add(year2);
            }
        }
        if (!IsPostBack)
        {
            
        }
        if (!IsPostBack)
        {
            LoadListBoxRentTypes();
        }
    }



    private void LoadListBoxRentTypes()
    {
        CheckBoxListRentTypes.Items.Clear();
        
        string tenantId = GetTenantId();
        QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);

        foreach (string rentType in tenant.RentTypes)
        {
            CheckBoxListRentTypes.Items.Add(rentType);
        }
        if (tenant.RentTypes.Count > 0)
        {
            CheckBoxListRentTypes.Items[0].Selected = true;
        }
    }
    protected void ButtonGenerateReport_Click(object sender, EventArgs e)
    {
        QuickPM.Period beginPeriod = new QuickPM.Period(Int32.Parse(DropDownListBeginYear.SelectedValue), QuickPM.Util.ConvertMonthToInt(DropDownListBeginMonth.SelectedValue));
        QuickPM.Period endPeriod = new QuickPM.Period(Int32.Parse(DropDownListEndYear.SelectedValue), QuickPM.Util.ConvertMonthToInt(DropDownListEndMonth.SelectedValue));
        List<string> rentTypes = new List<string>();
        foreach (ListItem item in CheckBoxListRentTypes.Items)
        {
            if (item.Selected)
            {
                rentTypes.Add(item.Text);
            }
        }
        html = GenerateReport(rentTypes, beginPeriod, endPeriod);
    }

    protected string GenerateParams()
    {
        string rentTypeIndices = "";
        QuickPM.Tenant tenant = new QuickPM.Tenant(GetTenantId());
        int i = 0;
        foreach (ListItem item in CheckBoxListRentTypes.Items)
        {
            if (item.Selected)
            {
                rentTypeIndices += "&" + "rentType" + i.ToString() + "=" + tenant.RentTypes.IndexOf(item.Value);
                i++;
            }
        }
        return "reportname=TenantCashReceipts&tenantid=" + GetTenantId() + "&" + "beginyear=" + DropDownListBeginYear.SelectedValue +
            "&" + "beginmonth=" + QuickPM.Util.ConvertMonthToInt(DropDownListBeginMonth.SelectedValue) + "&" + "endyear=" + DropDownListEndYear.SelectedValue +
            "&" + "endmonth=" + QuickPM.Util.ConvertMonthToInt(DropDownListEndMonth.SelectedValue) +
            "&" + "numrenttypes=" + i.ToString()
            + rentTypeIndices;
    }



    private string GenerateReport(List<string> rentTypes, QuickPM.Period beginPeriod, QuickPM.Period endPeriod)
    {
        QuickPM.Tenant tenant = new QuickPM.Tenant(GetTenantId());
        List<int> rentTypeIndices = new List<int>();
        foreach (string rentType in rentTypes)
        {
            rentTypeIndices.Add(tenant.RentTypes.IndexOf(rentType));
        }
        return QuickPMWebsite.AppCode.Reports.TenantCashReceipts(GetTenantId(), rentTypeIndices, beginPeriod.Year, 
            beginPeriod.Month, endPeriod.Year,
            endPeriod.Month);
    }
}
