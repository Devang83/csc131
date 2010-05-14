using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

public partial class Property_Map : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {        
        long id = 0;
        if (Request["PropertyId"] != null && long.TryParse(Request["PropertyId"], out id))
        {
            List<string> tenantIds = new QuickPM.Property(id).GetTenantIds();
            List<object> places = new List<object>();
            foreach (string tenantId in tenantIds)
            {
                places.Add(new QuickPM.Tenant(tenantId));
            }
            ((Maps_MapControl)Map).Places = places;
        }
    }

    

    
}
