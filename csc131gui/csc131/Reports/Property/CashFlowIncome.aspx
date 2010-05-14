<%@ Page Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" Inherits="Reports_Property_CashFlowIncome2" Title="Cash Flow" Codebehind="AppliedReceipts.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="CashFlowIncome" Src="~/Reports/Property/CashFlowIncome.ascx" %>

<%@ Register TagPrefix="uc" TagName="ReportsMenu" Src="~/Reports/ReportsMenu.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<uc:ReportsMenu ID="ReportsMenu1" runat="server" />

<uc:CashFlowIncome ID="CashFlowChart" runat="server" />

</asp:Content>



