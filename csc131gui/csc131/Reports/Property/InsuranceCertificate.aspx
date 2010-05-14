<%@ Page Title="" Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" Inherits="Reports_Property_InsuranceCertificate" Codebehind="InsuranceCertificate.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="ReportsMenu" Src="~/Reports/ReportsMenu.ascx" %>
<%@ Register TagPrefix="uc" TagName="InsuranceCertificateReport" Src="~/Reports/Property/InsuranceCertificateReport.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<title>Insurance Certificate Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<uc:ReportsMenu ID="ReportsMenu" runat="server" />    
<div id="content">
<uc:InsuranceCertificateReport runat="server" />
</div>
</asp:Content>

