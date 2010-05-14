using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class Reports_AppliedReceipts : System.Web.UI.Page
{
    protected QuickPM.Property property;

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
        LabelSelectProperty.Visible = Request["PropertyId"] == null;
        PropertyList.Visible = Request["PropertyId"] == null;        
        if (!IsPostBack && Request["PropertyId"] == null)
        {
            List<QuickPM.Property> properties = QuickPM.Property.Util.GetProperties();
            foreach (QuickPM.Property prop in properties)
            {
                PropertyList.Items.Add(new ListItem(prop.Name + " (#" + prop.Id.ToString() + ")", prop.Id.ToString()));
            }
            if (PropertyList.Items.Count > 0)
            {
                PropertyList.Items[0].Selected = true;
            }
        }
        LoadProperty();
        if (!IsPostBack)
        {
            LoadListBoxRentTypes();
        }
    }
	
    private void LoadProperty()
    {
        if (PropertyList.SelectedValue == "" && Request["PropertyId"] == null)
        {
            return;
        }
        int PropertyId = 0;
        if (Request["PropertyId"] != null)
        {
            PropertyId = Int32.Parse(Request["PropertyId"]);
        }
        else
        {
            PropertyId = Int32.Parse(PropertyList.SelectedValue);
        }
        property = new QuickPM.Property(PropertyId);

        property = new QuickPM.Property(PropertyId);
        
    }
    protected void PropertyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadListBoxRentTypes();
    }


    private void LoadListBoxRentTypes()
    {        
        CheckBoxListRentTypes.Items.Clear();
        if (property == null)
        {
            return;
        }
        foreach (string rentType in property.RentTypes)
        {
            CheckBoxListRentTypes.Items.Add(new ListItem(rentType, property.RentTypes.IndexOf(rentType).ToString()));
        }
        if (property.RentTypes.Count > 0)
        {
            CheckBoxListRentTypes.Items[0].Selected = true;
        }
    }

    protected int GetBeginYear()
    {
        return Int32.Parse(DropDownListBeginYear.SelectedValue);
    }

    protected int GetBeginMonth()
    {
        return QuickPM.Util.ConvertMonthToInt(DropDownListBeginMonth.SelectedValue);
    }

    protected int GetEndYear()
    {
        return Int32.Parse(DropDownListEndYear.SelectedValue);
    }

    protected int GetEndMonth()
    {
        return QuickPM.Util.ConvertMonthToInt(DropDownListEndMonth.SelectedValue);
    }

    protected List<int> GetRentTypes()
    {
        List<int> rentTypes = new List<int>();
        foreach (ListItem item in CheckBoxListRentTypes.Items)
        {
            if (item.Selected)
            {
                rentTypes.Add(int.Parse(item.Value));
            }
        }
        return rentTypes;
    }

    protected string GenerateParams()
    {
        string p = "reportName=PropertyAppliedReceipts";
        p += "&beginmonth=" + GetBeginMonth() + "&beginyear=" + GetBeginYear() + "&endmonth=" + GetEndMonth() + "&endyear=" + GetEndYear();
        List<int> rentTypes = GetRentTypes();
        p += "&numrenttypes=" + rentTypes.Count;
        for (int i = 0; i < rentTypes.Count; i++ )
        {
            p += "&renttype" + i + "=" + rentTypes[i];
        }
        p += "&propertyid=" + property.Id;
        return p;
    }

    protected void ButtonGenerateReport_Click(object sender, EventArgs e)
    {
        QuickPM.Period beginPeriod = new QuickPM.Period(GetBeginYear(), GetBeginMonth());
        QuickPM.Period endPeriod = new QuickPM.Period(GetEndYear(), GetEndMonth());
        Session["AppliedReceiptsReportHtml"] = GenerateAgedReport.GenerateHtml(property, GetRentTypes(), beginPeriod, endPeriod);        
    }


    
    protected void PropertyList_TextChanged(object sender, EventArgs e)
    {
        LoadListBoxRentTypes();
    }
}
