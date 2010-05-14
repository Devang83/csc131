<%@ Page Language="C#" AutoEventWireup="true" Inherits="ManageUsers_AddUser" MasterPageFile="~/BaseMaster.master" Codebehind="AddUser.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="User" Src="~/ManageUsers/User.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>Add User</title>
    <script type="text/javascript">
        function onbodyload() {

        }
        function onbodyunload() {

        }
    
</script> 
</asp:Content>
    
<asp:Content runat="server" ContentPlaceHolderID="MainContent">        
    <% if (Session["ManageUsers-UserAdded"] != null)
       { %>
       <h2>User added, an email has been sent to <%= Session["ManageUsers-UserAdded"]%>, with their login
       information.</h2>
       <a href="EditUser.aspx?username=<%= Session["ManageUsers-UserAdded"] %>">Setup User</a>
    <% Session["ManageUsers-UserAdded"] = null;
       }
       else
       { %>
              
    <% if(Session["Error"] != null)
        { %>   
        <%=  Session["Error"].ToString() %>
    <%  Session["Error"] = null;
        } %>
    <br />
    <table>
    <tr>
    <td><asp:Label ID="Label1" runat="server" Text="Email "></asp:Label></td>
    <td>
    <asp:TextBox ID="TextBoxEmailAddress" runat="server" Width="241px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td><asp:Label ID="Label2" runat="server" Text="Name "></asp:Label></td>
    <td><asp:TextBox ID="TextBoxName" runat="server" Width="241px"></asp:TextBox></td>
    </tr>
    </table>

    <asp:Button ID="ButtonAddUser" runat="server" onclick="ButtonAddUser_Click" 
    Text="Add" />    
           
       <% } %>
</asp:Content>
