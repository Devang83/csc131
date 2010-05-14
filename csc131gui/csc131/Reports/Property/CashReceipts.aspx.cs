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

public partial class Reports_CashReceipts : System.Web.UI.Page
{
    protected QuickPM.Property property;
    protected List<string> tableIds = new List<string>();



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
        if (PropertyList.SelectedValue == "")
        {
            return;
        }
        int PropertyId = Int32.Parse(PropertyList.SelectedValue);
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
			ListItem item = new ListItem(rentType, property.RentTypes.IndexOf(rentType).ToString());
            CheckBoxListRentTypes.Items.Add(item);
        }
        if (property.RentTypes.Count > 0)
        {
            CheckBoxListRentTypes.Items[0].Selected = true;
        }
    }

    protected QuickPM.Period GetBeginPeriod()
    {
        return new QuickPM.Period(Int32.Parse(DropDownListBeginYear.SelectedValue), QuickPM.Util.ConvertMonthToInt(DropDownListBeginMonth.SelectedValue));
    }

    protected QuickPM.Period GetEndPeriod()
    {
        return new QuickPM.Period(Int32.Parse(DropDownListEndYear.SelectedValue), QuickPM.Util.ConvertMonthToInt(DropDownListEndMonth.SelectedValue));
    }

    protected List<int> GetRentTypeIndices()
    {
        List<int> rentTypeIndices = new List<int>();
        foreach (ListItem item in CheckBoxListRentTypes.Items)
        {
            if (item.Selected)
            {
                rentTypeIndices.Add(int.Parse(item.Value));
            }
        }
        return rentTypeIndices;
    }

    protected string GenerateParams()
    {
        string p = "reportName=PropertyCashReceipts";
        int beginMonth = GetBeginPeriod().Month;
        int beginYear = GetBeginPeriod().Year;
        int endMonth = GetEndPeriod().Month;
        int endYear = GetEndPeriod().Year;
        p += "&beginmonth=" + beginMonth + "&beginyear=" + beginYear + "&endmonth=" + endMonth + "&endyear=" + endYear;
        List<int> rentTypes = GetRentTypeIndices();
        p += "&numrenttypes=" + rentTypes.Count;
        for (int i = 0; i < rentTypes.Count; i++)
        {
            p += "&renttype" + i + "=" + rentTypes[i];
        }
        p += "&propertyid=" + property.Id;
        return p;
    }

    protected void ButtonGenerateReport_Click(object sender, EventArgs e)
    {
        Session["CashReceiptsReportHtml"] = GenerateReport(property, GetRentTypeIndices(), GetBeginPeriod(), GetEndPeriod());
    }



    protected void PropertyList_TextChanged(object sender, EventArgs e)
    {
        LoadListBoxRentTypes();
    }

    private string GenerateReport(QuickPM.Property property, List<int> rentTypeIndices, QuickPM.Period beginPeriod, QuickPM.Period endPeriod)
    {
        return QuickPMWebsite.AppCode.Reports.PropertyCashReceipts(property, beginPeriod, endPeriod, rentTypeIndices);
    }
}
