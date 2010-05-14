<%@ Page Language="C#" AutoEventWireup="true" Inherits="Add_AddMenu" MasterPageFile="~/BaseMaster.master" Codebehind="AddMenu.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="MainMenu" Src="~/Topbar.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Add</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <ul class="Topbar">
            <li><a href="AddProperty.aspx">Add Property</a></li>            
            <li><a href="AddTenant.aspx">Add Tenant</a></li>                        
    </ul>
</asp:Content>    
