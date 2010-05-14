<%@ Page Language="C#" AutoEventWireup="true" Inherits="CreateAccount_Setup" MasterPageFile="~/BaseMaster.master" Codebehind="Setup.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
    <% 
        string title = "";
        if (Session["CreateAccountSuccessful"] == null)
       {
           title = "Setup Account";
           
           %>
    
    <% }
       else
       { 
            title = "Account Created";
       %>       
    <% } %>
    <title><%= title %></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <% if (Session["CreateAccountError"] != null)
       { %>
       <%= Session["CreateAccountError"] %>
    <% Session["CreateAccountError"] = null;
       } %>
    <% if(Session["CreateAccountSuccessful"] != null){ %>
        <h3>Account created</h3>
        <h2>Please print or save the following information</h2>
        Account Name:<%= Session["CreateAccountAccountName"].ToString() %><br />
        Email (your logon): <%= Session["CreateAccountEmail"].ToString() %><br />
        Password: <%= Session["CreateAccountPassword"].ToString() %><br />
        <a href="../login.aspx">Login</a>
    <% Session["CreateAccountSuccessful"] = null;
       Session["CreateAccountAccountName"] = null;
       Session["CreateAccountEmail"] = null;
       Session["CreateAccountPassword"] = null;       
       }
        else 
       { %>
    <h2>Create an account</h2>
    It's free. No signup or termination fees. You don't have to enter any billing information.
    If you want you can upgrade to a paid account.<br /><br />
    <div>
        <asp:Panel ID="Panel1" runat="server" GroupingText="Choose your account name"
        BorderColor="Aqua" BackColor="AliceBlue"
        >
        <br />
        <table>
        <tr>
        <td>
            <asp:Label ID="Label7" runat="server" Text="Account Name"></asp:Label> &nbsp;<asp:TextBox ID="TextBoxAccountName" runat="server"></asp:TextBox></td>
        </tr>
        </table>
        <br />
        (Enter one word, no spaces. You can use letters and numbers only)
        </asp:Panel>
        
        <br />
        <asp:Panel ID="Panel3" runat="server" GroupingText="Enter your personal information"
        BorderColor="Aqua" BackColor="AliceBlue"
        >
        <table cellpadding="5px">
        <tr>
        
            <td><asp:Label ID="Label3" runat="server" Text="Email" Width="150px" BackColor="LightSkyBlue" 
                    BorderColor="LightSkyBlue" BorderStyle="Solid" BorderWidth="5px" ForeColor="White"></asp:Label></td>
            <td><asp:TextBox ID="TextBoxEmail" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
        <tr>        
            <td><asp:Label ID="Label4" runat="server" Text="Password" Width="150px" BackColor="LightSkyBlue" 
                    BorderColor="LightSkyBlue" BorderStyle="Solid" BorderWidth="5px" ForeColor="White"></asp:Label></td>
            <td><asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" Width="150px" ></asp:TextBox></td>
        </tr>    
        <tr>
            <td><asp:Label ID="Label5" runat="server" Text="Confirm Password" Width="150px" BackColor="LightSkyBlue" 
                    BorderColor="LightSkyBlue" BorderStyle="Solid" BorderWidth="5px" ForeColor="White"></asp:Label></td>
            <td><asp:TextBox ID="TextBoxRepeatPassword" runat="server" TextMode="Password" Width="150px" ></asp:TextBox></td>
        </tr>
        </table>
        </asp:Panel>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton 
            ID="LinkButtonCreateAccount" runat="server" BackColor="LightBlue" 
            BorderColor="LightBlue" BorderStyle="Solid" BorderWidth="5px" 
            onclick="LinkButtonCreateAccount_Click">Create Account</asp:LinkButton>
    </div>
    <p>
        &nbsp;</p>
    <% } %>
</asp:Content>