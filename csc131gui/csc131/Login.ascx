<%@ Control Language="C#" AutoEventWireup="true" Inherits="Login" Codebehind="Login.ascx.cs" %>
<% if(Session["LoginStatus"] != null) { %>
    <center><font color = "red"><%= Session["LoginStatus"].ToString() %></font></center>
    <br />
<% Session["LoginStatus"] = null;
   } %>
<table style="height: 88px; width: 402px">
    <tr>    
    <td>Email</td>
        <td><asp:TextBox ID="TextBoxEmail" runat="server" 
                Width="188px"></asp:TextBox></td>
    </tr>
    <tr>
    <td>
    Password
    </td>
    <td>
        <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" 
            Width="188px"></asp:TextBox></td>
    </tr>
    <tr>
    <td></td>
    <td>
        <asp:CheckBox ID="CheckBoxRemember" runat="server" /> Stay signed in</td>
    </tr>
    <tr>
    <td></td>
    <td><asp:Button ID="ButtonLogin" runat="server" Text="Log In" 
            onclick="ButtonLogin_Click" /></td>
    </tr>
    </table> 
<a href="<%= Page.ResolveUrl("~/Home/ForgotPassword/-1")%>">Forgot Password</a>
<br />   
    