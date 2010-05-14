using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

public partial class Property_ExpenseSpreadsheet : System.Web.UI.Page
{
    protected Guid id;
    int PropertyId = 0;
    protected QuickPM.ExpenseList eb = null;    
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        if (Request["ebid"] != null)
        {
            eb = new QuickPM.ExpenseList(new Guid(Request["ebid"]));
        }
        else
        {
            ParseRequest();
        }        

        if (Request.Form["__EVENTTARGET"] == "DeleteExpense")
        {
            DeleteExpense(Request.Form["__EVENTARGUMENT"]);
        }
        if (Request.Form["__EVENTTARGET"] == "SaveExpenses")
        {
            SaveExpenses(Request.Form["__EVENTARGUMENT"]);
        }
    }

    protected void ParseRequest()
    {
        if (Request["beginyear"] == null || Request["beginmonth"] == null || Request["endyear"] == null || Request["endmonth"] == null ||
               Request["PropertyId"] == null)
        {
            eb = new QuickPM.ExpenseList();
            eb.BeginPeriod = new QuickPM.Period(DateTime.Now.Year, DateTime.Now.Month);
            eb.BeginPeriod = eb.BeginPeriod.AddMonth();            
            return;
        }
        int tmp1;
        if (!Int32.TryParse(Request["beginyear"], out tmp1) || !Int32.TryParse(Request["beginmonth"], out tmp1) ||
            !Int32.TryParse(Request["endyear"], out tmp1) || !Int32.TryParse(Request["endmonth"], out tmp1) ||
            !Int32.TryParse(Request["PropertyId"], out tmp1))
        {
            eb = new QuickPM.ExpenseList();
            eb.BeginPeriod = new QuickPM.Period(DateTime.Now.Year, DateTime.Now.Month);
            eb.BeginPeriod = eb.BeginPeriod.AddMonth();                        
            return;
        }
        
        QuickPM.Period beginPeriod = new QuickPM.Period();
        QuickPM.Period endPeriod = new QuickPM.Period();
        beginPeriod.Year = Convert.ToInt32(Request["beginyear"]);
        beginPeriod.Month = Convert.ToInt32(Request["beginmonth"]);
        endPeriod.Year = Convert.ToInt32(Request["endyear"]);
        endPeriod.Month = Convert.ToInt32(Request["endmonth"]);        
        PropertyId = Convert.ToInt32(Request["PropertyId"]);
        bool isBudget = true;
        if (Request["IsBudget"] != null && (bool.TrueString.ToLower() == Request["IsBudget"].ToLower() || bool.FalseString.ToLower() == Request["IsBudget"].ToLower()))
        {
            isBudget = (bool.TrueString.ToLower() == Request["IsBudget"].ToLower());
        }
        eb = new QuickPM.ExpenseList(PropertyId, beginPeriod, endPeriod, isBudget);
    }

    protected void SaveExpenses(string sChartOfAccount)
    {

        int chartOfAccount;
        if (!Int32.TryParse(sChartOfAccount, out chartOfAccount))
        {
            return;
        }
        if (eb.ExpenseItems.Count == 0)
        {
            return;
        }

        QuickPM.ExpenseItem ei = null;

        for (int i = 0; i < eb.ExpenseItems.Count; i++)
        {
            if (eb.ExpenseItems[i].ChartOfAccount == chartOfAccount)
            {
                ei = eb.ExpenseItems[i];
                break;
            }
        }
        if (ei == null)
        {
            return;
        }

        for (int p = 0; p < eb.ExpenseItems[0].periods.Count; p++)
        {
            string expenseAmount = Request.Form[sChartOfAccount + "-" + p.ToString()];
            decimal eA;
            if(!Decimal.TryParse(expenseAmount, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out eA)){
                return;
            }
            ei.expenses[p] = eA;
        }
        eb.Save();
        Response.Redirect(Request.Url.PathAndQuery + "#" + chartOfAccount.ToString());
    }

    protected void LinkButtonIncrease_Click(object sender, EventArgs e)
    {
        decimal increase = 0;
        if (decimal.TryParse(TextBoxIncrease.Text, out increase))
        {
            foreach (QuickPM.ExpenseItem ei in eb.ExpenseItems)
            {
                for (int p = 0; p < ei.expenses.Count; p++)
                {
                    ei.expenses[p] = (1 + increase / 100.00m) * ei.expenses[p];
                }
                
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
            foreach (QuickPM.ExpenseItem ei in eb.ExpenseItems)
            {
                for (int p = 0; p < ei.expenses.Count; p++)
                {
                    ei.expenses[p] = (1 - decrease / 100.00m) * ei.expenses[p];
                }
            }
            eb.Save();
        }
        else
        {
            Session["Error"] = "<font color=\"red\">" + "Please enter an amount for the decrease" + "</font>";
        }
    }

    protected void DeleteExpense(string expenseItemCOADescription)
    {

        for (int i = 0; i < eb.ExpenseItems.Count; i++ )
        {
            QuickPM.ExpenseItem expenseItem = eb.ExpenseItems[i];
            if (expenseItem.COADescription.Trim() == expenseItemCOADescription.Trim())
            {
                eb.ExpenseItems.Remove(expenseItem);
                if (!expenseItem.IsSubCOA)
                {
                    foreach (QuickPM.ExpenseItem ei in eb.GetSubExpenseItems(expenseItem))
                    {
                        eb.ExpenseItems.Remove(ei);
                    }
                }
                eb.Save();
                break;
            }
        }
    }

    protected void LinkButtonAddExpenseItem_OnClick(object sender, EventArgs e)
    {
        eb.Save();
        Response.Redirect("ExpenseItem.aspx?beginyear=" + eb.BeginPeriod.Year + "&beginmonth=" + eb.BeginPeriod.Month + "&endyear=" + eb.EndPeriod.Year + "&endmonth=" + eb.EndPeriod.Month + "&PropertyId=" + PropertyId + "&ebid=" + eb.Id);
    }

    protected void LinkButtonDeleteAll_OnClick(object sender, EventArgs e)
    {
        eb.ExpenseItems.Clear();
        eb.Save();
        
    }

    protected string GetExpenseItemHtml(QuickPM.ExpenseItem ei, string postfixText, string color, List<decimal> expenseTotals, List<decimal> monthlyTotals)
    {
        string html = "";
        int i = eb.ExpenseItems.IndexOf(ei);
        //foreach(QuickPM.ExpenseItem sei in eb.GetSubExpenseItems(ei))
        //{
            string anchor = "";//ei.ChartOfAccount.ToString();
            color = color == "fff" ? "ddd" : "fff";
           
            
            html += "<tr>";
            string whitespace = (ei.IsSubCOA || postfixText != "" ? "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" : "");            
            html += "<td><a name=" + anchor + "></a>" + whitespace + ei.GetShortCOADescription() + postfixText + "(<a href=" +  EditExpenseItemUrl(ei) + ">edit</a>,";

            if (ei.IsSubCOA || postfixText == "")
            {
                html += "<a href=\"javascript:__doPostBack('DeleteExpense','" + ei.COADescription + "')\"" + "onclick=\"javascript: return confirm('Delete?')\">delete</a>";
            }
            html +=")</td>";           
            
            for (int p = 0; p < ei.expenses.Count; p++)
            {
                expenseTotals[i] += ei.expenses[p];
                monthlyTotals[p] += ei.expenses[p];
                html += "<td align=\"right\"><input type=\"text\" style=\"background-color:#";
                html += color + ";border-width:1px;border-style:ridge;border-color:Silver\" "; 
                html += "name=\"" + ei.ChartOfAccount.ToString() + "-" + p.ToString() + "\" ";
                html += "id=\"" + ei.ChartOfAccount.ToString() + "-" + p.ToString() + "\" ";
                html += "size=\"" + ei.expenses[p].ToString("c").Length + "\" ";
                html += "value=\"" + ei.expenses[p].ToString("c") + "\"/></td>";
            }

            html += "<td>";
            html += "<a href=\"javascript:__doPostBack('SaveExpenses','" + ei.ChartOfAccount + "')\" onclick=\"\">save</a></td>";
            html += "</tr>";
        //}
        return html;
    }

    
    protected void LinkButtonDone_OnClick(object sender, EventArgs e)
    {
        if (Request["IsBudget"] != null && Request["IsBudget"].ToLower() == "false")
        {
            Response.Redirect("ReconciliationSpreadsheet.aspx?" + "ebid=" + eb.Id.ToString() + "&PropertyId=" + PropertyId);
        }
        else
        {
            Response.Redirect("Budget.aspx?PropertyId=" + PropertyId + "&ebid=" + eb.Id.ToString());
        }
    }

    protected string EditExpenseItemUrl(QuickPM.ExpenseItem ei)
    {
        return "ExpenseItem.aspx?beginyear=" + eb.BeginPeriod.Year + "&beginmonth=" + eb.BeginPeriod.Month + "&endyear=" + eb.EndPeriod.Year + "&endmonth=" + eb.EndPeriod.Month + "&PropertyId=" + PropertyId + "&ebid=" + eb.Id + "&COADescription=" + HttpUtility.UrlEncode(ei.COADescription);
    }

    protected void ButtonUploadExpenses_Click(object sender, EventArgs e)
    {
        //FileUploadExpenses.FileContent;
        
        
        QuickPM.CsvExpenses csv = new QuickPM.CsvExpenses(FileUploadExpenses.FileContent, PropertyId, eb.BeginPeriod, eb.EndPeriod);
        QuickPM.ExpenseList b = csv.CreateExpenseBudget();
        foreach (QuickPM.ExpenseItem ei in b.ExpenseItems)
        {
            eb.ExpenseItems.Add(ei);
        }
        eb.Save();
    }
}
