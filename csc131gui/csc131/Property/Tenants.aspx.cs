using System;
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

public partial class Property_Tenants : System.Web.UI.Page
{
    protected bool all = false;
    protected QuickPM.Property property = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        all = CheckBoxInactive.Checked;        
        long propertyId = 0;
        if (Request["PropertyId"] == null || !long.TryParse(Request["PropertyId"], out propertyId))
        {
            property = new QuickPM.Property();
        }
        else
        {
            property = new QuickPM.Property(propertyId);
        }
        bool canWrite = property.ACL.CanWrite(QuickPM.Database.GetUserId());
        LinkButtonAddTenant.Visible = canWrite;
        LinkButtonAddTenant.Enabled = canWrite;
    }

    protected void LinkButtonAddTenant_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Add/AddTenant.aspx?PropertyId=" + Request["PropertyId"]);
    }


}
