<%@ Page Title="" Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_Reports_InsuranceCertificates" Codebehind="InsuranceCertificates.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="InsuranceCertificateReport" Src="~/Reports/Property/InsuranceCertificateReport.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<center><h2><a href="<%= ResolveClientUrl("~/Property/Reports/ReportsMenu.aspx?propertyid=" + Request["PropertyId"])%>">Reports</a></h2></center>
<h3>Insurance Certificates</h3>
<uc:InsuranceCertificateReport runat="server" />
</asp:Content>

