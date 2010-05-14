using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Property_BudgetDates : System.Web.UI.Page
{
    QuickPM.Property property = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, this.Request);
        if (Request["PropertyId"] != null)
        {
            int number;
            if (Int32.TryParse(Request["PropertyId"], out number))
            {
                property = new QuickPM.Property(number);                
            }
        }
        if (!IsPostBack)
        {
            for (int year = DateTime.Today.Year - 5; year <= DateTime.Today.Year + 5; year++)
            {
                ListItem item = new ListItem(year.ToString());
                item.Selected = (year == DateTime.Today.Year + 1);
                DropDownListBeginYear.Items.Add(item);
                DropDownListEndYear.Items.Add(item);
            }
            DropDownListEndMonth.SelectedValue = "December";
        }
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        int beginMonth = QuickPM.Util.ConvertMonthToInt(DropDownListBeginMonth.SelectedValue);
        int endMonth = QuickPM.Util.ConvertMonthToInt(DropDownListEndMonth.SelectedValue);
        int beginYear = Convert.ToInt32(DropDownListBeginYear.SelectedValue);
        int endYear = Convert.ToInt32(DropDownListEndYear.SelectedValue);
        string isBudget = "true";
        if (Request["IsBudget"] != null)
        {
            isBudget = Request["IsBudget"];    
        }
        if (property == null)
        {
            return;
        }
        Response.Redirect(ResolveUrl("~/Property/ReconciliationAndBudget/ExpenseSpreadsheet.aspx") + "?beginyear=" + beginYear + "&beginmonth=" + beginMonth + "&endyear=" + endYear + "&endmonth=" + endMonth + "&PropertyId=" + property.Id + "&isbudget=" + isBudget);

        /*decimal expensePerMonth = 0m;
        if (decimal.TryParse(TextBoxMonthlyExpenses.Text, out expensePerMonth))
        {
            
            Session["BudgetExpenses"] = GenerateBudgetExpenses(expensePerMonth, new QuickPM.Period(beginYear, beginMonth), new QuickPM.Period(endYear, endMonth));
        }
        else
        {
            Session["Error"] = "<font color=\"red\">Please enter a monthly expense amount</font>";
            return;
        }
        Session["BudgetHtml"] = GenerateBudget(expensePerMonth, new QuickPM.Period(beginYear, beginMonth), new QuickPM.Period(endYear, endMonth));*/
    }

   

    
}
