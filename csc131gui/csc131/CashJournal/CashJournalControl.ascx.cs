using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CashJournal_CashJournalControl : System.Web.UI.UserControl
{
    protected long PropertyId;
    protected QuickPM.Period period = null;
    protected List<QuickPM.Deposit> deposits;

    public bool CanSelectProperty = true;

    private QuickPM.Property property = null;
    protected QuickPM.Property GetProperty()
    {
        if (property == null)
        {
            property = new QuickPM.Property(PropertyId);
        }
        return property;
    }

    protected QuickPM.Period GetCurrentPeriod()
    {
        int year, month;
        if (Request["Year"] != null && Int32.TryParse(Request["Year"], out year))
        {
            
        }
        else
        {
            year = DateTime.Now.Year;
        }

        if (Request["Month"] != null && Int32.TryParse(Request["Month"], out month))
        {
            
        }
        else
        {
            month = DateTime.Now.Month;
        }
        return new QuickPM.Period(year, month);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);

        
        if (QuickPM.Util.GetPropertyIds().Length == 0)
        {
            return;
        }

        long iPropertyId = QuickPM.Util.GetPropertyIds()[0];		
		this.PropertyId = iPropertyId;

        if (Request["PropertyId"] != null)
        {
            if(long.TryParse(Request["PropertyId"], out iPropertyId)){
				this.PropertyId = iPropertyId;
			}
			       
        }
		
        if (!IsPostBack)
        {
            foreach (int PropertyId in QuickPM.Util.GetPropertyIds())
            {
                QuickPM.Property p = new QuickPM.Property(PropertyId);
                ListItem item = new ListItem(p.Name + " (#" + PropertyId + ")", p.Id.ToString());
                if (p.Id == iPropertyId)
                {
                    item.Selected = true;
                }
                DropDownListProperty.Items.Add(item);
            }            
	    }
        period = null;
        period = GetCurrentPeriod();        
            QuickPM.Property pp = new QuickPM.Property(this.PropertyId);
            if (DropDownListTenant.Items.Count == 0 || QuickPM.Util.GetPropertyId(DropDownListTenant.Items[0].Value) != pp.Id)
            {
                DropDownListTenant.Items.Clear();
                foreach (string tenantId in pp.GetTenantIds())
                {
                    QuickPM.Tenant t = new QuickPM.Tenant(tenantId);
                    string PropertyId = QuickPM.Util.GetPropertyId(t.TenantId).ToString();
                    PropertyId = PropertyId.Length < 2 ? "0" + PropertyId : PropertyId;
                    string tenantNumber = t.TenantId.Split(new char[] { '-' })[1];


                    DropDownListTenant.Items.Add(new ListItem(t.GetShortName() + " (#" + PropertyId + "-" + tenantNumber + ")", t.TenantId));
                }
                DropDownListTenant.Items.Add(new ListItem("(" + pp.Name + ")", pp.Id.ToString()));

            }

            


            if (Request.Form["__EVENTTARGET"] == "DepositDateChanged")
            {
                DepositDateChanged(Request.Form["__EVENTARGUMENT"]);
            }

            if (Request.Form["__EVENTTARGET"] == "AddDepositEntry")
            {
                AddDepositEntry(Request.Form["__EVENTARGUMENT"]);
            }

            if (Request.Form["__EVENTTARGET"] == "DeleteDeposit")
            {
                DeleteDeposit(Request.Form["__EVENTARGUMENT"]);
            }

            if (Request.Form["__EVENTTARGET"] == "DeleteDepositEntry")
            {
                DeleteDepositEntry(Request.Form["__EVENTARGUMENT"]);
            }

            if (Request.Form["__EVENTTARGET"] == "ChangeQuickBooksImported")
            {
                ChangeQuickBooksImported(Request.Form["__EVENTARGUMENT"]);
            }            


            deposits = QuickPM.Deposit.GetDeposits(this.PropertyId, period.Year, period.Month);


            if (Request.Form["__EVENTTARGET"] == "FinishDeposit")
            {
                FinishDeposit(Request.Form["__EVENTARGUMENT"]);
            }
        
        //if(IsPostBack && period != null)
    }    

    protected void AddDepositEntry(string arg)
    {
        QuickPM.Deposit d = new QuickPM.Deposit(long.Parse(arg));
        QuickPM.DepositEntry de = new QuickPM.DepositEntry();
        de.HasTenantId = false;
        de.ReceivedDate = DateTime.Today;
        de.TransactionDate = DateTime.Today;
        d.DepositEntries.Add(de);
        d.Save();
        Response.Redirect(ResolveUrl("~/CashJournal/EditDepositEntry.aspx?depositId=" + d.Id.ToString() + "&entry_index=" + d.DepositEntries.IndexOf(de)));
    }

    protected void DepositDateChanged(string arg)
    {
        string[] val = arg.Split(new char[] { '|' });
        QuickPM.Deposit d = new QuickPM.Deposit(long.Parse(val[0]));
        d.DepositDate = new DateTime(d.DepositDate.Year, d.DepositDate.Month, Int32.Parse(val[1]));
        d.Save();
    }

    protected void ChangeQuickBooksImported(string par)
    {
        string[] values = par.Split(new char[] { '|' });
        bool imported = Boolean.Parse(values[0]);
        long id = long.Parse(values[1]);
        QuickPM.Deposit d = new QuickPM.Deposit(id);
        d.ImportedToQuickBooks = imported;
        d.Save();
    }

    protected void DeleteDeposit(string depositId)
    {
        QuickPM.Deposit d = new QuickPM.Deposit(long.Parse(depositId));
        d.DeleteFromAR();
        d.Delete();
    }

    protected void DeleteDepositEntry(string arg)
    {
        string[] vals = arg.Split(new char[] { ';' });        
        int depositEntryIndex;
        if (!int.TryParse(vals[1], out depositEntryIndex))
        {
            return;
        }
        QuickPM.Deposit deposit = new QuickPM.Deposit(long.Parse(vals[0]));
        deposit.DeleteFromAR(deposit.DepositEntries[depositEntryIndex]);
        deposit.DepositEntries.RemoveAt(depositEntryIndex);
        deposit.Save();
    }

    protected void LinkButtonAddDeposit_Click(object sender, EventArgs e)
    {
        long? depositId = AddEntry();
        if (depositId.HasValue)
        {
            FinishDeposit(depositId.Value.ToString());
        }
        ClearEntries();
    }

    protected void LinkButtonAdd_Click(object sender, EventArgs e)
    {
        AddEntry();
        ClearEntries();
    }

    protected void ClearEntries()
    {
        TextBoxAmount.Text = "";
        TextBoxCheckDate.Text = "";
        TextBoxCheckNumber.Text = "";
        TextBoxReceivedDate.Text = "";
        TextBoxNotes.Text = "";
    }

    protected long? AddEntry()
    {

        string tenantId = DropDownListTenant.SelectedValue;
        string cDate = TextBoxCheckDate.Text;
        string sAmount = TextBoxAmount.Text;
        string checkNumber = TextBoxCheckNumber.Text;
        string rDate = TextBoxReceivedDate.Text;
        string notes = TextBoxNotes.Text;
        int tmp = 0;
        string tmp3 = "";
        bool t2 = Int32.TryParse(tenantId, out tmp);
        if (!QuickPM.Util.TryFormatTenantId(tenantId, out tmp3) && !t2)
        {
            return null;
        }
        DateTime checkDate;
        if (!DateTime.TryParse(cDate, out checkDate))
        {
            return null;
        }
        decimal amount;
        if (!decimal.TryParse(sAmount, out amount))
        {
            return null;
        }

        DateTime receivedDate;
        if (!DateTime.TryParse(rDate, out receivedDate))
        {
            return null;
        }

        if (period == null)
        {
            return null;
        }

        QuickPM.Deposit deposit = new QuickPM.Deposit();
        deposit.PropertyId = PropertyId;
        deposit.DepositDate = DateTime.Today;
        if (period.Year != DateTime.Today.Year || period.Month != DateTime.Today.Month)
        {
            deposit.DepositDate = new DateTime(period.Year, period.Month, 1);
        }
        
        if (deposits.Count > 0)
        {
            foreach (QuickPM.Deposit d in deposits)
            {
                if (!d.Deposited)
                {
                    deposit = d;
                    break;
                }
            }
            
        }
        QuickPM.DepositEntry de = new QuickPM.DepositEntry();
        de.Amount = amount;
        de.Notes = notes;
        de.ReceivedDate = receivedDate;
        de.TenantId = tenantId;
        string result = "";		
        de.HasTenantId = QuickPM.Util.TryFormatTenantId(tenantId, out result);
        de.TransactionDate = checkDate;
        de.TransactionId = checkNumber;
        deposit.DepositEntries.Add(de);
        deposit.Save();
        deposits = QuickPM.Deposit.GetDeposits(PropertyId, period.Year, period.Month);
        return deposit.Id;
    }

    protected void FinishDeposit(string depositId)
    {

        if (deposits.Count == 0)
        {
            return;
        }
        QuickPM.Deposit deposit = null;
        foreach (QuickPM.Deposit d in deposits)
        {
            if (d.Deposited || d.Id != long.Parse(depositId))
            {
                continue;
            }
            deposit = d;
            break;            
        }
        if (deposit != null)
        {
            Response.Redirect(Page.ResolveClientUrl("~/CashJournal/AddingDeposit.aspx?DepositId=" + deposit.Id + "&DepositEntryIndex=0"));
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        string sPropertyId = DropDownListProperty.SelectedValue;	    
        if (!long.TryParse(sPropertyId, out PropertyId))
        {
		    throw new Exception("sPropertyId:" + sPropertyId);            
		    
        }

        QuickPM.Period period = GetCurrentPeriod();
	
        Response.Redirect(Request.Url.AbsolutePath + "?PropertyId=" + PropertyId + "&year=" + period.Year + "&month=" + period.Month);        
    }
}
