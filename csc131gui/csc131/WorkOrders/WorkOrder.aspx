<%@ Page Title="" Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" CodeBehind="WorkOrder.aspx.cs" Inherits="QuickPMWebsite.WorkOrders.WorkOrder" %>
<%@ Register TagPrefix="uc" TagName="WorkOrderControl" Src="~/WorkOrders/WorkOrderControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Work Order</title>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<uc:WorkOrderControl ID="OrderControl" runat="server" /> 
</asp:Content>
