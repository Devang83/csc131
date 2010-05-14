<%@ Page Language="C#" Inherits="QuickPMWebsite.Event" MasterPageFile="~/BaseMaster.master" %>
<%@ MasterType VirtualPath="~/BaseMaster.master" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
<%= message %>
<% message = ""; %>
<table>
<tr>
<td>Name</td> <td><asp:TextBox runat="server" Id="Name"></asp:TextBox></td>
</tr>
<tr>
<td>Date</td> <td><asp:TextBox runat="server" Id="Date"></asp:TextBox></td>
</tr>
</table>
<asp:Button runat="server" Text="Save" Id="ButtonSave" OnClick="ButtonSave_Click"></asp:Button>
</asp:Content>

