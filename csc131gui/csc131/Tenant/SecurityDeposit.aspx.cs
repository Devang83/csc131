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

public partial class Tenant_SecurityDeposit : System.Web.UI.Page
{
    protected QuickPM.Tenant tenant = null;
    protected QuickPM.SecurityDeposit securityDeposit = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.MaintainScrollPositionOnPostBack = true;
		
        string tenantId = Request["TenantId"];		
        if (tenantId == null)
        {
            return;
        }        

        LoadTenant();

        if (!this.IsPostBack)
        {
            LoadTenantValues();
        }
        bool canWrite = tenant.ACL.CanWrite(QuickPM.Database.GetUserId());
        if (!canWrite)
        {
            QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
        }
        ButtonAddRefund.Visible = canWrite;
        ButtonAddRefund.Enabled = canWrite;
        ButtonSubmit.Visible = canWrite;
        ButtonSubmit.Enabled = canWrite;

        if (Request.Form["__EVENTTARGET"] == "DeleteRefund")
        {
            DeleteRefund(Request.Form["__EVENTARGUMENT"]);
        }

        if (Request.Form["__EVENTTARGET"] == "DeleteRequiredRefund")
        {
            DeleteRequiredRefund(Request.Form["__EVENTARGUMENT"]);
        }
    }

    private void DeleteRequiredRefund(string index)
    {
        int i = Int32.Parse(index);
        securityDeposit.RefundSchedule.RemoveAt(i);
        securityDeposit.Save();
    }

    private void DeleteRefund(string refundIndex)
    {
        int i = Int32.Parse(refundIndex);
        securityDeposit.Refunds.RemoveAt(i);
        securityDeposit.Save();
    }

    protected void ButtonAddRefund_Click(object sender, EventArgs e)
    {
        string checkNumber = TextBoxRefundCheckNumber.Text.Trim();
        decimal amount = 0m;
        if (!decimal.TryParse(TextBoxRefundAmount.Text, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out amount))
        {
            Session["Error"] = "<font color=\"red\">" + "Please enter a refund amount" + "</font>";
            return;
        }
        DateTime date;
        if (!DateTime.TryParse(TextBoxRefundDate.Text, out date))
        {
            Session["Error"] = "<font color=\"red\">" + "Please enter a refund date" + "</font>";
            return;
        }
        QuickPM.SecurityDepositRefund refund = new QuickPM.SecurityDepositRefund(checkNumber, amount, date);
        securityDeposit.Refunds.Add(refund);
        securityDeposit.Save();
    }

    protected void ButtonAddRequiredRefund_Click(object sender, EventArgs e)
    {
        decimal amount = 0m;
        if (!decimal.TryParse(TextBoxRequiredRefundAmount.Text, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out amount))
        {
            Session["Error"] = "<font color=\"red\">" + "Please enter a refund amount" + "</font>";
            return;
        }
        DateTime date;
        if (!DateTime.TryParse(TextBoxRequiredRefundDate.Text, out date))
        {
            Session["Error"] = "<font color=\"red\">" + "Please enter a refund date" + "</font>";
            return;
        }
        QuickPM.SecurityDepositRequiredRefund refund = new QuickPM.SecurityDepositRequiredRefund(amount, date);
        securityDeposit.RefundSchedule.Add(refund);
        securityDeposit.Save();
    }

    private void LoadTenantValues()
    {
        LoadTenant();
        LoadSecurityDeposit();
    }

    protected void LoadSecurityDeposit()
    {
        if (tenant == null)
        {
            return;
        }
        securityDeposit = new QuickPM.SecurityDeposit(tenant.TenantId);
        if (securityDeposit == null)
        {
            return;
        }

        //TextBoxRefundAmount.Text = securityDeposit.RefundAmount.ToString("c");
        //TextBoxRefundCheckNumber.Text = securityDeposit.RefundCheckNumber;
        //TextBoxRefundDate.Text = securityDeposit.RefundDate.ToShortDateString();
        TextBoxSecurityDepositAmount.Text = securityDeposit.DepositAmount.ToString("c");
        TextBoxSecurityDepositCheckAmount.Text = securityDeposit.CheckAmount.ToString("c");
        TextBoxSecurityDepositCheckDate.Text = securityDeposit.CheckDate.ToShortDateString();
        TextBoxSecurityDepositCheckNumber.Text = securityDeposit.CheckNumber;
        TextBoxSecurityDepositCheckReceivedDate.Text = securityDeposit.CheckReceivedDate.ToShortDateString();
        TextBoxNotes.Text = securityDeposit.Notes;
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
        securityDeposit = new QuickPM.SecurityDeposit(tenant.TenantId);
    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        if (tenant == null)
        {
            return;
        }
		
        securityDeposit = new QuickPM.SecurityDeposit(tenant.TenantId);
        decimal checkAmount = 0m;
		if (Decimal.TryParse(TextBoxSecurityDepositCheckAmount.Text, System.Globalization.NumberStyles.Any, 
		                 System.Globalization.NumberFormatInfo.CurrentInfo, out checkAmount))
		{
			securityDeposit.CheckAmount = checkAmount;
		} 
		else 
		{
			Session["Error"] = "<font color=\"red\">" + "Please enter an amount for the security deposit check." + "</font>";
		}
		
		DateTime checkDate = DateTime.Now;		
		securityDeposit.CheckDate = checkDate;
		if (DateTime.TryParse(TextBoxSecurityDepositCheckDate.Text, out checkDate))
		{
			securityDeposit.CheckDate = checkDate;
		} 
		else 
		{
			Session["Error"] = "<font color=\"red\">" + "Please enter a date for the security deposit check." + "</font>";
		}
		
		
        
        securityDeposit.CheckNumber = TextBoxSecurityDepositCheckNumber.Text.Trim();
        
		DateTime checkReceivedDate = DateTime.Now;
		securityDeposit.CheckReceivedDate = checkReceivedDate;
		if (DateTime.TryParse(TextBoxSecurityDepositCheckReceivedDate.Text, out checkReceivedDate))
		{
			securityDeposit.CheckReceivedDate = checkReceivedDate;
		} 
		else 
		{
			Session["Error"] = "<font color=\"red\">" + "Please enter a received date for the security deposit check." + "</font>";
		}
		
		decimal depositAmount = 0m;
		
		if (Decimal.TryParse(TextBoxSecurityDepositAmount.Text, System.Globalization.NumberStyles.Any, 
		                 System.Globalization.NumberFormatInfo.CurrentInfo, out depositAmount))
		{
			securityDeposit.DepositAmount = depositAmount;
		} else 
		{
			Session["Error"] = "<font color=\"red\">" + "Please enter a security deposit amount" + "</font>";
		}
		
        
        
		
		securityDeposit.Notes = TextBoxNotes.Text.Trim();
        //securityDeposit.RefundAmount = Decimal.Parse(TextBoxRefundAmount.Text);
        //securityDeposit.RefundCheckNumber = TextBoxRefundCheckNumber.Text;
        //securityDeposit.RefundDate = DateTime.Parse(TextBoxRefundDate.Text);
        securityDeposit.Save();
    }
}
