using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Property_ReconciliationAndBudget_TenantRecon : System.Web.UI.Page
{
    protected QuickPM.TenantReconciliation tenRecon = null;
    protected QuickPM.ExpenseList el = null;
    protected QuickPM.Tenant tenant = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        if (Request["TenReconId"] == null)
        {
            return;
        }
        tenRecon = new QuickPM.TenantReconciliation(long.Parse(Request["TenReconId"]));
        el = new QuickPM.ExpenseList(tenRecon.ExpenseBudgetId);
        tenant = new QuickPM.Tenant(tenRecon.TenantId);
        
    }

    protected decimal GetMoniesCollected()
    {
        return GetMoniesCollected(el.BeginPeriod, el.EndPeriod);
    }

    protected decimal GetMoniesCollected(QuickPM.Period beginPeriod, QuickPM.Period endPeriod)
    {
        decimal moniesCollected = 0m;
        for (QuickPM.Period p = beginPeriod; p <= endPeriod; p = p.AddMonth())
        {
            QuickPM.ARRecord arRecord = new QuickPM.ARRecord(tenant.TenantId, p.Year, p.Month);
            
            foreach (string rentType in tenant.RentTypes)
            {
                moniesCollected += arRecord.Received(tenant.RentTypes.IndexOf(rentType));
            }            
        }
        return moniesCollected;
    }

    protected string GenerateExpenseTableHtml()
    {
        string html = "<table id=\"reconspreadsheet\">";
        html += "<tr>";
        html += "<th>";
        html += "Expense Item<br />Description";
        html += "</th>";
        List<decimal> totals = new List<decimal>();
        decimal total = 0m;
        for (QuickPM.Period p = el.BeginPeriod; p <= el.EndPeriod; p = p.AddMonth())
        { 
            html += "<th>" + p.ToString() + "</th>";
            totals.Add(0m);
        }
        html += "<th>Total</th>";
        html += "</tr>";
        foreach (QuickPM.ExpenseItem ei in el.ExpenseItems)
        {
            if (ei.IsSubCOA)
            {
                continue;
            }
            if (tenRecon.ExpenseParticipation.ContainsKey(ei.ChartOfAccount))
            {
                if (tenRecon.ExpenseParticipation[ei.ChartOfAccount] == QuickPM.TenantReconciliation.DoesntParticipate || 
                    tenRecon.ExpenseParticipation[ei.ChartOfAccount] == QuickPM.TenantReconciliation.LandlordPays)
                {
                    continue;
                }
            }
            else
            {
                continue;
            }
           
            html += "<tr>";
            html += "<td>" + ei.COADescription + "</td>";
            
            for (int period = 0; period < ei.expenses.Count; period++)
            {
                html += "<td>" + el.GetCOATotal(ei.ChartOfAccount, period).ToString("c") + "</td>";
                totals[period] += el.GetCOATotal(ei.ChartOfAccount, period);
            }
            html += "<td>" + el.GetCOATotal(ei.ChartOfAccount).ToString("c") + "</td>";
            
            total += el.GetCOATotal(ei.ChartOfAccount);
            html += "</tr>";
                     
        }
        html += "<tr>";
        html += "<td>Totals</td>";
        foreach (decimal expenseTotal in totals)
        {
            html += "<td>" + expenseTotal.ToString("c") + "</td>";
        }
        html += "<td>" + total.ToString("c") + "</td>";
        html += "</tr>";
        html += "</table>";
        return html;
    }
}
