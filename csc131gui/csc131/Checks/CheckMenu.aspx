<%@ Page Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" Inherits="Checks_CheckMenu" Title="Untitled Page" Codebehind="CheckMenu.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Check Menu</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <ul class="Topbar">
            <li><a href="<%= ResolveUrl("~/SearchForms/FindChecks.aspx") %>">Find Checks</a> </li>                        
        </ul>

</asp:Content>

