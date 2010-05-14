
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Tenant_SelectTenant" Codebehind="SelectTenant.ascx.cs" %>
<asp:Label ID="Label1" runat="server" Text="Tenant#"></asp:Label>
<asp:DropDownList ID="DropDownListTenant" OnTextChanged="DropDownListTenant_TextChanged" AutoPostBack="true" runat="server" >
</asp:DropDownList>