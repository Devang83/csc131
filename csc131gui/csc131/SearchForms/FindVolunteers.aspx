<%@ Page Language="C#" Inherits="QuickPMWebsite.FindVolunteers" MasterPageFile="~/BaseMaster.master" %>
<%@ MasterType VirtualPath="~/BaseMaster.master" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
<title>Find Volunteers</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
<h2>Search by Name (First or Last), Employer, or Interest</h2>
<table>
<tr>
<td>Name</td><td> <asp:TextBox runat="server" Id="Name"></asp:TextBox></td>
</tr>
<tr>
<td>Employer</td><td> <asp:TextBox runat="server" Id="Employer"></asp:TextBox></td>
</tr>
<tr>
<td>Interest</td><td> <asp:TextBox runat="server" Id="Interest"></asp:TextBox></td>
</tr>
</table>
<br/>
<asp:Button runat="server" Id="Search" Text="Search" OnClick="Search_Click"></asp:Button>
<%= resultsMessage %>
<% if (results.Count != 0) { %>
<table>
<tr>
<th>Name</th>
<th>Employer</th>
<th>Cell Phone</th>
<th>Interests</th>
</tr>
<% foreach(VolunteerTracker.Volunteer v in results) { 
string vInterests = "";
foreach(string interest in v.Interests)
{
	vInterests += interest + ", ";
}
%>
	<tr>
		<td><a href="../Volunteer/Volunteer.aspx?Id=<%= v.Id %>"><%= v.FirstName + " " + v.LastName %></a></td>		
		<td><%= v.Employer %></td>
		<td><%= v.CellPhone %></td>
		<td><%= vInterests %></td>
	</tr>
<% } %>
</table>
<% } %>
</asp:Content>


