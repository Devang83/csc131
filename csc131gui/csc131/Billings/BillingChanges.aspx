<%@ Page Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" Inherits="Billings_BillingChanges" Title="Untitled Page" Codebehind="BillingChanges.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Billing Changes</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="Panel2" GroupingText="Property" runat="server">    
    <asp:DropDownList ID="DropDownListProperty" AutoPostBack="true" runat="server" 
        onselectedindexchanged="DropDownListProperty_SelectedIndexChanged">
    </asp:DropDownList>
    </asp:Panel>
    <br />
    Changes in Next Days:<asp:TextBox ID="TextBoxDays" runat="server"></asp:TextBox>    
    <br />
    <br />
    <asp:Panel ID="Panel1" GroupingText="Rent Types" runat="server">
        <asp:CheckBoxList ID="ListBoxRentTypes" runat="server"></asp:CheckBoxList>
    </asp:Panel>
    <br />
    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
        onclick="ButtonSubmit_Click" />
        <br/>
        <% if (Session["AdjustHtml"] != null)
           { %>
           <%= Session["AdjustHtml"] %>
        <% Session["AdjustHtml"] = null;
           
           } %>     
</asp:Content>

