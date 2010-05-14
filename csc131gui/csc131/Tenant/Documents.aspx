<%@ Page Language="C#" AutoEventWireup="true" Inherits="Tenant_Documents" MasterPageFile="~/Tenant/Tenant.master" Codebehind="Documents.aspx.cs" %>

<%@ Register TagPrefix="uc" TagName="Documents" Src="~/Documents/DocumentsControl.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ChildMainContent" runat="server">
<h3>Documents</h3>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<asp:Panel runat = "server" ID="PanelDocuments" GroupingText="Documents">

<uc:Documents runat="server" Id="DocumentsTenant"></uc:Documents>   
</asp:Panel>
<br />

<asp:Panel runat = "server" ID="Panel1" GroupingText="Insurance Certificates">

<uc:Documents runat="server" Id="DocumentsIC"></uc:Documents>   
</asp:Panel>

</asp:Content>

