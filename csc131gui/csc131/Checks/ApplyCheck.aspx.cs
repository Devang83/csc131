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

public partial class ApplyCheck : System.Web.UI.Page
{

//    bool isNSFCheck = false;
    
    public void PeriodChanged()
    {
        UpdateCheck();
    }


    public QuickPM.Period GetDefaultPeriod()
    {
        QuickPM.MonetaryTransaction mt = GetMonetaryTransaction();
        if (mt == null)
        {
            return new QuickPM.Period(DateTime.Now.Year, DateTime.Now.Month);
        }
        return new QuickPM.Period(mt.ARRecordDate.Year, mt.ARRecordDate.Month);
    }

    protected void Page_Load(object sender, EventArgs e)
    {                
        QuickPM.MonetaryTransaction mt = GetMonetaryTransaction();
        if (mt == null)
        {
            return;
        }
		
        ((CalendarControl_CalendarControl)ucCalendar).GetDefaultPeriod = GetDefaultPeriod;        
        ((CalendarControl_CalendarControl)ucCalendar).PeriodChanged = PeriodChanged;
        this.Title = "Apply Check ";
        UpdateCheck();
        if (!mt.ACL.CanWrite(QuickPM.Database.GetUserId()))
        {
            QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
            ButtonAutoApply.Enabled = false;
            ButtonAutoApply.Visible = false;
            
        }
    }


    private void UpdateCheck()
    {
        if (GetMonetaryTransaction() == null)
        {
            return;
        }
        QuickPM.Tenant tenant = new QuickPM.Tenant(GetMonetaryTransaction().TenantId);

        QuickPM.Period period = ((CalendarControl_CalendarControl)ucCalendar).GetCurrentPeriod();
        int year = period.Year;
        int month = period.Month;
        
        if (tenant.RentTypes.Count == 0)
        {
            return;
        }
        if (Request.Form[tenant.RentTypes[0] + "Amount"] != null)
        {
            QuickPM.MonetaryTransaction mt = GetMonetaryTransaction();
            //QuickPM.ARRecord arRecord = new QuickPM.ARRecord(tenant.TenantId, year, month);
            foreach (string rentType in tenant.RentTypes)
            {
                int rentTypeIndex = tenant.RentTypes.IndexOf(rentType);
                string txt = Request.Form[rentType + "Amount"].ToString();
                // a blank entry is assumed to be zero.
                if (txt == "")
                {
                    txt = "0.00";
                }
                decimal amount = Decimal.Parse(txt, System.Globalization.NumberStyles.Any);
                mt.ChangeAppliedTo(new QuickPM.Period(year, month), amount, rentTypeIndex);

                mt.Save();
            }
        }
    }

    protected QuickPM.MonetaryTransaction GetMonetaryTransaction()
    {
        QuickPM.MonetaryTransaction mt;
        if (Request["Type"] == null)
        {
            return null;
        }
        if (Request["Type"].ToLower() == "check")
        {
            mt = new QuickPM.Check(long.Parse(Request["CheckId"]));
        }
        else if (Request["Type"].ToLower() == "nsf")
        {
            mt = new QuickPM.NSFCheck(long.Parse(Request["CheckId"]));
        }
        else
        {
            throw new Exception("Unknown transaction type:" + Request["Type"]);
        }

        return mt;
    }

    protected void ButtonAutoApply_Click(object sender, EventArgs e)
    {
        QuickPM.MonetaryTransaction mt = GetMonetaryTransaction();
        if (mt == null)
        {
            return;
        }
        QuickPM.Period p = ((CalendarControl_CalendarControl)ucCalendar).GetCurrentPeriod();
        int year = p.Year;
        int month = p.Month;
		mt.ClearApplied();
		mt.Save();
        mt.AutoApply(new QuickPM.Period(year, month));
        mt.Save();        
    }
}
