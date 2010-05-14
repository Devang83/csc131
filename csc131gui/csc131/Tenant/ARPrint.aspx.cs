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

public partial class Tenant_ARPrint : System.Web.UI.Page
{
    protected QuickPM.Tenant GetTenant()
    {
        string tenantId = Request.Params["TenantId"];
        QuickPM.Tenant tenant = null;
        if (tenantId != null)
        {
            tenantId = QuickPM.Util.FormatTenantId(tenantId);

                tenant = new QuickPM.Tenant(tenantId);
            


        }
        return tenant;
    }



    protected QuickPM.Period GetPeriod()
    {
        string strYear = Request.Params["Year"];
        string strMonth = Request.Params["Month"];
        int year = -1;
        int month = -1;
        if (strYear != null && strMonth != null)
        {
            year = Convert.ToInt32(Request.Params["Year"]);
            month = Convert.ToInt32(Request.Params["Month"]);
        }
        else
        {
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
        }
        return new QuickPM.Period(year, month);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        QuickPM.Tenant tenant = GetTenant();
        if (tenant == null)
        {
            return;
        }
        QuickPM.Period period = GetPeriod();
        if (period.Year == -1 || period.Month == -1)
        {
            return;
        }

        if (Request.Form["textareamemo"] != null)
        {
            QuickPM.ARRecord record = new QuickPM.ARRecord(tenant.TenantId, period.Year, period.Month);

            string memo = Request.Form["textareamemo"].Trim();
            foreach (string rentType in tenant.RentTypes)
            {

                int rentTypeIndex = tenant.RentTypes.IndexOf(rentType);
                record.Adjustments[rentTypeIndex] = Decimal.Parse(Request.Form["Adjustment" + rentType], System.Globalization.NumberStyles.Any);
            }
            record.Memo = memo;
            record.Save();
        }
 
    }
}
