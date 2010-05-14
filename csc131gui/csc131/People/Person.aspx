<%@ Page Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" Inherits="People_Person" Title="Untitled Page" Codebehind="Person.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Person" Src="~/PersonControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Edit Contact</title>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<uc:Person ID="KeyContact" runat="server" /> 
    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" />
</asp:Content>

