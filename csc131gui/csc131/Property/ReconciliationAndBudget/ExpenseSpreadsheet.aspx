<%@ Page Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_ExpenseSpreadsheet" Title="Untitled Page" Codebehind="ExpenseSpreadsheet.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<script type="text/javascript">
    function stripeTable(){
        stripe("expensespreadsheet");
    }
    addLoadEvent(stripeTable);    
</script>
<br />
<b>Created</b> &nbsp; <%= eb.CreatedDate.ToString()%>
<br />
<b>Modified</b> &nbsp; <%= eb.ModifiedDate.ToString()%>
<br />

<br />
<br />
    Increase Expenses <asp:TextBox ID="TextBoxIncrease" Width="20px" runat="server">4.0</asp:TextBox> % 
    <asp:LinkButton ID="LinkButtonIncrease" Font-Size="Large" OnClick="LinkButtonIncrease_Click" runat="server">+</asp:LinkButton>, 
    <asp:LinkButton ID="LinkButtonDecrease" Font-Size="Large" OnClick="LinkButtonDecrease_Click" runat="server">-</asp:LinkButton>
<br />
<br />    
<table cellpadding="3px" id = "expensespreadsheet" cellborder="0px" cellspacing="0px">
<tr>
<th>Expense</th>
<% 
    for (QuickPM.Period p = eb.BeginPeriod; p <= eb.EndPeriod; p = p.AddMonth())
   { %>
   <th><%= p.Month %>/<%= p.Year %></th>
   <% } %>
   <th>TOTAL</th>
   </tr>
<%  int numberOfMonths = 12;
    if (eb.ExpenseItems.Count > 0)
    {
        numberOfMonths = eb.ExpenseItems[0].expenses.Count;
    }
    System.Collections.Generic.List<decimal> monthlyTotals = new System.Collections.Generic.List<decimal>(numberOfMonths);
    System.Collections.Generic.List<decimal> expenseTotals = new System.Collections.Generic.List<decimal>(eb.ExpenseItems.Count);
    for (int i = 0; i < numberOfMonths; i++)
    {
        monthlyTotals.Add(0m);
    }
    for (int i = 0; i < eb.ExpenseItems.Count; i++)
    {
        expenseTotals.Add(0m);
    }    
    string color = "ddd";
        
    for (int i = 0; i < eb.ExpenseItems.Count; i++) {
    
       QuickPM.ExpenseItem ei = eb.ExpenseItems[i];       
       if (ei.IsSubCOA)
       {           
           continue;
       }
       
        %>
       

<% if (eb.COAHasSubItems(ei.ChartOfAccount))
   { %>
        <tr> <td><%= ei.GetShortCOADescription() %> (<a href="<%= EditExpenseItemUrl(ei) %>">edit</a>, 
        <a href="javascript:__doPostBack('DeleteExpense','<%= ei.COADescription %>')" onclick="javascript: return confirm('Delete?')">delete</a>)</td>
        <% for (int q = 0; q <= eb.ExpenseItems[0].periods.Count; q++)
        { %>
            <td></td>
        <% } %>
        </tr>
<% } else { %>
        <%= GetExpenseItemHtml(ei, "", color, expenseTotals, monthlyTotals) %>
<% 
   
   } %>
    
    <% 
        
        foreach (QuickPM.ExpenseItem sei in eb.GetSubExpenseItems(ei))
       { %>
        <%= GetExpenseItemHtml(sei, "", color, expenseTotals, monthlyTotals)%>
    <% } %>
    <% if(eb.COAHasSubItems(ei.ChartOfAccount)) { %>
            <%= GetExpenseItemHtml(ei, " - Other", color, expenseTotals, monthlyTotals)%>
    <% } %>
    
<% if (eb.COAHasSubItems(ei.ChartOfAccount))
   { %>
<tr>
<td>Total <%=ei.GetShortCOADescription() %></td>

<% 
    for (int k = 0; k < ei.periods.Count; k++)
    { %>
        <td align="right"><%= eb.GetCOATotal(ei.ChartOfAccount, k).ToString("c")%></td>
<% } %>
<td align="right"><%= eb.GetCOATotal(ei.ChartOfAccount).ToString("c")%></td>
</tr>
<% } %>
<% } %>
<tr>
<td>Total</td>
<% //color = color == "fff" ? "ddd" : "fff";
    decimal totalExpenses = 0m;
    for (int p = 0; p < monthlyTotals.Count; p++)
    {
        totalExpenses += monthlyTotals[p];
        %>
   <td align="right"><%= monthlyTotals[p].ToString("c") %></td>
<% } %>
<td align="right"><%= totalExpenses.ToString("c") %></td>

</tr>
</table>
<br />
    <asp:LinkButton ID="LinkButtonAddExpenseItem" runat="server" OnClick="LinkButtonAddExpenseItem_OnClick">Add Expense</asp:LinkButton> 
    <br />
    <br />
    
    <asp:LinkButton ID="LinkButtonDeleteAll" runat="server" OnClick="LinkButtonDeleteAll_OnClick" OnClientClick="javascript: return confirm('Delete All Expenses?')">Delete All Expenses</asp:LinkButton> 
    <br />
    <br />
    
    
    <asp:FileUpload ID="FileUploadExpenses" runat="server" /> 
     <asp:Button ID="ButtonUploadExpenses" runat="server" Text="Upload Expenses CSV File" OnClick="ButtonUploadExpenses_Click" />
    <br />
    <br />
    <asp:LinkButton ID="LinkButtonDone" runat="server" OnClick="LinkButtonDone_OnClick" Font-Size="Large">Done</asp:LinkButton>
</asp:Content>

