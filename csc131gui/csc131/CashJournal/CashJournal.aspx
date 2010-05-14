<%@ Page Title="" Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" Inherits="CashJournal_CashJournal" Codebehind="CashJournal.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Journal" Src="~/CashJournal/CashJournalControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Post Rents/Cash Journal</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc:Journal Id="CashJournal" runat="server"></uc:Journal>
</asp:Content>

