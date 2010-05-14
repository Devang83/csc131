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

public partial class Billings_BillingChanges : System.Web.UI.Page
{

    protected List<string> selectedRentTypes = new List<string>();
    protected Dictionary<DateTime, List<string>> dict = new Dictionary<DateTime, List<string>>();

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (this.IsPostBack)
        {
            return;
        }
        DropDownListProperty.Items.Add(new ListItem("All", "All"));
        foreach (QuickPM.Property property in QuickPM.Property.Util.GetProperties())
        {            
            DropDownListProperty.Items.Add(new ListItem(property.Name + " (#" + property.Id + ")", property.Id.ToString()));
        }
        
        DropDownListProperty.SelectedIndex = 0;
        DropDownListProperty_SelectedIndexChanged(this, new EventArgs());
    }
    protected void DropDownListProperty_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<string> rentTypes = new List<string>();
        ListBoxRentTypes.Items.Clear();
        if (DropDownListProperty.SelectedIndex == 0)
        {
            foreach (QuickPM.Property property in QuickPM.Property.Util.GetProperties())
            {                
                foreach (string rentType in property.RentTypes)
                {
                    if (!rentTypes.Contains(rentType))
                    {
                        rentTypes.Add(rentType);
                    }
                }
            }
        }
        else
        {
            string sNumber = DropDownListProperty.SelectedValue;
            int PropertyId = Int32.Parse(sNumber);
            rentTypes = (new QuickPM.Property(PropertyId)).RentTypes;
        }
        foreach (string rentType in rentTypes)
        {
            ListBoxRentTypes.Items.Add(new ListItem(rentType));
        }
    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        int days = 30;
        if (!int.TryParse(TextBoxDays.Text, out days))
        {
            days = 30;
        }        
        List<QuickPM.Property> properties = new List<QuickPM.Property>();
        if (DropDownListProperty.SelectedValue == "All")
        {
            foreach (QuickPM.Property property in QuickPM.Property.Util.GetProperties())
            {
                properties.Add(property);
            }
        }
        else
        {
            properties.Add(new QuickPM.Property(Int32.Parse(DropDownListProperty.SelectedValue)));
        }
        selectedRentTypes.Clear();
        foreach (ListItem item in ListBoxRentTypes.Items)
        {
            if (!item.Selected)
            {
                continue;
            }
            selectedRentTypes.Add(item.Value);
        }

        List<DateTime> adjustments = FindChanges(properties);
        adjustments.Sort();
        string adjustHtml = "";
        List<string> printedTenantIds = new List<string>();
        int j = 0;
        while (j < adjustments.Count)
        {
            if (adjustments[j] > DateTime.Today + new TimeSpan(days, 0, 0, 0))
            {
                adjustments.RemoveAt(j);
            }
            else
            {
                j++;
            }
            
        }
        foreach (DateTime adjustmentDate in adjustments)
        {
            List<string> tenantIds = dict[adjustmentDate];
            foreach (string tenantId in tenantIds)
            {
                if (printedTenantIds.Contains(tenantId))
                {
                    continue;
                }
                printedTenantIds.Add(tenantId);
                QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
                adjustHtml += "<br />"
                    + "<a href=\"" + ResolveUrl("~/Tenants/TenantPage/" + tenant.Id) + "\">" + tenant.Name + "</a>";
                adjustHtml += "&nbsp;&nbsp;";
                adjustHtml += adjustmentDate.ToShortDateString();

            }
        }
        if (adjustHtml == "")
        {
            adjustHtml = "<h2>No changes</h2>";
        }
        Session["AdjustHtml"] = adjustHtml;
    }

    protected List<DateTime> FindChanges(List<QuickPM.Property> properties)
    {
        List<DateTime> adjustments = new List<DateTime>();
        foreach (QuickPM.Property property in properties)
        {
            //FindChanges(property)
            foreach (DateTime dateTime in FindChanges(property))
            {
                if (!adjustments.Contains(dateTime))
                {
                    adjustments.Add(dateTime);
                }
            }            
        }
        adjustments.Sort();        
        return adjustments;
    }

    protected List<DateTime> FindChanges(QuickPM.Property property)
    {
        List<string> tenantIds = property.GetTenantIds();
        List<DateTime> adjustments = new List<DateTime>();
        foreach (string tenantId in tenantIds)
        {

            if (FindChanges(tenantId).HasValue)
            {
                adjustments.Add(FindChanges(tenantId).Value);
            }
        }
        adjustments.Sort();
        return adjustments;
    }
    protected DateTime? FindChanges(string tenantId)
    {
        QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
        List<DateTime> adjustments = new List<DateTime>();
        foreach (string rentType in selectedRentTypes)
        {
            if (FindChanges(tenant, rentType).HasValue)
            {
                adjustments.Add(FindChanges(tenant, rentType).Value);
            }
        }
        adjustments.Sort();
        if (adjustments.Count == 0)
        {
            return null;
        }
        if (dict.ContainsKey(adjustments[0]))
        {
            List<string> tenantIds = dict[adjustments[0]];
            if (!tenantIds.Contains(tenantId))
            {
                dict[adjustments[0]].Add(tenantId);
            }
        }
        else
        {
            List<string> tmp = new List<string>();
            tmp.Add(tenantId);
            dict[adjustments[0]] = tmp;
        }
        return adjustments[0];        
    }

    protected DateTime? FindChanges(QuickPM.Tenant tenant, string rentType)
    {
        
        if (!tenant.RentTypes.Contains(rentType))
        {
            return null;
        }
        List<QuickPM.BillingRecord> billingRecords = QuickPM.BillingRecord.GetBillingRecords(tenant.TenantId, tenant.RentTypes.IndexOf(rentType));
        //List<DateTime> billingStartDates = new List<DateTime>();
        //List<DateTime> billingEndDates = new List<DateTime>();
        DateTime closestDate = DateTime.MaxValue;
        foreach (QuickPM.BillingRecord billingRecord in billingRecords)
        {
            if (billingRecord.StartDate >= DateTime.Today)
            {
                if (billingRecord.StartDate - DateTime.Today < closestDate - DateTime.Today)
                {
                    closestDate = billingRecord.StartDate;
                }
            }
            if (billingRecord.EndDate >= DateTime.Today)
            {
                if (billingRecord.EndDate - DateTime.Today < closestDate - DateTime.Today)
                {
                    closestDate = billingRecord.EndDate;
                }
            }
        }
        if (closestDate == DateTime.MaxValue)
        {
            return null;
        }
        return closestDate;
    }
}
