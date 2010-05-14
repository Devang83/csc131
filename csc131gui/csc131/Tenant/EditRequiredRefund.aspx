<%@ Page Title="" Language="C#" MasterPageFile="~/Tenant/Tenant.master" AutoEventWireup="true" Inherits="Tenant_EditRequiredRefund" Codebehind="EditRequiredRefund.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<br />
<table>
<tr>
    <td>Date </td><td>
    <asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox></td>
</tr>
<tr>
    <td>Amount</td>
    <td>
        <asp:TextBox ID="TextBoxAmount" runat="server"></asp:TextBox></td>
</tr>
</table>
<br />
    <asp:LinkButton ID="LinkButtonFinished" OnClick="LinkButtonSubmit_Click" runat="server">Finished</asp:LinkButton>
</asp:Content>

