using System;
using System.Collections;
using System.Configuration;
using System.Data;
// using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
// using System.Xml.Linq;

public partial class AddCheck : System.Web.UI.Page
{
    protected string tenantId = "";
    protected int year = 0;
    protected int month = 0;
    protected string mtName = "Check";

    protected QuickPM.Tenant GetTenant()
    {
        if (tenantId == "" || tenantId == null)
        {
            string tId = "";
            if (Request["TenantId"] != null && QuickPM.Util.TryFormatTenantId(Request["TenantId"], out tId))
            {
                tenantId = tId;
                return new QuickPM.Tenant(tenantId);
            }
            else
            {
                return new QuickPM.Tenant("0000-0000");
            }
        }
        return new QuickPM.Tenant(tenantId);
    }
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Request["tenantid"] != null)
        {
            tenantId = Request["TenantId"];    
        }
        if (Request["Type"] != null)
        {

            if (Request["Type"].ToLower() == "nsf")
            {
                mtName = "NSF Check";
            }
            else if (Request["Type"].ToLower() == "check")
            {
                mtName = "Check";
            }
            else
            {
                throw new Exception("Unknown Monetary Transaction Type: " + Request["Type"]);
            }
        }
        QuickPM.Tenant tenant = GetTenant();
        if (!tenant.ACL.CanWrite(QuickPM.Database.GetUserId()))
        {
            Session["ChecksAddCheckError"] = "<font color=\"red\">We're sorry but you do not have permission to add a check. Please check with your administrator.</font>";
            buttonAddCheck.Visible = false;
            buttonAddCheck.Enabled = false;
            QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);

        }
    }
}
