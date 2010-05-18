<%@ Page Language="C#" Inherits="QuickPMWebsite.ListEvents" MasterPageFile="~/BaseMaster.master" %>
<%@ MasterType VirtualPath="~/BaseMaster.master" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
<table cellspacing="0px" cellpadding="10px">
<tr>
<th>Name</th>
<th>Time</th>
</tr>
<% foreach(VolunteerTracker.Event e in VolunteerTracker.Event.Find<VolunteerTracker.Event>()) { %>
<tr>
<td align="right"><a href="../Event/Event.aspx?Id=<%=e.Id%>"><%= e.Name %></a> </td>

<td align="right"> <%= e.Date %> </td>
</tr>
<% } %>
</table>
<br/>
<a href="../Add/AddEvent.aspx">Add Event</a>

</asp:Content>

