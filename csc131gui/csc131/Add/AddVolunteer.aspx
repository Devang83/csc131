<%@ Page Language="C#" Inherits="QuickPMWebsite.AddVolunteer" MasterPageFile="~/BaseMaster.master" %>
<%@ MasterType VirtualPath="~/BaseMaster.master" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
<%= message %>
<% message = ""; %>
<table>
<tr>
<td>First Name</td> <td><asp:TextBox runat="server" Id="FirstName"></asp:TextBox></td>
</tr>
<tr>
<td>Last Name</td> <td><asp:TextBox runat="server" Id="LastName"></asp:TextBox></td>
</tr>
<tr>
<td>Email</td> <td><asp:TextBox runat="server" Id="Email"></asp:TextBox></td>
</tr>
<tr>
<td>Cell Phone</td> <td><asp:TextBox runat="server" Id="CellPhone"></asp:TextBox></td>
</tr>
<tr>
<td>Office Phone</td> <td><asp:TextBox runat="server" Id="OfficePhone"></asp:TextBox></td>
</tr>
<tr>
<td>Home Phone</td> <td><asp:TextBox runat="server" Id="HomePhone"></asp:TextBox></td>
</tr>
<tr>
<td>Address</td> <td><asp:TextBox runat="server" Id="Address"></asp:TextBox></td>
</tr>
<tr>
<td>Ethnicity</td> <td><asp:TextBox runat="server" Id="Ethnicity"></asp:TextBox></td>
</tr>
<tr>
<td>Employer</td> <td><asp:TextBox runat="server" Id="Employer"></asp:TextBox></td>
</tr>
<tr>
<td>Job Title</td> <td><asp:TextBox runat="server" Id="JobTitle"></asp:TextBox></td>
</tr>
<tr>
<td>M/F</td> <td><asp:TextBox runat="server" Id="Male"></asp:TextBox></td>
</tr>


</table>
<asp:Button runat="server" Text="Add" Id="ButtonAdd" OnClick="ButtonAdd_Click"></asp:Button>
</asp:Content>

