<%@ Page Title="" Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_Reports_ReportsMenu" Codebehind="ReportsMenu.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<center><h2>Reports</h2>
<% if(Request["PropertyId"] != null ) { %>
<br />
<a href="Report.aspx?PropertyId=<%= Request["PropertyId"] %>">Monthly Report</a>
<br />
<br />
<a href="../../Property/ARSummary/<%= Request["PropertyId"] %>">Tenant Ledger Summary</a>
<br />
<br />
<a href="../../Reports/Property/AppliedReceipts.aspx?PropertyId=<%= Request["PropertyId"] %>">Applied Receipts</a>
<br />
<br />
<a href="../../Reports/Property/CashReceipts.aspx?PropertyId=<%= Request["PropertyId"] %>">Cash Receipts</a>
<br />
<br />
<a href="LeaseSummaries.aspx?PropertyId=<%= Request["PropertyId"] %>">Lease Summaries</a>
<br />
<br />
<a href="CashFlowIncome.aspx?PropertyId=<%= Request["PropertyId"] %>">Income Chart</a>
<br />
<br />
<a href="InsuranceCertificates.aspx?PropertyId=<%= Request["PropertyId"] %>">Insurance Certificates</a>
<br />
<br />
<a href="RentRoll.aspx?PropertyId=<%= Request["PropertyId"] %>">Rent Roll</a>

<% } %>
</center>
<br/>
</asp:Content>


