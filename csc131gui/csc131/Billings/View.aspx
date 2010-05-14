<%@ Page Language="C#" AutoEventWireup="true" Inherits="Billings_View" MasterPageFile="~/BaseMaster.master" Codebehind="View.aspx.cs" %>
<%@ Register TagName="Billing" TagPrefix="uc" Src="~/Billings/BillingsControl.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>View Billings</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>    

<asp:Content runat="server" ContentPlaceHolderID="MainContent">    
    <uc:Billing Id="BillingControl" runat="server"></uc:Billing>
</asp:Content>