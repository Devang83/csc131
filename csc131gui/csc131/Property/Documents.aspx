<%@ Page Language="C#" AutoEventWireup="true" Inherits="Property_Documents" MasterPageFile="~/Property/Property.master" Codebehind="Documents.aspx.cs" %>

<%@ Register TagPrefix="uc" TagName="Documents" Src="~/Documents/DocumentsControl.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ChildMainContent" runat="server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<h4>Documents</h4>
<% if(property != null){ %>
<uc:Documents runat="server" Id="Documents"></uc:Documents>   
<% } %>
</asp:Content>