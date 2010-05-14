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

public partial class Tenant_Reports_AppliedReceipts : System.Web.UI.Page
{

    protected List<string> tableIds = new List<string>();
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
        string tenantId = GetTenantId();
        QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
        CheckBoxListRentTypes.Items.Clear();
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
        QuickPM.Tenant tenant = new QuickPM.Tenant(GetTenantId());
        List<int> rentTypeIndices = new List<int>();
        foreach (ListItem item in CheckBoxListRentTypes.Items)
        {
            if (item.Selected)
            {
                rentTypeIndices.Add(tenant.RentTypes.IndexOf(item.Value));
            }
        }
        html = QuickPMWebsite.AppCode.Reports.TenantAppliedReceipts(GetTenantId(), rentTypeIndices, 
            int.Parse(DropDownListBeginYear.SelectedValue), QuickPM.Util.ConvertMonthToInt(DropDownListBeginMonth.SelectedValue), 
            int.Parse(DropDownListEndYear.SelectedValue), QuickPM.Util.ConvertMonthToInt(DropDownListEndMonth.SelectedValue));                   
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
        return "reportname=TenantAppliedReceipts&tenantid=" + GetTenantId() + "&" + "beginyear=" + DropDownListBeginYear.SelectedValue +
            "&" + "beginmonth=" + QuickPM.Util.ConvertMonthToInt(DropDownListBeginMonth.SelectedValue) + "&" + "endyear=" + DropDownListEndYear.SelectedValue +
            "&" + "endmonth=" + QuickPM.Util.ConvertMonthToInt(DropDownListEndMonth.SelectedValue) +
            "&" + "numrenttypes=" + i.ToString()
            + rentTypeIndices;
    }



    protected void PropertyList_TextChanged(object sender, EventArgs e)
    {
        LoadListBoxRentTypes();
    }
}
