<%@ Page Title="" Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_Budget" Codebehind="Budget.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<script type="text/javascript">
    function stripeTables(){
        <% foreach(Guid tableId in tableIds) { %>
            stripe("<%= tableId.ToString() %>");
        <% } %>
    }
    addLoadEvent(stripeTables);
    
    
</script>
<h3>Income</h3>
<%= Session["BudgetIncome"] %>
<hr />
<h3>Expenses</h3>
<%= Session["BudgetExpenses"] %>
<hr />
<h3>Net Income</h3>
<%= Session["BudgetNet"] %>

<br />
<br />
<div style="margin-left:auto;margin-right:auto;text-align:center"
<asp:LinkButton ID="SaveAsCSV" runat="server" OnClick="SaveAsCSV_OnClick" Font-Size="Large">Save</asp:LinkButton>
</div>
</asp:Content>

