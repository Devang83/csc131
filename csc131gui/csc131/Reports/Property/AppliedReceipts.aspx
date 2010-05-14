<%@ Page Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" Inherits="Reports_AppliedReceipts" Title="Applied Receipts Report" Codebehind="AppliedReceipts.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="ReportsMenu" Src="~/Reports/ReportsMenu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
 <title>Applied Receipts Report</title>
    <style type="text/css">
        #formReport
        {
            height: 318px;
        }
    </style>
    <script type="text/javascript" src="../Util.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<uc:ReportsMenu ID="ReportsMenu" runat="server" />    
    <div id="content">
    <% if (Session["AppliedReceiptsReportHtml"] == null)
       { %>
    <table>
    <tr><th>Applied Receipts Report</th></tr>
    <tr>
        <td align="right"><asp:Label ID="LabelSelectProperty" runat="server" Text="Property:"></asp:Label></td>
        <td align="left">
            <asp:DropDownList ID="PropertyList" runat="server" 
                onselectedindexchanged="PropertyList_SelectedIndexChanged" 
                ontextchanged="PropertyList_TextChanged" AutoPostBack="True">
            </asp:DropDownList>
        </td>
    </tr>
    </table>
    
    <asp:Panel ID="Panel1" runat="server" GroupingText="Beginning Period" 
            Height="78px">
            <table>
            <tr>
            <td><asp:Label ID="Label2" runat="server" Text="Year"></asp:Label></td>
            <td><asp:DropDownList ID="DropDownListBeginYear" runat="server">
            </asp:DropDownList></td>
            </tr>
            <tr>
            <td><asp:Label ID="Label3" runat="server" Text="Month"></asp:Label></td>
            <td><asp:DropDownList ID="DropDownListBeginMonth" runat="server">
                <asp:ListItem>January</asp:ListItem>
                <asp:ListItem>February</asp:ListItem>
                <asp:ListItem>March</asp:ListItem>
                <asp:ListItem>April</asp:ListItem>
                <asp:ListItem>May</asp:ListItem>
                <asp:ListItem>June</asp:ListItem>
                <asp:ListItem>July</asp:ListItem>
                <asp:ListItem>August</asp:ListItem>
                <asp:ListItem>September</asp:ListItem>
                <asp:ListItem>October</asp:ListItem>
                <asp:ListItem>November</asp:ListItem>
                <asp:ListItem>December</asp:ListItem>
            </asp:DropDownList></td>
            </tr>
            </table>

        </asp:Panel>
            <br />
        <br />
                
               <asp:Panel ID="Panel2" runat="server" GroupingText="Ending Period" 
                Height="85px" style="margin-bottom: 16px">
                <table>
                <tr>
                <td><asp:Label ID="Label4" runat="server" Text="Year"></asp:Label></td>
                <td><asp:DropDownList ID="DropDownListEndYear" runat="server">
                </asp:DropDownList></td>
                </tr>
                <tr>
                <td><asp:Label ID="Label5" runat="server" Text="Month"></asp:Label></td>
                <td><asp:DropDownList ID="DropDownListEndMonth" runat="server">
                    <asp:ListItem>January</asp:ListItem>
                    <asp:ListItem>February</asp:ListItem>
                    <asp:ListItem>March</asp:ListItem>
                    <asp:ListItem>April</asp:ListItem>
                    <asp:ListItem>May</asp:ListItem>
                    <asp:ListItem>June</asp:ListItem>
                    <asp:ListItem>July</asp:ListItem>
                    <asp:ListItem>August</asp:ListItem>
                    <asp:ListItem>September</asp:ListItem>
                    <asp:ListItem>October</asp:ListItem>
                    <asp:ListItem>November</asp:ListItem>
                    <asp:ListItem>December</asp:ListItem>
                </asp:DropDownList></td>
                </tr>
                </table>
            </asp:Panel>
            <br />
        <asp:Panel ID="Panel3" GroupingText="Rent Types" runat="server">
            <asp:CheckBoxList ID="CheckBoxListRentTypes" runat="server" >
            </asp:CheckBoxList>
    
        </asp:Panel>
    
    <asp:Button ID="ButtonGenerateReport" runat="server" Text="Generate Report" OnClientClick="Progress();" OnClick="ButtonGenerateReport_Click"/>
    <% } %>
    <span id="LoadingPage">
    </span>
    <% if (Session["AppliedReceiptsReportHtml"] != null)           
           {
               Session["AppliedReceiptsReportHtmlPrint"] = Session["AppliedReceiptsReportHtml"];               
           %>
            <a href="<%= Page.ResolveUrl("~/Reports/Print.aspx?" + GenerateParams()) %>">Print Friendly</a>
           <br />
           <%= Session["AppliedReceiptsReportHtml"]%>
           
           <% Session["AppliedReceiptsReportHtml"] = null;
           } %>    
           </div>
</asp:Content>


