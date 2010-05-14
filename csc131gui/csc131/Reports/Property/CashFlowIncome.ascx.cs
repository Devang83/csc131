using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_CashFlowIncome : System.Web.UI.UserControl
{

    protected List<decimal> moneyReceived = null;
    protected Dictionary<string, decimal> tenantPaid = null;
    protected List<decimal> amounts = new List<decimal>();
    protected List<string> names = new List<string>();
    protected List<long> months = null;
    protected QuickPM.Property property = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        List<QuickPM.Property> properties = QuickPM.Property.Util.GetProperties();
        foreach (QuickPM.Property property in properties)
        {
            this.PropertyList.Items.Add(new ListItem(property.Id + " " + property.Name));
        }
        if (Request["PropertyId"] != null)
        {
            this.PropertyList.Visible = false;
            this.Label1.Visible = false;
        }

        if (DropDownListBeginYear.Items.Count == 0)
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
    }

    protected void ButtonGenerateChart_Click(object sender, EventArgs e)
    {
        moneyReceived = new List<decimal>();
        tenantPaid = new Dictionary<string, decimal>();
        months = new List<long>();
        int beginYear = Convert.ToInt32(DropDownListBeginYear.SelectedValue);
        int beginMonth = QuickPM.Util.ConvertMonthToInt(DropDownListBeginMonth.SelectedValue);
        int endYear = Convert.ToInt32(DropDownListEndYear.SelectedValue);
        int endMonth = QuickPM.Util.ConvertMonthToInt(DropDownListEndMonth.SelectedValue);
        QuickPM.Period beginPeriod = new QuickPM.Period(beginYear, beginMonth);
        QuickPM.Period endPeriod = new QuickPM.Period(endYear, endMonth);
        if (endMonth == beginMonth && endYear == beginYear)
        {
            endPeriod = endPeriod.AddMonth();
        }
        int property;
        if (Request["PropertyId"] == null)
        {
            string selectedValue = this.PropertyList.SelectedValue;
            string[] tmp = selectedValue.Split(new char[] { ' ' });
            property = Convert.ToInt32(tmp[0]);
        }
        else
        {
            property = Convert.ToInt32(Request["PropertyId"]);
        }
        this.property = new QuickPM.Property(property);

        for (QuickPM.Period p = beginPeriod; p <= endPeriod; p = p.AddMonth())
        {
            months.Add((new DateTime(p.Year, p.Month, 1).Ticks) - (new DateTime(1970, 1, 1).Ticks));
            decimal received = 0;
			
            List<string> profileIds = new List<string>(QuickPM.Database.GetPropertyTenantIds(this.property.Id, p));
            foreach (string profileId in profileIds)
            {
                QuickPM.Tenant tenant = new QuickPM.Tenant(profileId);
                string shortName = tenant.GetShortName();
                List<QuickPM.Check> checks = QuickPM.Database.GetChecks(profileId, p);
                List<QuickPM.NSFCheck> nsfChecks = QuickPM.Database.GetNSFChecks(profileId, p);
                foreach (QuickPM.Check c in checks)
                {
                    received += c.Amount;
                    if (!tenantPaid.ContainsKey(shortName))
                    {
                        tenantPaid[shortName] = 0m;
                    }
                    tenantPaid[shortName] += c.Amount;                    
                }
                foreach (QuickPM.NSFCheck n in nsfChecks)
                {
                    received += n.Amount;
                    if (!tenantPaid.ContainsKey(shortName))
                    {
                        tenantPaid[shortName] = 0m;
                    }
                    tenantPaid[shortName] += n.Amount;
                }                
                
            }
            moneyReceived.Add(received);            
        }
        CreateTenantList();
     }

    private void CreateTenantList()
    {
        amounts = new List<decimal>();
        names = new List<string>();
        foreach (string tenantName in tenantPaid.Keys)
        {            
            InsertIntoList(tenantName, tenantPaid[tenantName]);            
        }
        CollapseList();
    }

    private void InsertIntoList(string tenantName, decimal amount)
    {
        if (amounts.Count == 0)
        {
            amounts.Add(amount);
            names.Add(tenantName);
        }
        for (int i = 0; i < amounts.Count; i++)
        {
            if (i == amounts.Count - 1)
            {
                if (amounts[i] > amount)
                {
                    amounts.Add(amount);
                    names.Add(tenantName);
                }
                else
                {
                    amounts.Insert(i, amount);
                    names.Insert(i, tenantName);
                }
                break;
            }
            if (amounts[i] < amount)
            {
                amounts.Insert(i, amount);
                names.Insert(i, tenantName);
                break;
            }
        }

    }

    private void CollapseList()
    {
        int numItems = 8;
        if (amounts.Count <= numItems)
        {
            return;
        }
        string name = "Other";
        decimal amount = 0m;
        
        while (amounts.Count > numItems - 1)
        {
            amount += amounts[amounts.Count - 1];
            amounts.RemoveAt(amounts.Count - 1);
            names.RemoveAt(names.Count - 1);
        }
        amounts.Add(amount);
        names.Add(name);
    }
	
	
	protected string GenerateFunction(List<decimal> amounts, List<string> names) 
	{
		string html = "$(function() {\n";
                    
        int ii = 0;
        for(int kk = 0; kk < amounts.Count; kk++){ 
                     
            html += "var " + "b" + ii.ToString() + " = [ \n";
            html += "[" + ii.ToString() + ", " + (amounts[kk]).ToString() + "]\n";
            html += "];\n";
            ii++;
                        //break;
         }
         html += "var d2 = [[0, 3], [4, 8], [8, 5], [9, 13]];\n";

                    
         html += "$.plot($(\"#barchart\"), [\n";
         int jj = 0; 
         for(int ll = 0; ll < names.Count; ll++) 
		 {                                 
         	html += "{ data: " + "b" + jj.ToString() + ", label : \"" + names[ll] + "\",\n";
            html += "bars: { show:true } }\n";
            jj++;
            if(jj <= names.Count -1) {
            	html += ",\n";
            }
          }    
          html += "], {yaxis : {\n";
     	  html += "tickFormatter: function(v, axis) {\n";
          html += "             return \"$\" + dojo.currency.format(v);\n";
		  html += "                                                                }\n";
          html += "                       }\n";
		  html += "                       });\n";                                    
          html += "});\n";
          return html;
	}
}
