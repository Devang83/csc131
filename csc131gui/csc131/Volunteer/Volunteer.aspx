<%@ Page Language="C#" Inherits="QuickPMWebsite.Volunteer" MasterPageFile="~/BaseMaster.master" %>
<%@ MasterType VirtualPath="~/BaseMaster.master" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
<%= message %>
<% message = ""; %>
<asp:Panel runat="server" GroupingText="Contact Information">
<table>
<tr>
<td>First Name</td> <td><asp:TextBox runat="server" Id="FirstName"></asp:TextBox></td>
<td>Last Name</td> <td><asp:TextBox runat="server" Id="LastName"></asp:TextBox></td>
</tr>
<tr>
<td>Email</td> <td><asp:TextBox runat="server" Id="Email"></asp:TextBox></td>
<td>Cell Phone</td> <td><asp:TextBox runat="server" Id="CellPhone"></asp:TextBox></td>
</tr>
<tr>
<td>Office Phone</td> <td><asp:TextBox runat="server" Id="OfficePhone"></asp:TextBox></td>
<td>Home Phone</td> <td><asp:TextBox runat="server" Id="HomePhone"></asp:TextBox></td>
</tr>
<tr>
<td>Address</td> <td><asp:TextBox runat="server" Id="Address"></asp:TextBox></td>
<td>Employer</td> <td><asp:TextBox runat="server" Id="Employer"></asp:TextBox></td>
</tr>
</table>
<asp:Button runat="server" Text="Save" Id="ButtonSave" OnClick="ButtonSave_Click"></asp:Button>
</asp:Panel>


<br/><br/>
<asp:Panel runat="server" GroupingText="Delete">
<asp:Button runat="server" OnClientClick="javascript: return confirm('Delete?')" Text="Delete Volunteer" Id="ButtonDelete" OnClick="ButtonDelete_Click"></asp:Button>
</asp:Panel>
<br/><br/>
<asp:Panel runat="server" GroupingText="Interests">
<table cellspacing="0px" cellpadding="10px">
<tr>
<th>Interest</th>
</tr>
<% foreach(string interest in v.Interests) { 
if (interest != string.Empty) {%>
<tr>
<td>
	<%= interest %>
</td>
<td>
<a href="javascript: callMethod('Delete', '<%= interest %>')" onclick="javascript:confirm('Delete?')">Delete</a>
</td>
</tr>
<%  }
} %>
</table>
<asp:TextBox runat="server" Id="Interest"></asp:TextBox>
<asp:Button runat="server" Text="Add" Id="ButtonAddInterest" OnClick="ButtonAddInterest_Click"></asp:Button>
</asp:Panel>

<br/>
<asp:Panel runat="server" GroupingText="Events">

<table cellspacing="0px" cellpadding="10px">
<tr>
<th>Event Name</th><th>Event Date</th>
<th>Hours</th>
<th>Minutes</th>
<th>Notes</th>
</tr>
<% foreach(VolunteerTracker.Attend a in attends) {
	VolunteerTracker.Volunteer v = new VolunteerTracker.Volunteer(a.VolunteerId);
	VolunteerTracker.Event e = new VolunteerTracker.Event(a.EventId);
%>
	<tr>
		<td><a href="../Event/Event.aspx?Id=<%= e.Id%>"><%= e.Name %></a></td><td><%= e.Date.ToShortDateString() %></td><td><%= a.Hours.ToString()%></td>
		<td><%= a.Minutes.ToString() %></td>
		<td><%= a.Notes %></td>
	</tr>
<% } %>
</table>
</asp:Panel>
</asp:Content>

