<%@ Page Language="C#" AutoEventWireup="true" Inherits="ManageUsers_EditPassword" MasterPageFile="~/BaseMaster.master" Codebehind="EditPassword.aspx.cs" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="../Util.js"></script>
    <title>Edit Password</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>    

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <% if (msg != null && msg == "Your new password has been saved")
       {            
        %>
            <center><h2>Your new password has been saved</h2></center>
            <a href="EditUser.aspx?username=<%= this.Profile.UserName %>">My Account</a>
        
    <% msg = null;
    }
       else
       { %>
       <% if (msg != null)
          { %>
          <font color="red"><%= msg %></font>
       <% msg = null;
          } %>
    <div>
    <table cellpadding="10px">
    <tr>
    <td>Current password:</td><td><asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox></td> 
    </tr>
    <tr>
    <td>New password:</td> <td><asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
    
    </tr>
    <tr>
    <td>Confirm new password:</td><td><asp:TextBox ID="RepeatNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
    </tr>
    <tr>
    <td><asp:LinkButton ID="LinkButtonSave" runat="server" onclick="LinkButtonSave_Click">Save</asp:LinkButton></td>
    <td><a href="EditUser.aspx?username=<%= this.Profile.UserName %>">Cancel</a></td>
    </tr>
    </table>
    </div>
    <% } %>
</asp:Content>