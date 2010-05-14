using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuickPMWebsite.CashJournal
{
    public partial class AddingDeposit : System.Web.UI.Page
    {
        protected int index = 0;
        protected QuickPM.Deposit deposit = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(Context.Profile, Request);
            index = GetDepositEntryIndex();
            deposit = GetDeposit();
            if (deposit.Id == -1)
            {
               Response.Redirect("CashJournal.aspx?month=" + DateTime.Today.Month + "&year=" + DateTime.Today.Year);
            }
            if (index == deposit.DepositEntries.Count || deposit.Id == -1)
            {
                deposit.Deposited = true;
				if(deposit.DepositDate.Year == DateTime.Now.Year && 
				   deposit.DepositDate.Month == DateTime.Now.Month)
				{
					deposit.DepositDate = new DateTime(deposit.DepositDate.Year, 
					                                       deposit.DepositDate.Month, DateTime.Now.Day);
				}
                deposit.Save();
                Response.Redirect("CashJournal.aspx?month=" + deposit.DepositDate.Month + "&year=" + deposit.DepositDate.Year + "&PropertyId=" + deposit.PropertyId);
            }
            else if(index < deposit.DepositEntries.Count)
            {				
                if(!deposit.AddToAR(deposit.DepositEntries[index]))
				{
					Session["ErrorMessage"] = "Could not add check#" + deposit.DepositEntries[index].TransactionId + ", it has already been added!";
				}
                deposit.Save();
                //index = index + 1;                
            }
        }

        protected string GetTenantId()
        {
            QuickPM.DepositEntry de = GetDepositEntry();
            if (de.HasTenantId)
            {
                return de.TenantId;
            }
            else
            {
                return "";
            }
        }

        protected QuickPM.DepositEntry GetDepositEntry()
        {
            return GetDeposit().DepositEntries[GetDepositEntryIndex()];
        }

        protected QuickPM.Deposit GetDeposit()
        {
            long depositId = -1;
            if (Request["DepositId"] == null)
            {
                return new QuickPM.Deposit();
            }
            if (!long.TryParse(Request["DepositId"], out depositId))
            {
                return new QuickPM.Deposit();
            }
            return new QuickPM.Deposit(depositId);
        }
        protected int GetDepositEntryIndex()
        {
            int index = 0;
            if (Request["DepositEntryIndex"] == null)
            {
                return 0;
            }
            if (!int.TryParse(Request["DepositEntryIndex"], out index))
            {
                return 0;
            }
            return index;            
        }
    }
}
