using System;
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
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Text;

public partial class Tenant_AR : System.Web.UI.Page 
{
    protected QuickPM.Tenant GetTenant()
    {
        string tenantId = Request.Params["TenantId"];
        QuickPM.Tenant tenant = null;
        if (tenantId != null)
        {
            tenant = new QuickPM.Tenant(QuickPM.Util.FormatTenantId(tenantId));            
        }
        else
        {
            tenant = new QuickPM.Tenant("9999-9999");
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
        QuickPM.Tenant tenant= GetTenant();
        if(tenant == null){
            return;
        }
        QuickPM.Period period = GetPeriod();
        if(period.Year == -1 || period.Month == -1)
        {
            return;
        }
        if (Request.Form["textareamemo"] != null)
        {
            QuickPM.ARRecord record = new QuickPM.ARRecord(tenant.TenantId, period.Year, period.Month);

            string memo = Request.Form["textareamemo"].Trim();
            foreach (string rentType in tenant.RentTypes)
            {                
				decimal amount;
				decimal.TryParse(Request["Adjustment" + rentType], System.Globalization.NumberStyles.Any, 
				                 System.Globalization.NumberFormatInfo.CurrentInfo, out amount);
                record.Adjustments[tenant.RentTypes.IndexOf(rentType)] = amount;//Decimal.Parse(Request.Form["Adjustment" + rentType], System.Globalization.NumberStyles.Any);                                
            }
            record.Memo = memo;
            record.Save();
        }
        //TextBoxMemo.Text = QuickPM.Database.GetARRecord(tenant.TenantId, period.Year, period.Month).Memo.Trim();        
        //TextBoxMemo.TextMode = TextBoxMode.MultiLine;
        //if (!Page.IsPostBack) {
        //    NameLabel.Text = Request.Params["Name"];         
        //}
    }    
    
}
