

<%@ Page Language="C#" MasterPageFile="~/Tenant/Tenant.master" AutoEventWireup="true" Inherits="Tenant_StandardReport" Title="Untitled Page" Codebehind="StandardReport.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="TenantReport" Src="~/Reports/TenantReportControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<h3>Monthly Report</h3>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 

<uc:TenantReport ID="TenantReport" runat="server" />
</asp:Content>

