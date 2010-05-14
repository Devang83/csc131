<%@ Page Language="C#" Inherits="QuickPMWebsite.ListVolunteers" MasterPageFile="~/BaseMaster.master" %>
<%@ MasterType VirtualPath="~/BaseMaster.master" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
<table>
<% foreach(VolunteerTracker.Volunteer v in VolunteerTracker.Volunteer.Find<VolunteerTracker.Volunteer>()) { %>
<tr>
<td><a href="../Volunteer/Volunteer.aspx?Id=<%=v.Id%>"><%= v.FirstName %></a> </td><td> <%= v.LastName %> </td><td> <%= v.CellPhone %></td>
</tr>
<% } %>
</table>
</asp:Content>

