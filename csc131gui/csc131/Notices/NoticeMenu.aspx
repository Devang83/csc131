<%@ Page Language="C#" AutoEventWireup="true" Inherits="Notices_NoticeMenu" MasterPageFile="~/BaseMaster.master" Codebehind="NoticeMenu.aspx.cs" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>Notices Menu</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
    <a href="GenerateNotice.aspx?notice=Rent">Pay Rent Notice</a><br />
    <a href="GenerateNotice.aspx?notice=AdditionalRent">Pay Additional Rent Notice</a>
    </div>
</asp:Content>
