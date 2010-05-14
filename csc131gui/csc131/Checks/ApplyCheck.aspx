<%@ Page Language="C#" AutoEventWireup="true" Inherits="ApplyCheck"  MasterPageFile="~/BaseMaster.master" Codebehind="ApplyCheck.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="MainMenu" Src="~/Topbar.ascx" %>
<%@ Register TagPrefix="uc" TagName="CheckMenuHeader" Src="~/Checks/CheckHeaderMenu.ascx" %>
<%@ Register TagPrefix="uc" TagName="Calendar" Src="~/CalendarControl/CalendarControl.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="../style.css"/>
    <title>Apply Check</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <uc:CheckMenuHeader ID="CheckMenu" runat="server" />             
        <uc:Calendar ID="ucCalendar" runat="server" />    
     <%           
        
        QuickPM.MonetaryTransaction mt = GetMonetaryTransaction();
        if (mt != null)
        {
            QuickPM.Tenant tenant = new QuickPM.Tenant(mt.TenantId);
            int year = ((CalendarControl_CalendarControl)ucCalendar).GetCurrentPeriod().Year;
            int month = ((CalendarControl_CalendarControl)ucCalendar).GetCurrentPeriod().Month;
            QuickPM.ARRecord arRecord = null;
            arRecord = new QuickPM.ARRecord(tenant.TenantId, year, month);            
     %>
     <% if (arRecord == null)
        { %>
        <br />
        Sorry, no records exist for <%= QuickPM.Util.ConvertMonthToString(month)%> <%= year.ToString()%>
     <% } %>
    <% if (arRecord != null)
       { %>
    <table width="50%">
    <tr>
        <td>Tenant# <%= tenant.TenantId%></td>    
        <td>Name: <%= tenant.Name%></td>
    </tr>    
    <tr>
    </tr>
    <tr>
        <td>Check# <%= mt.Number%></td>
    </tr>
    <tr>
        <td>Check Amount: <%= mt.Amount.ToString("c")%></td>
    </tr>
    <tr>
        <td>Remaining: <%= mt.RemainingMoney().ToString("c")%></td>
    </tr>
    </table>

    <table border="1">
        <tr>
            <th>Income Type</th>
            <th>Current Billing</th><th>Adjustment</th>
            <th>Current Amount<br />Applied</th>
            <th>Additional Amount<br />Applied</th>
            <th>Outstanding Balance</th>
        </tr>
        <% 
         string[] categories = { "CurrentBilling", "Adjustment", "CurrentApplied", "OtherApplied", "OutstandingBalance" };
         System.Collections.Generic.Dictionary<string, decimal> totals = new System.Collections.Generic.Dictionary<string, decimal>();
         foreach (string category in categories)
         {
             totals.Add(category, 0m);
         }
         foreach (string rentType in tenant.RentTypes)
         {
             int rentTypeIndex = tenant.RentTypes.IndexOf(rentType);
             QuickPM.Bill bill = QuickPM.Bill.GetBill(tenant.TenantId, rentTypeIndex, year, month);             

             decimal currentApplied = 0m;
             foreach (QuickPM.MoneyApplied moneyApplied in mt.AppliedTo)
             {
                 if (moneyApplied.RentTypeIndex == rentTypeIndex && moneyApplied.Date.Year == arRecord.Year &&
                     moneyApplied.Date.Month == arRecord.Month)
                 {
                     currentApplied = moneyApplied.Amount;
                     break;
                 }
             }
             decimal totalApplied = arRecord.Received(rentTypeIndex);
             decimal additionalApplied = 0m;
             if (mt is QuickPM.Check)
             {
                 additionalApplied = totalApplied - currentApplied;
             }
             else if (mt is QuickPM.NSFCheck)
             {
                 additionalApplied = totalApplied + currentApplied;
             }
             decimal outstandingBalance = bill.Amount + arRecord.Adjustments[rentTypeIndex] - totalApplied;
             totals["CurrentBilling"] += bill.Amount;
             totals["Adjustment"] += arRecord.Adjustments[rentTypeIndex];
             totals["CurrentApplied"] += currentApplied;
             totals["OtherApplied"] += additionalApplied;
             totals["OutstandingBalance"] += outstandingBalance;
               %>           
           <tr>
            <td><%= rentType%></td>
            <td><%= bill.Amount.ToString("c")%></td>
            <td><%= arRecord.Adjustments[rentTypeIndex].ToString("c")%></td>
            <% if(mt.ACL.CanWrite(QuickPM.Database.GetUserId())) { %>
                <td><input type="text" name="<%= rentType + "Amount"  %>" value="<%= currentApplied.ToString("c") %>" />
                </td>
            <% } else { %>
                <td>
                    <%= currentApplied.ToString("c") %>
                </td>
            <% } %>
            <td><%= additionalApplied.ToString("c")%></td>
            <td><%= outstandingBalance.ToString("c")%></td>
           </tr>

        <% } %>  
        <tr>
        <td>Total</td>
        <% 
         foreach (string category in categories)
         { %>
        
            <td><%= totals[category].ToString("c")%></td>
        
        <% } %>
        </tr>        
    </table>
    <% if (mt.ACL.CanWrite(QuickPM.Database.GetUserId()))
       { %>    
        <input type="submit" value="Apply" />
    <% } %>
    
    <% } %>    
    <asp:Button ID="ButtonAutoApply" runat="server" onclick="ButtonAutoApply_Click" 
        Text="Auto Apply" />
        <% } %>
</asp:Content>