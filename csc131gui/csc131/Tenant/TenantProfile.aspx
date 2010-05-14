<%@ Page Language="C#" AutoEventWireup="true" Inherits="Tenant_ATenant" MasterPageFile="~/Tenant/Tenant.master" Codebehind="Tenant.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Tenant" Src="~/Tenant/Tenant.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ChildMainContent" runat="server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
    <uc:Tenant ID="TheTenant" runat="server" />
</asp:Content>
