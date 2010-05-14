<%@ Page Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_Report" Title="Untitled Page" Codebehind="Report.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="PropertyReport" Src="~/Reports/PropertyReportControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<center><h2><a href="<%= ResolveClientUrl("~/Property/Reports/ReportsMenu.aspx?propertyid=" + Request["PropertyId"])%>">Reports</a></h2></center>

<uc:PropertyReport ID="Report" runat="server" />
</asp:Content>

