<%@ Page Language="C#" AutoEventWireup="true" Inherits="Tenant_SecurityDeposit" MasterPageFile="~/Tenant/Tenant.master" Codebehind="SecurityDeposit.aspx.cs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ChildMainContent" runat="server">
<h3>Security Deposit</h3>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
<br />
<% if(securityDeposit != null) { %>
<% if (Session["Error"] != null){ %>
<%= Session["Error"].ToString() %>
<% Session["Error"] = null;
   } %>
   
<table>
<tr>
    <td>Required Security Deposit Per Lease</td>
    <td><asp:TextBox ID="TextBoxSecurityDepositAmount" runat="server"></asp:TextBox></td>
</tr>    
</table>
<asp:Button ID="ButtonSubmit1" runat="server" Text="Save" onclick="ButtonSubmit_Click" />
<br />

<asp:Panel ID="Panel1" GroupingText="Security Deposit Check" runat="server">
    
<table>    
<tr>
    <td>Check#</td>
    <td><asp:TextBox ID="TextBoxSecurityDepositCheckNumber" runat="server"></asp:TextBox></td>
    
</tr>

<tr>
    <td>Amount</td>
    <td><asp:TextBox ID="TextBoxSecurityDepositCheckAmount" runat="server"></asp:TextBox></td>    
<td>
    <asp:Panel ID="Panel3" GroupingText="Outstanding Security Deposit" runat="server">
        <font color="red"><%= (securityDeposit.DepositAmount - securityDeposit.CheckAmount).ToString("c") %></font>
    </asp:Panel>
    </td>
</tr>

<tr>
    <td>Date</td>
    <td><asp:TextBox ID="TextBoxSecurityDepositCheckDate" runat="server"></asp:TextBox>
    		  <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxSecurityDepositCheckDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>

    </td>    
</tr>

<tr>
    <td>Received Date</td>
    <td><asp:TextBox ID="TextBoxSecurityDepositCheckReceivedDate" runat="server"></asp:TextBox>
    		  <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxSecurityDepositCheckReceivedDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>

    </td>    
</tr>        
</table>
</asp:Panel>

<br />
<asp:Panel ID="Panel4" GroupingText="Required Security Deposit Refunds" runat="server">
    <%  if(securityDeposit.RefundSchedule.Count >0){ %>
    <table> 
    <tr>
    <th></th>
    <th>Date</th>    
    <th>Amount</th>
    </tr>   
    <% foreach (QuickPM.SecurityDepositRequiredRefund refund in securityDeposit.RefundSchedule)
       {
           int refundIndex = securityDeposit.RefundSchedule.IndexOf(refund);
           
           %>
        <tr>
            <% if(tenant.ACL.CanWrite(QuickPM.Database.GetUserId())){ %>
            <td>(<a href="EditRequiredRefund.aspx?index=<%= refundIndex %>&tenantid=<%= securityDeposit.TenantId %>">edit</a>, <a href="javascript:__doPostBack('DeleteRequiredRefund', '<%= refundIndex %>')">delete</a>) </td>
            <% } %>
            <td> <%= refund.Date.ToShortDateString() %></td><td>&nbsp;&nbsp;<%= refund.Amount.ToString("c") %></td>
        </tr>
    <% } %>   

</table>
<% } %>
</asp:Panel>

    Required Refund Date 
    <asp:TextBox ID="TextBoxRequiredRefundDate" runat="server"></asp:TextBox>    
    		  <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxRequiredRefundDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>

    
    &nbsp;&nbsp;Amount 
    <asp:TextBox ID="TextBoxRequiredRefundAmount" runat="server"></asp:TextBox>    
    
    <asp:Button  ID="ButtonRequiredRefund" runat="server" Text="Add" OnClick="ButtonAddRequiredRefund_Click" />
    


<br />    
<br />
<asp:Panel ID="Panel2" GroupingText="Actual Security Deposit Refunds" runat="server">
    <% if (securityDeposit.Refunds.Count > 0 ) { %>
    <table cellpadding="3px"> 
    <tr>
    <th></th>
    <th>Check#</th>
    <th>Amount</th>
    <th>Date</th>
    </tr>   
    <% foreach (QuickPM.SecurityDepositRefund refund in securityDeposit.Refunds)
       {
           int refundIndex = securityDeposit.Refunds.IndexOf(refund);
           %>
        <tr>
            <% if(tenant.ACL.CanWrite(QuickPM.Database.GetUserId())) { %>
                <td>(<a href="EditActuallyRefund.aspx?index=<%= refundIndex %>&tenantid=<%= securityDeposit.TenantId %>">edit</a>, <a href="javascript:__doPostBack('DeleteRefund', '<%= refundIndex %>')">delete</a>) </td>
            <% } %>
            <td><%= refund.CheckNumber %></td><td> &nbsp;&nbsp;<%= refund.Amount.ToString("c") %></td><td><%= refund.CheckDate.ToShortDateString() %></td>
        </tr>
    <% } %>   

</table>
 <% } %>
</asp:Panel>
    &nbsp;&nbsp; Refund Check#  <asp:TextBox ID="TextBoxRefundCheckNumber" runat="server"></asp:TextBox>
    &nbsp;&nbsp;Refund Amount 
    <asp:TextBox ID="TextBoxRefundAmount" runat="server"></asp:TextBox>
    
    &nbsp; &nbsp; Refund Date 
    <asp:TextBox ID="TextBoxRefundDate" runat="server"></asp:TextBox>		  <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxRefundDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>
    
    
    <asp:Button ID="ButtonAddRefund" runat="server" Text="Add" OnClick="ButtonAddRefund_Click" />
    

<br />    
<br />
<h3>Notes</h3> &nbsp
<asp:TextBox ID="TextBoxNotes" runat="server" TextMode="MultiLine" Height="140px" Width="400px"></asp:TextBox>
<br />
<br />
    <asp:Button ID="ButtonSubmit" runat="server" Text="Save" onclick="ButtonSubmit_Click" />
  <% } %>
</asp:Content>