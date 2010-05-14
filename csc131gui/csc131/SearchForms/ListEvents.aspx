<%@ Page Language="C#" Inherits="QuickPMWebsite.ListEvents" MasterPageFile="~/BaseMaster.master" %>
<%@ MasterType VirtualPath="~/BaseMaster.master" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
<table>
<% foreach(VolunteerTracker.Event e in VolunteerTracker.Event.Find<VolunteerTracker.Event>()) { %>
<tr>
<td><a href="../Event/Event.aspx?Id=<%=e.Id%>"><%= e.Name %></a> </td><td> <%= e.Date %> </td>
</tr>
<% } %>
</table>
</asp:Content>

