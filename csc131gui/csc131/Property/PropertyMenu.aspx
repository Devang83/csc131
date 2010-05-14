<%@ Page Language="C#" AutoEventWireup="true" Inherits="Property_PropertyMenu" MasterPageFile="~/BaseMaster.master" Codebehind="PropertyMenu.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Property Menu</title>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
        <ul class="Topbar">
            <li><a href="<%= ResolveUrl("~/SearchForms/ListProperties.aspx") %>">List Properties</a> </li>            
            <li><a href="<%= ResolveUrl("~/Add/AddProperty.aspx") %>">Add Property</a></li>            
        </ul>
</asp:Content>