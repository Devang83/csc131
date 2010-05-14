using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class Property_ExpenseItem : System.Web.UI.Page
{
    protected QuickPM.Period beginPeriod = new QuickPM.Period(DateTime.Now.Year, DateTime.Now.Month);
    protected QuickPM.Period endPeriod = new QuickPM.Period(DateTime.Now.Year, DateTime.Now.Month);
    protected int PropertyId = 0;
    protected Guid expenseBudgetId;
    protected List<decimal> values = new List<decimal>();
    protected QuickPM.ExpenseItem ei = new QuickPM.ExpenseItem();
    protected bool newExpenseItem = true;
    protected QuickPM.ExpenseList eb = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        
        if (Request["beginyear"] == null || Request["beginmonth"] == null || Request["endyear"] == null || Request["endmonth"] == null ||
            Request["PropertyId"] == null || Request["EBId"] == null)
        {
            eb = new QuickPM.ExpenseList();
            endPeriod = beginPeriod.SubtractMonth();
            return;
        }
        int tmp;
        if (!Int32.TryParse(Request["beginyear"], out tmp) || !Int32.TryParse(Request["beginmonth"], out tmp) ||
            !Int32.TryParse(Request["endyear"], out tmp) || !Int32.TryParse(Request["endmonth"], out tmp) ||
            !Int32.TryParse(Request["PropertyId"], out tmp))
        {
            eb = new QuickPM.ExpenseList();
            endPeriod = beginPeriod.SubtractMonth();
            return;
        }


        beginPeriod.Year = Convert.ToInt32(Request["beginyear"]);
        beginPeriod.Month = Convert.ToInt32(Request["beginmonth"]);
        endPeriod.Year = Convert.ToInt32(Request["endyear"]);
        endPeriod.Month = Convert.ToInt32(Request["endmonth"]);
        PropertyId = Convert.ToInt32(Request["PropertyId"]);
        expenseBudgetId = new Guid(Request["EBId"]);
        ei.ChartOfAccount = 0;
        ei.COADescription = "";
        ei.IsSubCOA = false;
        ei.ParentCOA = 0;
        
        eb = new QuickPM.ExpenseList(expenseBudgetId);
        
        for (QuickPM.Period p = beginPeriod; p <= endPeriod; p = p.AddMonth())
        {
            ei.expenses.Add(0m);
            ei.periods.Add(p);
        }
        if (Request["COADescription"] != null)
        {            
            QuickPM.ExpenseItem expenseItem = null;
            foreach (QuickPM.ExpenseItem eei in eb.ExpenseItems)
            {
                if (eei.COADescription.Trim() == HttpUtility.UrlDecode(Request["COADescription"].Trim()))
                {                    
                    expenseItem = eei;
                    break;
                }
            }
            if (expenseItem != null)
            {
                newExpenseItem = false;
                ei = expenseItem;
                if (!IsPostBack)
                {
                    TextBoxCOA.Text = ei.ChartOfAccount.ToString();
                    TextBoxExpenseName.Text = ei.COADescription;
                }
            }
        }


        if (!IsPostBack)
        {
            CheckBoxSubaccount.Checked = ei.IsSubCOA;
            
            foreach (QuickPM.ExpenseItem i in eb.ExpenseItems)
            {
                if (i.IsSubCOA)
                {
                    continue;
                }
                ListItem li = new ListItem(i.COADescription, i.ChartOfAccount.ToString());
                if (ei.IsSubCOA && ei.ParentCOA == i.ChartOfAccount)
                {
                    li.Selected = true;
                }
                DropDownListAccounts.Items.Add(li);
            }
        }
        
    }

    protected void LinkButtonIncrease_Click(object sender, EventArgs e)
    {
        decimal increase = 0;
        if (decimal.TryParse(TextBoxIncrease.Text, out increase))
        {
            for (int p = 0; p < ei.expenses.Count; p++)
                {
                    ei.expenses[p] = (1 + increase / 100.00m) * ei.expenses[p];
                }
            eb.Save();
        }
        else
        {
            Session["Error"] = "<font color=\"red\">" + "Please enter an amount for the increase" + "</font>";
        }
    }

    protected void LinkButtonDecrease_Click(object sender, EventArgs e)
    {
        decimal decrease = 0;
        if (decimal.TryParse(TextBoxIncrease.Text, out decrease))
        {
            for (int p = 0; p < ei.expenses.Count; p++)
                {
                    ei.expenses[p] = (1 - decrease / 100.00m) * ei.expenses[p];
                }
            eb.Save();
        }
        else
        {
            Session["Error"] = "<font color=\"red\">" + "Please enter an amount for the decrease" + "</font>";
        }
    }


    protected void LinkButtonSubmit_Click(object sender, EventArgs e)
    {
        List<decimal> amounts = new List<decimal>();        
        int i = 0;
        ei.COADescription = TextBoxExpenseName.Text.Trim();
        ei.IsSubCOA = CheckBoxSubaccount.Checked;
        if (TextBoxCOA.Text.Trim() != "")
        {
            ei.ChartOfAccount = Int32.Parse(TextBoxCOA.Text);
        }
        else
        {
            int maxChartOfAccount = 0;
            foreach (QuickPM.ExpenseItem eitem in eb.ExpenseItems)
            {
                if (eitem.ChartOfAccount >= maxChartOfAccount)
                {
                    maxChartOfAccount = eitem.ChartOfAccount;
                }
            }
            ei.ChartOfAccount = maxChartOfAccount + 1;
        }
        if (CheckBoxSubaccount.Checked)
        {
            ei.ParentCOA = Convert.ToInt32(DropDownListAccounts.SelectedValue);
        }
        for (QuickPM.Period p = beginPeriod; p <= endPeriod; p = p.AddMonth())
        {
            string strAmount = Request.Form["Amount" + p.Month.ToString() + p.Year.ToString()];
            decimal amount = 0m;
            if (Decimal.TryParse(strAmount, System.Globalization.NumberStyles.Currency, System.Globalization.NumberFormatInfo.CurrentInfo, out amount))
            {
                amounts.Add(amount);
            }
            else
            {
                amounts.Add(0m);                
            }

            ei.expenses[i]= amount;
            //ei.periods.Add(p);
            i++;
        }
        QuickPM.ExpenseList.RootPath = Request.PhysicalApplicationPath + "/App_Data";        
        if (newExpenseItem)
        {
            eb.ExpenseItems.Add(ei);
        }
        eb.Save();
        Response.Redirect("ExpenseSpreadsheet.aspx?ebid=" + eb.Id.ToString() + "&PropertyId=" + eb.PropertyId);
    }
}
