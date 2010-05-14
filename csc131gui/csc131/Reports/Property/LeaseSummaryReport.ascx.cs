using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Property_LeaseSummaryReport : System.Web.UI.UserControl
{
    protected string report = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        if (!IsPostBack && Request["PropertyId"] == null)
        {
            List<QuickPM.Property> properties = QuickPM.Property.Util.GetProperties();
            foreach (QuickPM.Property property in properties)
            {
                DropDownListProperty.Items.Add(new ListItem(property.Name + " (#" + property.Id + ")", property.Id.ToString()));
            }
        }
        if (Request["PropertyId"] != null)
        {
            int PropertyId;
            if (Int32.TryParse(Request["PropertyId"], out PropertyId))
            {
                report = CreateLeaseSummaries(PropertyId);
            }
        }
    }

    public long GetPropertyId()
    {
        string propertyId = DropDownListProperty.SelectedValue != "" ? DropDownListProperty.SelectedValue : Request["PropertyId"];
        if (propertyId != null)
        {
            return long.Parse(propertyId);
        }
        else
        {
            return -1;
        }
    }

    protected void Submit(object sender, EventArgs e)
    {
        if(DropDownListProperty.SelectedValue == null)
        {
            return;
        }
        report = CreateLeaseSummaries(GetPropertyId());
    }

    protected string GenerateParams()
    {
        string p = "reportName=PropertyLeaseSummaries";
        p += "&propertyid=" + GetPropertyId();
        return p;
    }


    private string CreateLeaseSummaries(long propertyId)
    {
        /*QuickPM.Lease lease = new QuickPM.Lease(tenantId);
        QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
        if (lease.NewLease)
        {            
            return tenant.Name + " (#" + tenant.TenantId + ")" + " has no lease on record!";
        }*/
        return "<div style=\"font-size:0.70em\">" + QuickPMWebsite.AppCode.Reports.PropertyLeaseSummaries(propertyId, this.Page) + "</div>";
    }
}
