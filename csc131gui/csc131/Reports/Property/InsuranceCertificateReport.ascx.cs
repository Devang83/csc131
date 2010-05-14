using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Property_InsuranceCertificateReport : System.Web.UI.UserControl
{
    protected string report = null;
    bool missing = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        if (!IsPostBack && Request["PropertyId"] == null)
        {
            List<QuickPM.Property> properties = QuickPM.Property.Util.GetProperties();
            foreach (QuickPM.Property p in properties)
            {
                DropDownListProperty.Items.Add(new ListItem(p.Name + " (# " + p.Id.ToString() + ")", p.Id.ToString()));
            }
        }
        if (Request["PropertyId"] != null)
        {
            long PropertyId;
            if (Int64.TryParse(Request["PropertyId"], out PropertyId))
            {
                report = GenerateReport(PropertyId);
            }
        }
    }



    protected string GenerateParams()
    {
        string p = "reportName=PropertyInsuranceCertificates";
        p += "&missing=" + missing.ToString();
        if (!missing)
        {
            p += "&propertyid=" + GetPropertyId();
        }
        return p;
    }

    protected long GetPropertyId()
    {
        string PropertyId = DropDownListProperty.SelectedValue != "" ? DropDownListProperty.SelectedValue : Request["PropertyId"];
        if (PropertyId == null)
        {
            return -1;
        }
        return long.Parse(PropertyId);
        
    }

    protected void Submit(object sender, EventArgs e)
    {
        report = GenerateReport(GetPropertyId());
    }

    protected void SubmitMissing(object sender, EventArgs e)
    {
        missing = true;
        report = GenerateMissing();        
    }

    string GenerateMissing()
    {
        return QuickPMWebsite.AppCode.Reports.PropertyInsuranceCertificatesMissing(this.Page);
    }

    string GenerateReport(long propertyId)
    {

        return QuickPMWebsite.AppCode.Reports.PropertyInsuranceCertificates(false, propertyId, this.Page);
    }

}
