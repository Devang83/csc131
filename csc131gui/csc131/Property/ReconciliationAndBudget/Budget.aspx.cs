using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Property_Budget : System.Web.UI.Page
{
    protected QuickPM.Property property = null;
    protected List<Guid> tableIds = new List<Guid>();
    protected QuickPM.ExpenseList eb = null;
    string incomeCSV = "";
    string netCSV = "";
    string expensesCSV = "";
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
        if (Request["EBId"] == null)
        {
            return;
        }
        eb = new QuickPM.ExpenseList(new Guid(Request["EBId"]));

        if (!IsPostBack)
        {
            Session["BudgetExpenses"] = GenerateBudgetExpenses();
            Session["BudgetIncome"] = GenerateBudget();
        }
    }


    protected void SaveAsCSV_OnClick(object sender, EventArgs e)
    {
        string guid = Guid.NewGuid().ToString();
        string fileName = Request.PhysicalApplicationPath + "/App_Data/Tmp/" + guid + ".csv";
        StreamWriter s = new StreamWriter(fileName);
        //doc.Data = new UTF8Encoding(true).GetBytes("test");
        GenerateBudget();
        GenerateBudgetExpenses();
        s.Write("INCOME" + System.Environment.NewLine + incomeCSV + System.Environment.NewLine + expensesCSV + System.Environment.NewLine + netCSV);
        s.Close();
        FileStream file = new FileStream(fileName, FileMode.Open);
        
        long fileLength = file.Length;
        file.Close();
        
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=" + eb.PropertyId.ToString() + "-Budget.csv");
        Response.AddHeader("Content-Length", fileLength.ToString());
        Response.ContentType = "application/excel";
        Response.WriteFile(fileName);
        Response.End();
        File.Delete(fileName);
    }
    

    protected string GenerateBudgetExpenses()
    {
        Guid tableId = Guid.NewGuid();
        string html = "<table cellpadding=\"10px\" id = \"" + tableId.ToString() + "\" cellborder=\"0px\" cellspacing=\"0px\"><tr><th>Expense</th>";
        tableIds.Add(tableId);
        string csv = "\"Expense\",";
        for (QuickPM.Period p = eb.BeginPeriod; p <= eb.EndPeriod; p = p.AddMonth())
        { 
            html += "<th>" + p.Month.ToString() + "/" + p.Year.ToString() + "</th>";
            csv += p.Month.ToString() + "/" + p.Year.ToString() + ", ";
        }
        html += "<th>Expense Total</th>";
        csv += "Expense Total";
        csv += System.Environment.NewLine;
        html += "</tr>";
        int numberOfMonths = 12;
        if (eb.ExpenseItems.Count > 0)
        {
            numberOfMonths = eb.ExpenseItems[0].expenses.Count;
        }
        System.Collections.Generic.List<decimal> monthlyTotals = new System.Collections.Generic.List<decimal>(numberOfMonths);
        System.Collections.Generic.List<decimal> expenseTotals = new System.Collections.Generic.List<decimal>(eb.ExpenseItems.Count);
        for (int i = 0; i < numberOfMonths; i++)
        {
            monthlyTotals.Add(0m);
        }
        for (int i = 0; i < eb.ExpenseItems.Count; i++)
        {
            expenseTotals.Add(0m);
        }
        for (int i = 0; i < eb.ExpenseItems.Count; i++) {
    
            QuickPM.ExpenseItem ei = eb.ExpenseItems[i];
            if (ei.IsSubCOA)
            {
                continue;
            }
            html += "<tr> <td>" +  ei.COADescription + "</td>";
            csv += "\"" + ei.COADescription.Replace("\"", "\"\"") + "\",";
     
            for (int p = 0; p < ei.expenses.Count; p++)
            {
                decimal exp = 0m;

                for (int j = 0; j < eb.ExpenseItems.Count; j++)
                {
                    if (j == i) continue;
                    if (eb.ExpenseItems[j].IsSubCOA && eb.ExpenseItems[j].ParentCOA == ei.ChartOfAccount)
                    {
                        exp += eb.ExpenseItems[j].expenses[p];
                    }
                }
            
                expenseTotals[i] += exp;//ei.expenses[p];
                monthlyTotals[p] += exp;//ei.expenses[p];
             
                html += "<td>" + exp.ToString("c") + "</td>";
                csv += "" + exp.ToString() + ",";
            }
           
            html += "<td>" + expenseTotals[i].ToString("c") + "</td>";
            csv += "" + expenseTotals[i].ToString() + "";
            csv += System.Environment.NewLine;
            html += "</tr>";
        } 
        html += "<tr>";
        html += "<td><b>Total</b></td>";
        csv += "Total,";
        decimal totalExpenses = 0m;
        for (int p = 0; p < monthlyTotals.Count; p++)
        {
            totalExpenses += monthlyTotals[p];
            
            html += "<td><b>" + monthlyTotals[p].ToString("c") + "</b></td>";
            csv += "" + monthlyTotals[p].ToString() + ",";
        }
        html += "<td><b>" + totalExpenses.ToString("c") + "</b></td>";
        csv += "" + totalExpenses.ToString() + "";
        csv += System.Environment.NewLine;
        html += "</tr>";
        html += "</table>";
        expensesCSV = csv;
        return html;
    }

    protected string GenerateBudget()
    {
        List<string> tenantIds = property.GetAllTenantIds();
        Guid tableId = Guid.NewGuid();
        tableIds.Add(tableId);
        string html = "<table id=\"" + tableId.ToString() + "\" cellpadding=\"5\" cellspacing=\"0\">";
        incomeCSV = "";
        Dictionary<QuickPM.Period, decimal> totalByMonth = new Dictionary<QuickPM.Period, decimal>();
        html += "<tr>";
        html += "<th>Tenant</th>";
        incomeCSV += "Tenant,";
        for (QuickPM.Period p = eb.BeginPeriod; p <= eb.EndPeriod; p = p.AddMonth())
        {
            totalByMonth[p] = 0m;
            html += "<th>" + p.Month + "/" + p.Year + "</th>";
            incomeCSV += p.Month + "/" + p.Year + ", ";
        }
        incomeCSV += "Tenant Total";
        html += "<th>Tenant Total</th>";
        html += "</tr>";
        incomeCSV += System.Environment.NewLine;
        Dictionary<string, decimal> tenantTotal = new Dictionary<string, decimal>();
        foreach (string tenantId in tenantIds)
        {
            tenantTotal[tenantId] = 0m;
        }

        foreach (string tenantId in tenantIds)
        {
            html += "<tr>";
            html += "<td>";
            QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
            string name = tenant.Name;
            if (name.Length > 17)
            {
                name = name.Substring(0, 16) + ".";
            }
            html += name; //+ " (" + tenant.TenantId + ")";            
            html += "</td>";
            incomeCSV += "\"" + name.Replace("\"", "\"\"") + "\",";
            for (QuickPM.Period p = eb.BeginPeriod; p <= eb.EndPeriod; p = p.AddMonth())
            {


                Dictionary<string, QuickPM.Bill> bills = QuickPM.Bill.GetBills(tenantId, p.Year, p.Month);

                decimal periodTotal = 0m;
                foreach (QuickPM.Bill bill in bills.Values)
                {
                    tenantTotal[tenantId] += bill.Amount;
                    periodTotal += bill.Amount;
                }
                totalByMonth[p] += periodTotal;
                html += "<td>";
                html += periodTotal.ToString("c");                
                html += "</td>";
                incomeCSV += "" + periodTotal.ToString() + ",";
            }
            html += "<td>";
            html += tenantTotal[tenantId].ToString("c");
            html += "</td>";
            html += "</tr>";
            incomeCSV += "" + tenantTotal[tenantId].ToString() + "" + System.Environment.NewLine;
        }
        html += "<tr>";
        html += "<td><b>Total By Month</b></td>";
        incomeCSV += "\"Total By Month\", ";
        decimal grandTotal = 0m;
        for (QuickPM.Period p = eb.BeginPeriod; p <= eb.EndPeriod; p = p.AddMonth())
        {
            html += "<td><b>";
            html += totalByMonth[p].ToString("c");
            grandTotal += totalByMonth[p];
            html += "</b></td>";

            incomeCSV += "" + totalByMonth[p].ToString() + ",";
        }
        html += "<td><b>" + grandTotal.ToString("c") + "</b></td>";
        html += "</tr>";
        html += "</table>";
        incomeCSV += "" + grandTotal.ToString() + "";
        incomeCSV += System.Environment.NewLine;

        tableId = Guid.NewGuid();
        tableIds.Add(tableId);
        string netHtml = "<table id=\"" + tableId.ToString() + "\" cellpadding=\"5\" cellspacing=\"0\">";
        netHtml += "<tr>";
        netHtml += "<th></th>";
        netCSV = ",";
        for (QuickPM.Period p = eb.BeginPeriod; p <= eb.EndPeriod; p = p.AddMonth())
        {
            netHtml += "<th>" + p.Month + "/" + p.Year + "</th>";
            netCSV += p.Month + "/" + p.Year + ",";
        }
        netHtml += "<th>Net Total</th>";
        netHtml += "</tr>";
        netHtml += "<tr><td>Net Income</td>";
        netCSV += "\"Net Total\",";
        netCSV += System.Environment.NewLine;
        netCSV += "\"Net Income\", ";
        
        decimal netTotal = 0m;
        for (QuickPM.Period p = eb.BeginPeriod; p <= eb.EndPeriod; p = p.AddMonth())
        {
            netHtml += "<td>" + (totalByMonth[p] - eb.GetExpensesForMonth(p)).ToString("c") + "</td>";
            netTotal += (totalByMonth[p] - eb.GetExpensesForMonth(p));
            netCSV += "" + (totalByMonth[p] - eb.GetExpensesForMonth(p)).ToString() + ", ";
        }
        netHtml += "<td>" + netTotal.ToString("c") + "</td>";
        netHtml += "</table>";
        netCSV += "" + netTotal.ToString() + "";
        
        Session["BudgetNet"] = netHtml;
        

        //foreach(QuickPM.Period p = beginPeriod; p <= endPeriod; prop = prop.A
        return html;
    }
}
