<%@ Page Language="C#" AutoEventWireup="true" Inherits="Tenant_ARPrint" Codebehind="ARPrint.aspx.cs" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= Request["TenantId"] %></title>
</head>
<body>
    <%         
        QuickPM.Tenant tenant = GetTenant();
        bool hasValues = true;        
        if(tenant == null)
        {
            hasValues = false;
        }       
        QuickPM.Period period = GetPeriod();
    %>
    
<% string backlinkurl = Server.UrlEncode(ResolveUrl("~/Tenant/AR.aspx")+ "?tenantid=" + tenant.TenantId + "&year=" + period.Year + "&month=" + period.Month); %>
<script type="text/javascript" src="<%= ResolveUrl("~/Util.js") %>"></script>       
        
    <div class="content">
    <% if (hasValues)
       { %>
        <% if (Request["TenantId"] != null)
       {
           long PropertyId = QuickPM.Util.GetPropertyId(Request["TenantId"]);
           string property = Request["TenantId"].Split(new char[] { '-' })[0];
           string tenantNumber = Request["TenantId"].Split(new char[] { '-' })[1];
           %>
    <h3>Tenant# <a href="<%= ResolveUrl("~/Property/PropertyPage.aspx?PropertyId=" + PropertyId.ToString())%>"><%= property %></a>-<%= tenantNumber %> <br /> <%= new QuickPM.Tenant(Request["TenantId"]).Name%></h3>
    <% } %>
    <h3>Period &nbsp;<%= QuickPM.Util.ConvertMonthToString(period.Month) %>, <%= period.Year %></h3>	

    <form name="checks" method="post" action="../Checks/HandleCheck.aspx?backlinkurl=<%= backlinkurl %>">    
	<% 
		System.Collections.Generic.List<QuickPM.Check> checks = QuickPM.Database.GetChecks(tenant.TenantId, period);
		if (checks.Count > 0) { %>    
	<table id="CheckTable" cellspacing="10" border="1">
        <tr>
        <th>Check#</th>
        <th>Check Amount</th>
        <th>Check Date</th>
        <th>Received</th>
        </tr>
        <% 
           for (int i = 0; i < checks.Count; i++)
           { %>
            <tr>
                <td><%= checks[i].Number %></td>
                
                <td><%= checks[i].Amount.ToString("c") %></td>
                
                <td><%= checks[i].CheckDate.ToShortDateString() %></td>
                
                <td><%= checks[i].ReceivedDate.ToShortDateString() %></td>                                                
            </tr>       
        <% } %>
	    </table>
	<% } %>    
    <% 
	System.Collections.Generic.List<QuickPM.NSFCheck> nsfChecks = QuickPM.Database.GetNSFChecks(tenant.TenantId, period);
	if(nsfChecks.Count > 0) { %>
    <table id="NSFCheckTable" cellspacing="10" border="1">
        <tr>
        <th>NSF Check#</th>
        <th>Amount</th>
        <th>Check Date</th>
        <th>NSF Date</th>
        </tr>
        <% 
           for (int i = 0; i < nsfChecks.Count; i++)
           { %>
            <tr>
                <td><%= nsfChecks[i].Number %></td>
                
                <td><%= nsfChecks[i].Amount.ToString("c") %></td>
                
                <td><%= nsfChecks[i].CheckDate.ToShortDateString() %></td>
                
                <td><%= nsfChecks[i].NSFDate.ToShortDateString() %></td>                                
            </tr>       
        <% } %>
    </table>
    <% } %>
    <input type="hidden" name = "checkid" value="" />
    <input type="hidden" name = "func" value="ApplyCheck" />
    <input type="hidden" name = "transactiontype" value="check" />
    <input type="hidden" name="tenantid" value="<%= tenant.TenantId %>" />
    <input type="hidden" name="year" value="<%= period.Year %>" />
    <input type="hidden" name="month" value="<%= period.Month %>" />    
    </form>
    <form id="AR" name="AR" method="post" action="AR.aspx?tenantid=<%= Request.Params["tenantid"] %>&year=<%= period.Year %>&month=<%= period.Month %>">
    
    
    <div>
    <%
        //QuickPM.Tenant tenant = GetTenant();
        //bool hasValues = false;
        //hasValues = (tenant != null);
        //QuickPM.Period period = GetPeriod();
    %>
    <% 
           QuickPM.ARRecord record = new QuickPM.ARRecord(tenant.TenantId, period.Year, period.Month);           
    %>
    <div id="tablediv" class="tablediv">
    <table id="LedgerTable" border= "0" cellpadding="11" cellspacing = "0" style="border-width:thin; border-style: solid; border-color:Blue;" >
        <tr>
        <th>Income Type</th>
        <th>Balance Forward</th>
        <th>Current Billing</th>
        <th>Adjustment</th>
        <th>Applied <br />on Current</th>
        <th>Applied <br />on Prior</th>
        <th>NSF</th>
        <th>Outstanding <br />Balance</th>
        </tr>
    <% 
       Dictionary<string, decimal> balanceForward = record.BalanceForward();
       Dictionary<string, QuickPM.Bill> billingRecords = QuickPM.Bill.GetBills(tenant.TenantId, period.Year, period.Month);
       Dictionary<string, decimal> current = new System.Collections.Generic.Dictionary<string, decimal>();
       Dictionary<string, decimal> prior = new System.Collections.Generic.Dictionary<string, decimal>();
       Dictionary<string, decimal> nsf = new Dictionary<string, decimal>();
       Dictionary<string, decimal> totals = new System.Collections.Generic.Dictionary<string, decimal>();
       string[] categories = new string[]{"BalanceForward", "Billing", "Adjustment", "ROC", "ROP", "NSF", "Outstanding"};
       foreach (string category in categories)
       {
           totals[category] = 0m;
       }
    %>
    <% foreach (string rentType in tenant.RentTypes)
       {           
           int index = tenant.RentTypes.IndexOf(rentType);
           int rentTypeIndex = index;
           %>
       <tr>
       <td><a href="<%= ResolveUrl("~/Tenant/Billing.aspx?rentnum=" + index + "&tenantid=" + tenant.TenantId)%>"><%= rentType %></a></td>
       <td><%= balanceForward[rentType].ToString("c") %></td>
       <% string idCurr = "CurrentBilling" + rentType; 
          string idAdj = "Adjustment" + rentType;
              %>
       <%
           prior[rentType] = 0m;
           current[rentType] = 0m;
           nsf[rentType] = 0m;
           for (int i = 0; i < checks.Count; i++)
           {
               QuickPM.Check check = checks[i];
               foreach (QuickPM.MoneyApplied moneyApplied in check.AppliedTo)
               {
                   if (moneyApplied.RentTypeIndex != rentTypeIndex)
                       continue;
                   if (moneyApplied.Date < new DateTime(period.Year, period.Month, 1))
                       prior[rentType] += moneyApplied.Amount;
                   else if (moneyApplied.Date == new DateTime(period.Year, period.Month, 1))
                       current[rentType] += moneyApplied.Amount;
               }
           }
           
           foreach (QuickPM.NSFCheck nsfCheck in nsfChecks)
           {
               foreach (QuickPM.MoneyApplied moneyApplied in nsfCheck.AppliedTo)
               {
                   if (moneyApplied.RentTypeIndex == rentTypeIndex)
                   {
                       nsf[rentType] += moneyApplied.Amount;
                   }
               }
           }
           
           totals["Billing"] += billingRecords[rentType].Amount;
           totals["BalanceForward"] += balanceForward[rentType];
           totals["Adjustment"] += record.Adjustments[rentTypeIndex];
           totals["ROC"] += current[rentType];
           totals["ROP"] += prior[rentType];
           totals["NSF"] += nsf[rentType];     
       %>
       <td><%=  billingRecords[rentType].Amount.ToString("c") %>
       </td>
       <td><%= record.Adjustments[rentTypeIndex].ToString("c") %></td>          
       <td><%= current[rentType].ToString("c") %></td>
       <td><%= prior[rentType].ToString("c") %></td>
       <td><%= nsf[rentType].ToString("c") %></td>
       <%
           decimal outstandingBalance = balanceForward[rentType] + billingRecords[rentType].Amount -
                   current[rentType] - prior[rentType] + nsf[rentType] + record.Adjustments[rentTypeIndex];
           totals["Outstanding"] += outstandingBalance; 
       %>
       <td><%= outstandingBalance.ToString("c") %></td>
       </tr>
    <% } %>
    <tr>
    <td>Totals</td>
    <% foreach(string category in categories) { 
           %>
           
           <td><%= totals[category].ToString("c") %></td>
    <% } %>
    </tr>
    </table>
    </div>
    <h4>Memo:</h4>
        <textarea rows = "10" cols="100" name="textareamemo" disabled = "disabled" ><%= record.Memo %></textarea>
    <% }; %>
    </div>
    </form>
    </div>
</body>
</html>
