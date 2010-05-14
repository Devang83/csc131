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

public partial class Checks_ViewCheck : System.Web.UI.Page
{
    protected QuickPM.MonetaryTransaction mt = null;
    protected QuickPM.Tenant tenant = null;
    protected void Page_Load(object sender, EventArgs e)
    {        

        
        
        if (IsPostBack)
        {
            if (Request["Type"] == "check")
            {
                mt = new QuickPM.Check(long.Parse(Request["CheckId"].ToString()));
                tenant = new QuickPM.Tenant(mt.TenantId);
                
            }
            else if (Request["Type"] == "nsf")
            {
                mt = new QuickPM.NSFCheck(long.Parse(Request["CheckId"]));

                tenant = new QuickPM.Tenant(mt.TenantId);
                
            };
        }

        //if (CheckNumber.Text == "")
        //{
        if (!IsPostBack)
        {
            if (Request["Type"] == "check")
            {
                mt = new QuickPM.Check(long.Parse(Request["CheckId"]));
                tenant = new QuickPM.Tenant(mt.TenantId);
                Memo.Text = mt.Memo;
                CheckNumber.Text = mt.Number;
                CheckAmount.Text = mt.Amount.ToString("c");
                TextBoxCheckDate.Text = ((QuickPM.Check)mt).CheckDate.ToShortDateString();
                TextBoxReceivedDate.Text = ((QuickPM.Check)mt).ReceivedDate.ToShortDateString();
            }
            else if (Request["Type"] == "nsf")
            {
                mt = new QuickPM.NSFCheck(long.Parse(Request["CheckId"]));

                tenant = new QuickPM.Tenant(mt.TenantId);
                Memo.Text = mt.Memo;
                CheckNumber.Text = mt.Number;
                CheckAmount.Text = mt.Amount.ToString("c");
                TextBoxCheckDate.Text = ((QuickPM.NSFCheck)mt).CheckDate.ToShortDateString();
                TextBoxReceivedDate.Text = ((QuickPM.NSFCheck)mt).NSFDate.ToShortDateString();
            }
            
        }
        ButtonDeleteCheck.Text = "Delete Check";
        if (mt.MoneyOut())
        {
            ButtonDeleteCheck.Text = "Delete NSF Check";
        }
        UnapplyCheck.Visible = mt.AppliedTo.Count != 0;

        if (!mt.ACL.CanWrite(QuickPM.Database.GetUserId()))
        {
            QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
            UnapplyCheck.Enabled = false;
            UnapplyCheck.Visible = false;
            ApplyCheck.Enabled = false;
            ApplyCheck.Visible = false;
            SubmitButton.Enabled = false;
            SubmitButton.Visible = false;
            ButtonDeleteCheck.Enabled = false;
            ButtonDeleteCheck.Visible = false;
        }
    }
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        mt.Memo = Memo.Text;
        decimal result;
        if (Decimal.TryParse(CheckAmount.Text, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out result))
        {

            mt.Amount = Decimal.Parse(CheckAmount.Text, System.Globalization.NumberStyles.Any);
            mt.Number = CheckNumber.Text;
            if (mt is QuickPM.Check)
            {
                DateTime checkDate = new DateTime();
                if (!DateTime.TryParse(TextBoxCheckDate.Text, out checkDate))
                {                    
                    Session["Error"] = "<font color=\"red\">" + "Please enter a Check Date" + "</font>";
                }
                else
                {
                    ((QuickPM.Check)mt).CheckDate = checkDate;
                }
                DateTime receivedDate = new DateTime();
                if (!DateTime.TryParse(TextBoxReceivedDate.Text, out receivedDate))
                {
                    Session["Error"] = "<font color=\"red\">" + "Please enter a Received Date" + "</font>";
                }
                else
                {
                    ((QuickPM.Check)mt).ReceivedDate = receivedDate;
                }
            }
            if (mt is QuickPM.NSFCheck)
            {
                DateTime checkDate = new DateTime();
                if (!DateTime.TryParse(TextBoxCheckDate.Text, out checkDate))
                {
                    Session["Error"] = "<font color=\"red\">" + "Please enter a Check Date" + "</font>";
                }
                else
                {
                    ((QuickPM.NSFCheck)mt).CheckDate = checkDate;
                }
                DateTime nsfDate = new DateTime();
                if (!DateTime.TryParse(TextBoxReceivedDate.Text, out nsfDate))
                {
                    Session["Error"] = "<font color=\"red\">" + "Please enter a NSF Date" + "</font>";
                }
                else
                {
                    ((QuickPM.NSFCheck)mt).NSFDate = nsfDate;
                }
            }
            mt.Save();
            CheckAmount.Text = mt.Amount.ToString("c");
        }
        else
        {
            Session["Error"] = "<font color=\"red\">" + "Please enter a check amount" + "</font>";
            return;
        }
    }
    protected void ApplyCheck_Click(object sender, EventArgs e)
    {
        
        mt.Memo = Memo.Text;
        decimal result;
        if (Decimal.TryParse(CheckAmount.Text, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out result))
        {
            mt.Amount = result;                
        }
        mt.Number = CheckNumber.Text;
        mt.Save();
        Server.Transfer("ApplyCheck.aspx?checkid=" + mt.Id + "&type=" + Request["Type"], true);
    }

    protected void Unapply_Click(object sender, EventArgs e)
    {
        mt.ClearApplied();
        mt.Save();
    }

    protected void ButtonDeleteCheck_Click(object sender, EventArgs e)
    {
        mt.Delete();
        QuickPM.MonetaryTransaction.RefreshCache(mt.TenantId);
        Session["CheckDeleted"] = true;
        Response.Redirect(ResolveUrl("~/Tenant/AR.aspx?tenantid=") + mt.TenantId + "&year=" + mt.ARRecordDate.Year + "&month=" + mt.ARRecordDate.Month);
    }
}
