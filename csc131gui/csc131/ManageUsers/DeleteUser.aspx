<%@ Page Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" Inherits="ManageUsers_DeleteUser" Title="Untitled Page" Codebehind="DeleteUser.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Delete <%= theUser.Email %></title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <%= error %>
    
    Delete <%= theUser.Email %>?
    
    <asp:LinkButton ID="LinkButtonYes" runat="server" OnClick="LinkButtonYes_Click" Font-Size="Larger">Yes</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="LinkButtonNo" runat="server" OnClick="LinkButtonNo_Click" Font-Size="Larger">No</asp:LinkButton>
    
</asp:Content>

