<%@ Control Language="C#" AutoEventWireup="true" Inherits="CashJournal_CashJournalControl" Codebehind="CashJournalControl.ascx.cs" %>
<%@ Register TagPrefix="uc" TagName="Calendar" Src="~/CalendarControl/CalendarControl.ascx" %>

<script type="text/javascript" src="<%= ResolveUrl("~/Util.js") %>"></script>
<script type="text/javascript">
    function stripeTables(){        
        stripe("deposits");        
    }
    addLoadEvent(stripeTables);
</script>
    <asp:Panel ID="Panel1" GroupingText="Property, Year, & Month" runat="server">    
    <% if (CanSelectProperty)
       { %>
    Property &nbsp;
    <asp:DropDownList ID="DropDownListProperty" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Submit_Click">
    
    </asp:DropDownList>
    <br />
    <br />
    <% } %>    
    
    <uc:Calendar ID="ucCalendar" runat="server" />
    
</asp:Panel>
<a href="<%= ResolveUrl("~/CashJournal/Printable.aspx?year=" + period.Year + "&month=" + period.Month + "&PropertyId=" + PropertyId) %>">Printable</a>
&nbsp;
&nbsp;
<a href="<%= ResolveUrl("~/CashJournal/ExportMenu/-1")%>">Export To Excel</a>
<br />
<% if(GetProperty().ACL.CanWrite(QuickPM.Database.GetUserId())) {  %>
<table id="Table1" border="0px" cellspacing="0px">
<tr>
<th>Tenant Name(#)</th><th>Check<br />Date</th><th>Received Date<br /> OR NSF Date</th><th>Check #</th><th>Check<br />Amount</th><th>Notes</th>
</tr>

<tr>
    <td><asp:DropDownList ID="DropDownListTenant" runat="server"></asp:DropDownList></td>    
    <td><asp:TextBox ID="TextBoxCheckDate" Width="100px" runat="server"></asp:TextBox>
    <script type="text/ecmascript">		      
		      //$(document).ready(function() {
		      //  $("#<%= TextBoxCheckDate.UniqueID.Replace("$", "_") %>").datepicker();
		      //});

		  </script>
    </td>
    <td><asp:TextBox ID="TextBoxReceivedDate" Width="100px" runat="server"></asp:TextBox>
    <script type="text/ecmascript">		      
		      //$(document).ready(function() {
		      //  $("#<%= TextBoxReceivedDate.UniqueID.Replace("$", "_") %>").datepicker();
		      //});

		  </script>
    </td>
    <td><asp:TextBox ID="TextBoxCheckNumber" Width="100px" runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="TextBoxAmount" Width="100px" runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="TextBoxNotes" runat="server"></asp:TextBox></td>
    <td>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButtonAdd_Click">Add Entry</asp:LinkButton><br />
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButtonAddDeposit_Click">Add Entry & Deposit</asp:LinkButton>
    </td>
   </tr>
</table>
<% } %>
<table id="deposits" border="0px" cellpadding="10px" cellspacing="0px">
<tr>
<th>Tenant Name(#)</th><th>Check<br />Date</th><th>Received Date<br /> OR NSF Date</th><th>Check #</th><th>Check<br />Amount</th><th>Deposit<br />Amount</th>
<th>&nbsp;&nbsp;Imported To <br />&nbsp;&nbsp;QuickBooks</th><th>Notes</th>
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
            <%= deposit.DepositDate.Month.ToString() %>/
            <% if (deposit.ACL.CanWrite(QuickPM.Database.GetUserId()))
               { %>
            <select onchange="__doPostBack('DepositDateChanged','<%= deposit.Id.ToString() + "|" %>' + this.options[this.selectedIndex].value)" >
            <% for (int day = 1; day <= DateTime.DaysInMonth(deposit.DepositDate.Year, deposit.DepositDate.Month); day++)
               { %>
                    <option value="<%= day %>" <%= day == deposit.DepositDate.Day ? "selected = \"selected\"" : "" %>><%= day%></option>
            <% } %>
            </select>
            <% }else { %>
            <%= deposit.DepositDate.Day.ToString() %>
            <% } %>
            /
            <%= deposit.DepositDate.Year.ToString() %>            
            </b> 
            <% if (deposit.ACL.CanWrite(QuickPM.Database.GetUserId()))
               { %>
            (<a href="javascript:__doPostBack('DeleteDeposit','<%= deposit.Id.ToString() %>')" onclick="javascript: return confirm('Delete Deposit?')">delete deposit</a>) 
            <%} %>
            </td>
            <td></td>            
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td>&nbsp;&nbsp;
            <% if (deposit.ImportedToQuickBooks)
               { %> <b><i><u> <% } %>
            <% if (deposit.ACL.CanWrite(QuickPM.Database.GetUserId()))
               { %>
            <a href="javascript:__doPostBack('ChangeQuickBooksImported', 'true|<%= deposit.Id.ToString() %>')">Yes</a>
            <% } else { %>
                Yes
            <% } %>
            <% if (deposit.ImportedToQuickBooks)
               { %> </u></i></b> <% } %>
            &nbsp; &nbsp; 
            <% if (!deposit.ImportedToQuickBooks)
               { %> <b><i><u> <% } %>
                <% if(deposit.ACL.CanWrite(QuickPM.Database.GetUserId())) { %>
               <a href="javascript:__doPostBack('ChangeQuickBooksImported', 'false|<%= deposit.Id.ToString() %>')">No</a> 
               <% } else { %>
                    No
               <% } %>
               <% if (!deposit.ImportedToQuickBooks)
                  { %> </u></i></b> <% } %>
            </td>
            <td></td>
            
            </tr>
            <% foreach (QuickPM.DepositEntry de in deposit.DepositEntries)
               { %>
               <tr>
                <% 
                string link = "";
                if (de.HasTenantId)
                {
                    link = "<a href=\"" + ResolveUrl("~/Tenant/AR.aspx?year=" + period.Year + "&month=" + period.Month + "&tenantid=" + de.TenantId) + "\">" + (new QuickPM.Tenant(de.TenantId)).Name + " (" + de.TenantId + ")" + "</a>";
                } else {
                    link = new QuickPM.Property(PropertyId).Name;
                }                    
                %>
                    
               <% if(deposit.ACL.CanWrite(QuickPM.Database.GetUserId())) { %>
               <td>(<a href="<%= ResolveUrl("~/CashJournal/EditDepositEntry.aspx") %>?depositId=<%= deposit.Id.ToString() %>&entry_index=<%=deposit.DepositEntries.IndexOf(de).ToString() %>">edit</a>, <a href="javascript:__doPostBack('DeleteDepositEntry','<%= deposit.Id.ToString() + ";" + deposit.DepositEntries.IndexOf(de).ToString() %>')" onclick="javascript: return confirm('Delete?')">delete</a>) &nbsp; &nbsp; <%= link %></td>
               <% }else { %>
                <td><%= link %></td>
               <% } %>
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
               <tr>
               <% if(deposit.ACL.CanWrite(QuickPM.Database.GetUserId())) { %>
                <td>                    
                    <a href="javascript:__doPostBack('AddDepositEntry','<%= deposit.Id.ToString()%>')">Add Check</a>
                </td>
                <% } %>
               </tr>
            
               
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
               <% }
                  else if(deposit.ACL.CanWrite(QuickPM.Database.GetUserId()))
                  { %>
                    <tr>
                    <td>
                        <a href="javascript:__doPostBack('FinishDeposit','<%= deposit.Id.ToString()%>')">Finish Deposit</a>
                    </td>
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