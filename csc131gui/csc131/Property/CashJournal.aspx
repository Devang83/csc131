<%@ Page Title="" Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_CashJournal" Codebehind="CashJournal.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Journal" Src="~/CashJournal/CashJournalControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
    <script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<h4>Post Rents/Cash Journal</h4>
    <uc:Journal Id="CashJournal" runat="server"></uc:Journal>
</asp:Content>

