<%@ Page Title="" Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" Inherits="CashJournal_EditDepositEntry" Codebehind="EditDepositEntry.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Edit Deposit Entry</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <asp:Panel ID="Panel1" GroupingText="Deposit Entry" runat="server">
    
<table>
<tr>
<th>Tenant Name(#)</th><th>Check<br />Date</th><th>Received Date<br /> OR NSF Date</th><th>Check #</th><th>Check<br />Amount</th><th>Notes</th>
</tr>
    <tr>
        <td><asp:DropDownList ID="DropDownListTenant" runat="server"></asp:DropDownList></td>    
        <td><asp:TextBox ID="TextBoxCheckDate" Width="100px" runat="server"></asp:TextBox></td>
        <td><asp:TextBox ID="TextBoxReceivedDate" Width="100px" runat="server"></asp:TextBox></td>
        <td><asp:TextBox ID="TextBoxCheckNumber" Width="100px" runat="server"></asp:TextBox></td>
        <td><asp:TextBox ID="TextBoxAmount" Width="100px" runat="server"></asp:TextBox></td>
        <td><asp:TextBox ID="TextBoxNotes" runat="server"></asp:TextBox></td>
        <td><asp:LinkButton ID="LinkButtonSubmit" runat="server" OnClick="LinkButtonSubmit_Click">Save</asp:LinkButton></td>
        <td>
            <asp:LinkButton ID="LinkButtonCancel" OnClick="LinkButtonCancel_Click" runat="server">Cancel</asp:LinkButton>
        </td>
        
   </tr>
</table>
</asp:Panel>
</asp:Content>

