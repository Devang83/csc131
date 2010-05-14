<%@ Page Title="" Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_Reports_LeaseSummaries" Codebehind="LeaseSummaries.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="LeaseSummaryReport" Src="~/Reports/Property/LeaseSummaryReport.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<center><h2><a href="<%= ResolveClientUrl("~/Property/Reports/ReportsMenu.aspx?propertyid=" + Request["PropertyId"])%>">Reports</a></h2></center>

<h3>Lease Summaries</h3>
<uc:LeaseSummaryReport runat="server" />
</asp:Content>

