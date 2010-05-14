using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tenant_EditActuallyRefund : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sRefundIndex = Request["Index"];
        int refundIndex = 0;
        if (Request["TenantId"] == null || Request["Index"] == null)
        {
            return;
        }
        if (!Int32.TryParse(sRefundIndex, out refundIndex))
        {
            return;
        }
        QuickPM.Tenant tenant = new QuickPM.Tenant(Request["TenantId"]);
        bool canWrite = tenant.ACL.CanWrite(QuickPM.Database.GetUserId());
        if (!canWrite)
        {
            QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
            LinkButtonFinished.Visible = false;
        }
        if (!IsPostBack)
        {            
            QuickPM.SecurityDeposit secD = new QuickPM.SecurityDeposit(tenant.TenantId);
            TextBoxAmount.Text = secD.Refunds[refundIndex].Amount.ToString("c");
            TextBoxDate.Text = secD.Refunds[refundIndex].CheckDate.ToShortDateString();
            TextBoxCheckNumber.Text = secD.Refunds[refundIndex].CheckNumber;
        }
    }



    protected void LinkButtonSubmit_Click(object sender, EventArgs e)
    {
        string tenantId = Request["TenantId"];
        string sRefundIndex = Request["Index"];
        int refundIndex = 0;
        if (Request["TenantId"] == null || Request["Index"] == null)
        {
            return;
        }
        if (!Int32.TryParse(sRefundIndex, out refundIndex))
        {
            return;
        }


        QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
        QuickPM.SecurityDeposit secD = new QuickPM.SecurityDeposit(tenant.TenantId);
        string sAmount = TextBoxAmount.Text.Trim();
        string checkNumber = TextBoxCheckNumber.Text.Trim();
        decimal amount = 0m;
        if (!Decimal.TryParse(sAmount, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out amount))
        {
            return;
        }
        DateTime date;
        if (!DateTime.TryParse(TextBoxDate.Text, out date))
        {
            return;
        }
        secD.Refunds[refundIndex].Amount = amount;
        secD.Refunds[refundIndex].CheckDate = date;
        secD.Refunds[refundIndex].CheckNumber = checkNumber;
        secD.Save();
        Response.Redirect("SecurityDeposit.aspx?tenantid=" + tenant.TenantId);
    }
}
