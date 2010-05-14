using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Property_ReconciliationSpreadsheet : System.Web.UI.Page
{
    public QuickPM.ExpenseList eb = null;
    public QuickPM.Property property = null;
    public List<QuickPM.PropertyUnit> units = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        if (Request["ebid"] != null)
        {
            eb = new QuickPM.ExpenseList(new Guid(Request["ebid"]));
        }
        else
        {
            return;
        }
        property = new QuickPM.Property(eb.PropertyId);
        units = QuickPM.PropertyUnit.FindUnits(eb.PropertyId);        
        List<string> tenantIds = property.GetTenantIds();
        units = QuickPM.PropertyUnit.FindUnits(property.Id);
        foreach (string tenantId in tenantIds)
        {
            QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
            bool needToAdd = true;
            foreach (QuickPM.PropertyUnit unit in units)
            {
                if (unit.GetCurrentTenantId() == tenant.TenantId)
                {
                    needToAdd = false;
                    break;
                }
            }
            if (needToAdd)
            {
                QuickPM.PropertyUnit unit = new QuickPM.PropertyUnit("", tenant.Property, "");                
                unit.Save();                
                //tenant.Prop
                //tenant.PropertyUnitId = unit.Id;
                tenant.Save();                
                units.Add(unit);
            }
        }

        if (Request.Form["__EVENTTARGET"] == "ExpenseSelectionChanged")
        {
            ExpenseSelectionChanged(Request.Form["__EVENTARGUMENT"]);
        }
    }

    protected string CreateUnitHtml(QuickPM.PropertyUnit unit, out int sqFt)
    {
        string html = "<tr>";
        if (unit.GetCurrentTenantId() != "")
        { 
            html += "<td>" + "<a href=\"" + Page.ResolveUrl("~/Tenant/BasicLeaseInfo.aspx?tenantid=" + unit.GetCurrentTenantId()) + "\">" + new QuickPM.Tenant(unit.GetCurrentTenantId()).GetShortName() + "</a>" + "</td>";
        }
        else
        {
            html += "<td>" + "<a href=\"" + Page.ResolveUrl("~/Property/Units.aspx?PropertyId=" + unit.PropertyId) + "\">" + unit.UnitNumber + " (Vacant)</a></td>";
        }
        sqFt = unit.SqFt;
        html += "<td>" + unit.SqFt.ToString() + "</td>";
        QuickPM.TenantReconciliation recon = QuickPM.TenantReconciliation.GetRecon(unit, eb);
        decimal total = 0m;
        foreach (QuickPM.ExpenseItem ei in eb.ExpenseItems)
        {                     
            decimal shareAmount = GetPropertyUnitsShare(unit, ei.ChartOfAccount);
            string share = shareAmount.ToString("c");
            int parcip = QuickPM.TenantReconciliation.DoesntParticipate;
            bool hasKey = false;
            foreach(int chartOfAccount in recon.ExpenseParticipation.Keys){
                if(chartOfAccount == ei.ChartOfAccount && recon.ExpenseParticipation[chartOfAccount] == QuickPM.TenantReconciliation.DoesntParticipate){
                    share = "";
                    hasKey = true;
                }
                if(chartOfAccount == ei.ChartOfAccount){
                    parcip = recon.ExpenseParticipation[chartOfAccount];
                    hasKey = true;
                }
            }
            if (!hasKey)
            {
                share = "";
            }
            html += "<td>";
            html += "<select>";
            string[] optionText = new string[3];
            optionText[QuickPM.TenantReconciliation.DoesntParticipate] = "Doesn't Participate";
            optionText[QuickPM.TenantReconciliation.LandlordPays] = "Landlord Pays";
            optionText[QuickPM.TenantReconciliation.TenantPays] = "Tenant Pays";            
            total += shareAmount;

            for(int p = 0; p <= 2; p++)
            {
                string tmpshare = p == QuickPM.TenantReconciliation.DoesntParticipate ? "" : share;
                string selectedText = p == parcip ? "selected=\"selected\"" : "";
                html += "<option value=\"" + p + "\" onclick=\"javascript:__doPostBack('ExpenseSelectionChanged','" + 
                    unit.Id.ToString() + ";" + p + ";" + ei.ChartOfAccount + "')\"" + 
                    selectedText + ">" + optionText[p] + " " + tmpshare + "</option>";    
            }                        
            
            html += "</select>";
            html += "</td>";
            
                   
                    
        }
        html += "<td>";
        html += total.ToString("c");
        html += "</td>";
        html += "<td>";
        html += "<a href=\"" + Page.ResolveUrl("~/Property/ReconciliationAndBudget/TenantRecon.aspx?tenreconid=" + recon.Id + "&PropertyId=" + property.Id) + "\">View</a>";
        html += "</td>";
        html += "</tr>";
        return html;
    }


    protected void ExpenseSelectionChanged(string arg)
    {
        string[] vals = arg.Split(new char[] { ';' });
        QuickPM.PropertyUnit unit = new QuickPM.PropertyUnit(long.Parse(vals[0]));
        int value = int.Parse(vals[1]);
        int chartOfAccount = int.Parse(vals[2]);
        QuickPM.TenantReconciliation recon = QuickPM.TenantReconciliation.GetRecon(unit, eb);
        recon.ExpenseParticipation[chartOfAccount] = value;
        recon.Save();
    }

    protected decimal GetPropertyUnitsShare(QuickPM.PropertyUnit unit, int chartOfAccount)
    {
        List<QuickPM.TenantReconciliation> tenantRecons = QuickPM.TenantReconciliation.Find<QuickPM.TenantReconciliation>("ExpenseBudgetId", eb.Id);
        int totalParcipSqFt = 0;
        bool unitPays = false;
        foreach (QuickPM.TenantReconciliation recon in tenantRecons)
        {
            if (!recon.ExpenseParticipation.ContainsKey(chartOfAccount) || recon.ExpenseParticipation[chartOfAccount] == QuickPM.TenantReconciliation.DoesntParticipate)
            {
                continue;
            }
            if (recon.ExpenseParticipation[chartOfAccount] == QuickPM.TenantReconciliation.LandlordPays || recon.ExpenseParticipation[chartOfAccount] == QuickPM.TenantReconciliation.TenantPays)
            {
                totalParcipSqFt += recon.GetPropertyUnit().SqFt;
            }
            if (recon.UnitNumber == unit.UnitNumber && recon.TenantId == unit.GetCurrentTenantId())
            {
                if (recon.ExpenseParticipation[chartOfAccount] != QuickPM.TenantReconciliation.DoesntParticipate)
                {
                    unitPays = true;
                }
            }

        }
        if (!unitPays)
        {
            return 0m;
        }
        decimal share = ((decimal)unit.SqFt / (decimal)totalParcipSqFt);
        return totalParcipSqFt > 0 ? share * eb.GetCOATotal(chartOfAccount) : 0m;        
        
    }

    protected decimal GetTenantsShare(string tenantId, int chartOfAccount)
    {
                
        List<QuickPM.TenantReconciliation> tenantRecons = QuickPM.TenantReconciliation.Find<QuickPM.TenantReconciliation>("ExpenseBudgetId", eb.Id);
        int totalParcipSqFt = 0;
        bool tenantPays = false;
        foreach (QuickPM.TenantReconciliation recon in tenantRecons)
        {
            if (!recon.ExpenseParticipation.ContainsKey(chartOfAccount) || recon.ExpenseParticipation[chartOfAccount] == QuickPM.TenantReconciliation.DoesntParticipate)
            {
                continue;
            }
            if (recon.ExpenseParticipation[chartOfAccount] == QuickPM.TenantReconciliation.LandlordPays || recon.ExpenseParticipation[chartOfAccount] == QuickPM.TenantReconciliation.TenantPays)
            {
                totalParcipSqFt += recon.GetPropertyUnit().SqFt;
            }
            if (recon.TenantId == tenantId)
            {
                if (recon.ExpenseParticipation[chartOfAccount] == QuickPM.TenantReconciliation.TenantPays)
                {
                    tenantPays = true;
                }
            }

        }
        if (!tenantPays)
        {
            return 0m;
        }
        QuickPM.PropertyUnit tenantUnit = new QuickPM.PropertyUnit(new QuickPM.Tenant(tenantId).GetPropertyId());
        return tenantUnit.SqFt / totalParcipSqFt * eb.GetCOATotal(chartOfAccount);        
    }
}
