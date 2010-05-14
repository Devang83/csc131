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

public partial class Tenant_Documents : System.Web.UI.Page
{
    protected QuickPM.Tenant tenant = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.MaintainScrollPositionOnPostBack = true;

        LoadTenantValues();
        List<QuickPM.InsuranceCertificate> insuranceCertificates = QuickPM.InsuranceCertificate.GetInsuranceCertificates(tenant);
        if (insuranceCertificates.Count != 0)
        {
            ((Documents_DocumentsControl)DocumentsIC).DocumentIds = insuranceCertificates[0];
        }
        else
        {
            QuickPM.InsuranceCertificate ic = new QuickPM.InsuranceCertificate();
            ic.BeginDate = DateTime.MinValue;
            ic.EndDate = DateTime.MinValue;
            ic.SetAssociatedId(tenant);
            ic.Save();

            ((Documents_DocumentsControl)DocumentsIC).DocumentIds = ic;
        }
        ((Documents_DocumentsControl)DocumentsTenant).DocumentIds = this.tenant;        
    }

    private void LoadTenantValues()
    {
        LoadTenant();
        
    }


    protected void LoadTenant()
    {
        string tenantid = Request["TenantId"];
        if (tenantid == null)
        {
            return;
        }
        tenantid = QuickPM.Util.FormatTenantId(tenantid);
        tenant = new QuickPM.Tenant(tenantid);
    }

}
