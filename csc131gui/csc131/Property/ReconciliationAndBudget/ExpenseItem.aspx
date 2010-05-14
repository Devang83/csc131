<%@ Page Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_ExpenseItem" Title="Untitled Page" Codebehind="ExpenseItem.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<br />
<table cellpadding="5px">
<tr>
<td><b>Expense Name</b> &nbsp; <asp:TextBox ID="TextBoxExpenseName" runat="server"></asp:TextBox></td>
<td><b>Account#</b> &nbsp; <asp:TextBox ID="TextBoxCOA" runat="server"></asp:TextBox></td>
</tr>
</table>
<table cellpadding="5px">
<tr>
<td>
<asp:CheckBox ID="CheckBoxSubaccount" runat="server" /> <b>Subaccount of</b> &nbsp; 
    <asp:DropDownList ID="DropDownListAccounts" runat="server">
    </asp:DropDownList>
    </td>
    </tr>
</table>
<br />
<br />
   Increase Expense <asp:TextBox ID="TextBoxIncrease" Width="20px" runat="server">4.0</asp:TextBox> % 
    <asp:LinkButton ID="LinkButtonIncrease" Font-Size="Large" OnClick="LinkButtonIncrease_Click" runat="server">+</asp:LinkButton>, 
    <asp:LinkButton ID="LinkButtonDecrease" Font-Size="Large" OnClick="LinkButtonDecrease_Click" runat="server">-</asp:LinkButton>
    <br />
    <br />

<table>
<% int i = -1;
    for (QuickPM.Period p = beginPeriod; p <= endPeriod; p = p.AddMonth())
   {
       i++;
        %>
   <tr>
        <td><%= p.Month %>/<%= p.Year %> </td> <td> <input type="text" name="Amount<%= p.Month.ToString() + p.Year.ToString()%>" id="Amount<%= p.Month.ToString() + p.Year.ToString()%>" value="<%= ei.expenses[i].ToString("c") %>" /></td>
    </tr>
<%  
    } %>
</table>
    <asp:LinkButton ID="LinkButtonSubmit" runat="server" 
        onclick="LinkButtonSubmit_Click">Submit</asp:LinkButton>
</asp:Content>

