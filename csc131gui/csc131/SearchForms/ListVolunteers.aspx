<%@ Page Language="C#" Inherits="QuickPMWebsite.ListVolunteers" MasterPageFile="~/BaseMaster.master" %>
<%@ MasterType VirtualPath="~/BaseMaster.master" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
<table cellpadding="10px" cellspacing="0px">
<tr>
<th>Name</th>
<th>Employer</th>
<th>Cell Phone</th>
<th>Interests</th>
</tr>
<% foreach(VolunteerTracker.Volunteer v in VolunteerTracker.Volunteer.Find<VolunteerTracker.Volunteer>()) { %>
<tr>
<td><a href="../Volunteer/Volunteer.aspx?Id=<%=v.Id%>"><%= v.FirstName + " " + v.LastName %></a> </td><td> <%= v.CellPhone %></td>
<td><%= v.Employer %></td> 
<td>
<% foreach(string interest in v.Interests) { 
	if (interest != string.Empty) {
	%>
	<%= interest %>,
<% }
	} %>
</td>
</tr>
<% } %>
</table>
</asp:Content>

