<%@ Page Title="" Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_ReconciliationAndBudget_TenantRecon" Codebehind="TenantRecon.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<script type="text/javascript">
    function stripeTables() {
        stripe("reconspreadsheet");
    }
    addLoadEvent(stripeTables);
</script>

<% if(tenRecon != null) { %>
        <%= GenerateExpenseTableHtml() %>
    <br />
    <br />
    
<% } %>
</asp:Content>

