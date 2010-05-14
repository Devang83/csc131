<%@ Page Language="C#" AutoEventWireup="true" Inherits="ManageUsers_AddUser2" MasterPageFile="~/BaseMaster.master" Codebehind="EditUser.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="User" Src="~/ManageUsers/User.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>Edit User</title>    
    <script type="text/javascript">
        function onbodyload() {

        }
        function onbodyunload() {

        }
    
</script> 
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">   
   <uc:User ID="user" runat="server" />
</asp:Content>
