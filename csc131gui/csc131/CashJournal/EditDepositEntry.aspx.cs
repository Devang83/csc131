using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CashJournal_EditDepositEntry : System.Web.UI.Page
{

    protected QuickPM.Deposit deposit = null;
    protected int depositEntryIndex = -1;
    protected void Page_Load(object sender, EventArgs e)
    {        
        string depositId = Request["DepositId"];
        if(Request["DepositId"] == null || Request["Entry_Index"] == null)
        {
            return;
        }        
        if (!Int32.TryParse(Request["Entry_Index"], out depositEntryIndex))
        {
            return;
        }
        deposit = new QuickPM.Deposit(long.Parse(depositId));
        if (deposit.DepositEntries.Count <= depositEntryIndex)
        {
            return;
        }
        if (!IsPostBack)
        {
            QuickPM.DepositEntry de = deposit.DepositEntries[depositEntryIndex];
            TextBoxCheckDate.Text = de.TransactionDate.ToShortDateString();
            TextBoxCheckNumber.Text = de.TransactionId;
            TextBoxNotes.Text = de.Notes;
            TextBoxReceivedDate.Text = de.ReceivedDate.ToShortDateString();
            TextBoxAmount.Text = de.Amount.ToString("c");
            QuickPM.Property p = new QuickPM.Property(deposit.PropertyId);
            foreach (string tenantId in p.GetTenantIds())
            {
                QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
                ListItem item = new ListItem(tenant.Name + " (" + tenant.TenantId + ")", tenant.TenantId);
                if (tenant.TenantId == de.TenantId && de.HasTenantId)
                {
                    item.Selected = true;
                }
                DropDownListTenant.Items.Add(item);
            }

            ListItem it = new ListItem("(" + p.Name + ")", p.Id.ToString());
            if (!de.HasTenantId)
            {
                it.Selected = true;
            }
            DropDownListTenant.Items.Add(it);            
        }
    }

    protected void LinkButtonCancel_Click(object sender, EventArgs e)
    {
        QuickPM.DepositEntry de = deposit.DepositEntries[depositEntryIndex];
        if (de.Amount == 0 && !de.HasTenantId)
        {
            deposit.DepositEntries.Remove(de);
            deposit.Save();
        }
        Response.Redirect("CashJournal.aspx?month=" + deposit.DepositDate.Month + "&year=" + deposit.DepositDate.Year + "&PropertyId=" + deposit.PropertyId);
    }

    protected void LinkButtonSubmit_Click(object sender, EventArgs e)
    {
        QuickPM.DepositEntry de = deposit.DepositEntries[depositEntryIndex];
        if (deposit.Deposited)
        {
            if (de.HasTenantId)
            {
                deposit.DeleteFromAR(de);
            }
        }
        DateTime transactionDate = de.TransactionDate;
        if (DateTime.TryParse(TextBoxCheckDate.Text, out transactionDate))
        {
            de.TransactionDate = transactionDate;
        }
        de.TransactionId = TextBoxCheckNumber.Text.Trim();
        de.Notes = TextBoxNotes.Text.Trim();
        de.TenantId = DropDownListTenant.SelectedValue;
        de.HasTenantId = (DropDownListTenant.SelectedValue != deposit.PropertyId.ToString());
        DateTime receivedDate = de.ReceivedDate;
        if (DateTime.TryParse(TextBoxReceivedDate.Text, out receivedDate))
        {
            de.ReceivedDate = receivedDate;
        }
        decimal amount = de.Amount;
        if (decimal.TryParse(TextBoxAmount.Text, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out amount))
        {
            de.Amount = amount;
        }
        if (de.HasTenantId)
        {
            deposit.AddToAR(de);
        }
        deposit.Save();           
        Response.Redirect("CashJournal.aspx?month=" + deposit.DepositDate.Month + "&year=" + deposit.DepositDate.Year + "&PropertyId=" + deposit.PropertyId);
    }
}
