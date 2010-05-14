<%@ Page Language="C#" MasterPageFile="~/Tenant/Tenant.master" AutoEventWireup="true" Inherits="Tenant_Report" Title="Untitled Page" Codebehind="Reports.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<h3>Reports</h3>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<ul class="Topbar">
        <li><a href="<%= ResolveUrl("~/Tenant/Reports/StandardReport.aspx?tenantid=") + Request["TenantId"] %>">Monthly Report</a></li>
        <li><a href="<%= ResolveUrl("~/Tenant/Reports/AgedAR.aspx?tenantid=") + Request["TenantId"] %>">Aged Receivables Report</a></li>
        <li><a href="<%= ResolveUrl("~/Tenant/Reports/AppliedReceipts.aspx?tenantid=") + Request["TenantId"] %>">Applied Receipts Report</a></li>
        <li><a href="<%= ResolveUrl("~/Tenant/Reports/CashReceipts.aspx?tenantid=") + Request["TenantId"] %>">Cash Receipts Report</a></li>
        <li><a href="<%= ResolveUrl("~/Tenant/Reports/ReceivedChecks.aspx?tenantid=") + Request["TenantId"] %>">Checks Received Report</a></li>
    </ul>    
</asp:Content>

