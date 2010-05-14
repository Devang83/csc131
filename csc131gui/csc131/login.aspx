<%@ Page Language="C#" AutoEventWireup="true" Inherits="login" MasterPageFile="~/BaseMaster.master" Codebehind="login.aspx.cs" %>
 <%@ Register TagPrefix="uc" TagName="Login" Src="~/Login.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
   <title>Volunteer Tracker</title>
   <script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
     <uc:Login id="Login" runat="server" />
</asp:Content>
