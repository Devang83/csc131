<%@ Page Language="C#" MasterPageFile="~/Tenant/Tenant.master" AutoEventWireup="true" Inherits="Tenant_Reports_CashReceipts" Title="Untitled Page" Codebehind="CashReceipts.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<div id="content">
    <% if (html == null)
       { %>
    <table>
    <tr><th>Cash Receipts Report</th></tr>    
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
    <% if (html != null)           
           {               
           %>
            <a href="<%= ResolveUrl("~/Reports/Print.aspx?" + GenerateParams()) %>">Print Friendly</a>
           <br />
           <%= html %>
           
           <% html = null;
           } %>
           </div>    
</asp:Content>

