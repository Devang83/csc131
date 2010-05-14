<%@ Page Title="" Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_ReconciliationSpreadsheet" Codebehind="ReconciliationSpreadsheet.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<script type="text/javascript">
    function stripeTables(){        
            stripe("reconspreadsheet");        
    }
    addLoadEvent(stripeTables);
</script>

<% if (eb != null)
   {
       int totalSqFt = 0;
       %>
   <table cellpadding="5px" id="reconspreadsheet" cellspacing="0px">
   <tr>
        <th>Tenant/Unit#</th>
        <th>Sq.Ft.</th>
    <% foreach (QuickPM.ExpenseItem ei in eb.ExpenseItems)
       { %>
       <th><%= ei.COADescription %></th>
    <% } %>
        <th>Total</th>
        <th>Tenant's Reconciliation</th>
   </tr>
   
   
    <% foreach (QuickPM.PropertyUnit unit in units)
       {
           int sqFt = 0;
           %>
            <%= CreateUnitHtml(unit, out sqFt) %>
    <% totalSqFt += sqFt;
       } %>
    <tr>
    <td>Totals</td>
    <td><%= totalSqFt.ToString("n") %></td>
    <% foreach (QuickPM.ExpenseItem ei in eb.ExpenseItems)
       { %>
       <td><%= eb.GetCOATotal(ei.ChartOfAccount).ToString("c") %></td>
    <% } %>
    </tr>
    </table>
<% } %>
</asp:Content>

