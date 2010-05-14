using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tenant_Map : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPM.Tenant tenant = GetTenant();
        if (tenant != null)
        {
            ((Maps_MapControl)Map).Places = new List<object>(new object[] { tenant });
        }
        ((Maps_MapControl)Map).ZoomLevel = 13;
    }


    protected QuickPM.Tenant GetTenant()
    {        
        if (Request["TenantId"] == null)
        {
            return null;
        }
        return Request["TenantId"] != null ? new QuickPM.Tenant(Request["TenantId"]) : null;
        
    }

 

}
