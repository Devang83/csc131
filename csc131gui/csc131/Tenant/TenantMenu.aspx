<%@ Page Language="C#" AutoEventWireup="true" Inherits="Tenant_TenantMenu" MasterPageFile="~/BaseMaster.master" Codebehind="TenantMenu.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Tenant Menu</title>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="Topbar">
            <li><a href="<%= ResolveUrl("~/SearchForms/KeywordSearch.aspx") %>">Find Tenants</a></li>            
            <li><a href="<%= ResolveUrl("~/SearchForms/DelinquentTenants.aspx") %>">Delinquent Tenants</a></li>
            <li><a href="<%= ResolveUrl("~/SearchForms/ListPropertyTenants.aspx") %>">List Tenants</a></li>                                    
            <li><a href="<%= ResolveUrl("~/Add/AddTenant.aspx") %>">Add Tenant</a></li>
    </ul>
</asp:Content>