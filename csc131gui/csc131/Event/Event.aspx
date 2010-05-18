<%@ Page Language="C#" Inherits="QuickPMWebsite.Event" MasterPageFile="~/BaseMaster.master" %>
<%@ MasterType VirtualPath="~/BaseMaster.master" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
<%= message %>
<% message = ""; %>
<asp:Panel runat="server" GroupingText="Event">
<table>
<tr>
<td>Name</td> <td><asp:TextBox runat="server" Id="Name"></asp:TextBox></td>
<td>Notes</td> <td><asp:TextBox runat="server" Id="Notes"></asp:TextBox></td>
</tr>
<tr>
<td>Date</td> <td><asp:TextBox runat="server" Id="Date"></asp:TextBox></td>
<td>
Time <asp:DropDownList runat="server" Id="Time">		
	</asp:DropDownList>
</td>

</tr>
</table>

<asp:Button runat="server" Text="Save" Id="ButtonSave" OnClick="ButtonSave_Click"></asp:Button>
</asp:Panel>
<br/>
<br/>
<asp:Panel runat="server" GroupingText="Volunteers">

<table>
<tr>
<th>First Name</th>
<th>Last Name</th>
<th>Hours</th>
<th>Minutes</th>
<th>Notes</th>
</tr>
	
			<%foreach(VolunteerTracker.Attend a in attends) { 
				VolunteerTracker.Volunteer v = new VolunteerTracker.Volunteer(a.VolunteerId);
				
			%>
		<tr>
			<td><a href="../Volunteer/Volunteer.aspx?Id=<%=v.Id %>"><%= v.FirstName %></a></td><td><%= v.LastName %></td><td><%= a.Hours %></td><td><%= a.Minutes %></td>
			<td><%= a.Notes %></td>
			<td><a href="javascript: callMethod('Delete', '<%= v.Id %>')" onclick="javascript:confirm('Delete?')">Delete</a></td>
		</tr>
	<%  } %>
	<tr>
		<td>&nbsp;</td>
	</tr>
	<tr>	
		<td><asp:DropDownList runat="server" Text="Volunteers" Id="Volunteers"></asp:DropDownList></td>
		<td>Hours <asp:TextBox runat="server" Id="Hours"></asp:TextBox></td>
		<td>Minutes <asp:TextBox runat="server" Id="Minutes"></asp:TextBox></td>
		<td>Notes <asp:TextBox runat="server" Id="VolunteerNotes"></asp:TextBox></td>
		<td><asp:Button runat="server" Text="Add" Id="ButtonAdd" OnClick="ButtonAdd_Click"></asp:Button></td>	
	</tr>
</table>
</asp:Panel>

</asp:Content>

