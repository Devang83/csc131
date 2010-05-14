<%@ Page Language="C#" AutoEventWireup="true" Inherits="SearchForms_SearchMenu" MasterPageFile="~/BaseMaster.master" Codebehind="SearchMenu.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Search Forms</title>
    <script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <ul class="Topbar">
            <li><a href="<%= ResolveUrl("~/SearchForms/KeywordSearch.aspx") %>">Tenants</a></li>            
            <li><a href="<%= ResolveUrl("~/SearchForms/DelinquentTenants.aspx") %>">Delinquent Tenants</a></li>
            <li><a href="<%= ResolveUrl("~/SearchForms/ListPropertyTenants.aspx") %>">List Tenants</a></li>                        
            <li><a href="<%= ResolveUrl("~/SearchForms/ListProperties.aspx") %>">List Properties</a> </li>            
        </ul>
    </div>
</asp:Content>        
