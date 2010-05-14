<%@ Page Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_PrintBillings" Title="Untitled Page" Codebehind="PrintBillings.aspx.cs" %>
<%@ Register TagName="Billings" TagPrefix="uc" Src="~/Billings/BillingsControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<center><h3>Property Billings</h3></center>
<uc:Billings ID="BillingsControl" runat="server" />
</asp:Content>

