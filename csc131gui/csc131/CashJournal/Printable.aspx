<%@ Page Language="C#" AutoEventWireup="true" Inherits="CashJournal_Printable" Codebehind="Printable.aspx.cs" %>      
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cash Journal</title>
<script type="text/javascript" src="<%= ResolveUrl("~/Util.js") %>"></script>
<script type="text/javascript">
    function stripeTables() {
        stripe("deposits");
    }
    addLoadEvent(stripeTables);
</script>

</head>
<body>    
<center>
    <h2>Cash Journal For <%= new QuickPM.Property(PropertyId).Name %> </h2>
    <h2>Period: <%= new QuickPM.Period(year, month).ToString() %></h2>
</center>
<center>
<table id="deposits" border="0px" cellpadding="10px" cellspacing="0px">
<tr>
<th>Tenant Name(#)</th><th>Check<br />Date</th><th>Received Date<br /> OR NSF Date</th><th>Check #</th>
<th>Check<br />Amount</th>
<th>Deposit<br />Amount</th>
<th>Notes</th>
</tr>
<% if (deposits != null)
   {    
       foreach (QuickPM.Deposit deposit in deposits)
       {
           decimal total = 0m;
       %>
            <tr>
            <td>
            <b>Deposit, &nbsp; &nbsp;             
            <%= deposit.DepositDate.ToShortDateString() %>/
            </b> 
            </td>            
            <td></td>            
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            
            </tr>
            <% foreach (QuickPM.DepositEntry de in deposit.DepositEntries)
               { %>
               <tr>
                <% 
                string link = "";
                if (de.HasTenantId)
                {
                    link = (new QuickPM.Tenant(de.TenantId)).Name + " (" + de.TenantId + ")";
                }
                else
                {
                    link = new QuickPM.Property(PropertyId).Name;
                }
                    
                %>
                    
               <td><%= link %></td>
               <td><%= de.TransactionDate.ToShortDateString() %></td>
               <td><%= de.ReceivedDate.ToShortDateString() %></td>
               <td><%= de.TransactionId %></td>
               <td><%= de.Amount.ToString("c") %></td>               
                <td></td>
                <td></td>
                <td><%= de.Notes %></td> 
                </tr>
            <% total += de.Amount;
               } %>               
               
               <% if (deposit.Deposited)
                  { %>
                   <tr>
                   <td><b>Deposit Total</b></td>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td><b><%= total.ToString("c")%></b></td>
                   <td></td>                                      
                   <td></td>                   
                   </tr> 
               <% } %>
               <tr style="background-color:LightBlue">
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
               </tr>                 
<%      } %>

    <tr>
        <td><b>CASH JOURNAL TOTAL</b></td>
        <%
            decimal totalReceived = 0m;
            foreach(QuickPM.Deposit d in deposits){
                foreach(QuickPM.DepositEntry de in d.DepositEntries){
                    totalReceived += de.Amount;
                }
                
            }
        %>
        <td></td><td></td><td></td><td></td>
        <td><b><%= totalReceived.ToString("c") %></b></td>
    </tr>
 <% } %>
   
</table>   
    <br />
</center>
</body>
</html>
