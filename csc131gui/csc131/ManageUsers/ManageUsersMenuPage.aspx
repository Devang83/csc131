<%@ Page Language="C#" AutoEventWireup="true" Inherits="ManageUsers_ManageUsersMenuPage" MasterPageFile="~/BaseMaster.master" Codebehind="ManageUsersMenuPage.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>Manage Users</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
    <table border="1px">
    <tr>
    <th>Email</th>
    <th>Name</th>
    </tr>
    
    <%  
        foreach(MembershipUser membership in Membership.GetAllUsers()) {
            VolunteerTracker.User user = new VolunteerTracker.User(membership.UserName);
    %>
        <tr>
        
        <td><a href="EditUser.aspx?username=<%= membership.UserName %>"><%= membership.Email %></a></td>
        <td><%= user.Name %></td>
        <% if (Roles.IsUserInRole(Context.Profile.UserName, "Manager"))
           { %>
        <td><a href="DeleteUser.aspx?username=<%= membership.UserName %>">Delete User</a> </td>
        <% } %>
        </tr>
    <% } %>
    </table>
    </div>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ManageUsers/AddUser.aspx">Add User</asp:HyperLink>
</asp:Content>