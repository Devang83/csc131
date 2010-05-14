<%@ Page Title="" Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" Inherits="Reports_Property_LeaseSummaries" Codebehind="LeaseSummaries.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="ReportsMenu" Src="~/Reports/ReportsMenu.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeaseSummaryReport" Src="~/Reports/Property/LeaseSummaryReport.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Lease Summaries</title>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc:ReportsMenu runat="server" />
    <div id="content">
        <uc:LeaseSummaryReport runat="server" />
    </div>
</asp:Content>

