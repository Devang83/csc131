<%@ Page Language="C#" MasterPageFile="~/Tenant/Tenant.master" AutoEventWireup="true" Inherits="Tenant_AR" Title="Ledger" Codebehind="AR.aspx.cs" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register TagPrefix="uc" TagName="Calendar" Src="~/CalendarControl/CalendarControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<b>Ledger</b>
    <%         
        QuickPM.Tenant tenant = GetTenant();
        bool hasValues = true;        
        if(tenant == null)
        {
            hasValues = false;
        }
        else
        {
            hasValues = true;
        }
        QuickPM.Period period = GetPeriod();

        QuickPM.Period previousPeriod = period.SubtractMonth();
        QuickPM.Period nextPeriod = period.AddMonth();
        string backlinkurl = "";
        if (tenant != null)
        {
            backlinkurl = Server.UrlEncode(ResolveUrl("~/Tenant/AR.aspx") + "?tenantid=" + tenant.TenantId + "&year=" + period.Year + "&month=" + period.Month);
        }
        bool disabled = false;
        if (!tenant.ACL.CanWrite(QuickPM.Database.GetUserId()))
        {
            disabled = true;
        }
        QuickPM.ARRecord record = new QuickPM.ARRecord(tenant.TenantId, period.Year, period.Month);           
        
    %>
<script type="text/javascript" src="<%= ResolveUrl("~/Util.js") %>"></script>
<script type="text/javascript">
    function stripeTables(){
        
        stripe("CheckTable");
        stripe("NSFCheckTable");
        stripe("LedgerTable", '#fff', '#ccd');
    }
    addLoadEvent(stripeTables);
</script>
       
    <% if (tenant != null)
       { %>       

    <%= hasValues ? "<br />" : "" %>
    
    <table width = "25%">
    <tr>    
    <td>    
        <uc:Calendar ID="ucCalendar" runat="server" />
        </td>    
    <td><a href="ARPrint.aspx?tenantid=<%= tenant.TenantId %>&year=<%= period.Year %>&month=<%= period.Month %>">Printable</a>,</td>
    <td><a href="SendBill.aspx?tenantid=<%= tenant.TenantId %><%= Request["Year"] != null && Request["Month"] != null ? "&year=" + Request["Year"] + "&month=" + Request["Month"] : "" %>">Email</a></td>
    </tr>
    </table>   
    <% } %>
    <% if (hasValues)
       { %>            
    <table>
    <tr>
    <td>
    <table id="CheckTable" cellspacing="10" border="1">    
        <tr>
        <th>Check#</th>
        <th>Check Amount</th>
        <th>Check Date</th>
        <th>Received</th>
        </tr>
        <% System.Collections.Generic.List<QuickPM.Check> checks = QuickPM.Database.GetChecks(tenant.TenantId, period);
           for (int i = 0; i < checks.Count; i++)
           { %>
            <tr>
                <td><%= checks[i].Number %></td>
                
                <td><%= checks[i].Amount.ToString("c") %></td>
                
                <td><%= checks[i].CheckDate.ToShortDateString() %></td>
                
                <td><%= checks[i].ReceivedDate.ToShortDateString() %></td>                                
                <% if (!disabled)
                   { %>
                <td>
                    <a href="<%= ResolveUrl("~/Checks/ApplyCheck.aspx?checkid=" + checks[i].Id.ToString() + "&type=check") %>">Apply</a>                    
                </td>
                <% } %>
                <td>
                    <a href="<%= ResolveUrl("~/Checks/ViewCheck.aspx?checkid=" + checks[i].Id.ToString() + "&type=check") %>">View</a>
                </td>
            </tr>       
        <% } %>
    </table>
    </td>
    <td>
    <table id="NSFCheckTable" cellspacing="10" border="1">
        <tr>
        <th>NSF Check#</th>
        <th>Amount</th>
        <th>Check Date</th>
        <th>NSF Date</th>
        </tr>
        <% System.Collections.Generic.List<QuickPM.NSFCheck> nsfChecks = QuickPM.Database.GetNSFChecks(tenant.TenantId, period);
           for (int i = 0; i < nsfChecks.Count; i++)
           { %>
            <tr>
                <td><%= nsfChecks[i].Number %></td>
                
                <td><%= nsfChecks[i].Amount.ToString("c") %></td>
                
                <td><%= nsfChecks[i].CheckDate.ToShortDateString() %></td>
                
                <td><%= nsfChecks[i].NSFDate.ToShortDateString() %></td>                                
                <% if (!disabled)
                   { %>
                <td>
                    <a href="<%= ResolveUrl("~/Checks/ApplyCheck.aspx?checkid=" + nsfChecks[i].Id.ToString() + "&type=nsf") %>">Apply</a>                    
                </td>
                <% } %>
                <td>
                    <a href="<%= ResolveUrl("~/Checks/ViewCheck.aspx?checkid=" + nsfChecks[i].Id.ToString() + "&type=nsf") %>">View</a>                    
                </td>
            </tr>       
        <% } %>
    </table>
    </td>
    </tr>
    </table>       
    <% if (!disabled)           
       { %>
       <br />
            <a href="../Checks/AddCheck.aspx?tenantid=<%= tenant.TenantId %>&year=<%= period.Year %>&month=<%= period.Month %>&type=check">Add Check</a>
            &nbsp; &nbsp; &nbsp;
            <a href="../Checks/AddCheck.aspx?tenantid=<%= tenant.TenantId %>&year=<%= period.Year %>&month=<%= period.Month %>&type=nsf">Add NSF Check</a>
            <br />
            <br />
    <% } %>
        
<div>
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
       <td><a href="<%= ResolveUrl("~/Tenants/Billing/" + tenant.Id + "?rentnum=" + index)%>"><%= rentType %></a></td>
       <td><%= balanceForward[rentType].ToString("c") %></td>
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
       <% if (!disabled)
          { %>
       <td><%=  billingRecords[rentType].Amount.ToString("c") %>  
       &nbsp; <a href="<%= ResolveUrl("~/Tenants/Billing/" + tenant.Id + "?rentnum=" + index)%>">edit</a>
       </td>
       <td><input type="text" name="<%= "Adjustment" + tenant.RentTypes[rentTypeIndex] %>" value="<%= record.Adjustments[rentTypeIndex].ToString("c") %>"  /></td>
       <% }
          else
          { %>
       <td><%=  billingRecords[rentType].Amount.ToString("c") %>
       </td>
       <td><%= record.Adjustments[rentTypeIndex].ToString("c") %></td>
          
       <% } %>
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
    <% foreach (string category in categories) { 
           %>
           
           <td><%= totals[category].ToString("c") %></td>
    <% } %>
    </tr>
    </table>
    </div>
    <h4>Memo:</h4>
    <% if(!disabled){ %>
    <textarea rows = "10" cols="100" name="textareamemo" ><%= record.Memo %></textarea>
    <% } else { %>
        <textarea rows = "10" cols="100" name="textareamemo" disabled = "disabled" ><%= record.Memo %></textarea>
    <% } %>
    <% }; %>
    </div>    
    <input type="submit" value="Update" <%= disabled %>/>
    <!-- ledger form -->
</asp:Content> 