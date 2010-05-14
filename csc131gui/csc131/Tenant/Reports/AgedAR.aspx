<%@ Page Language="C#" MasterPageFile="~/Tenant/Tenant.master" AutoEventWireup="true" Inherits="Tenant_AgedARReport" Title="Untitled Page" Codebehind="AgedAR.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
<script type="text/javascript" src="<%= ResolveUrl("~/Util.js") %>"></script>
<script type="text/javascript">
    function stripeTables(){
        <% foreach(Guid tableId in tableIds) { %>
            stripe("<%= tableId.ToString() %>");
        <% } %>
    }
    addLoadEvent(stripeTables);
    
    
</script>

<% if (Session["Error"] != null)
   { %>
   <%= Session["Error"].ToString() %>
   
<% Session["Error"] = null;
   } %>
<h2> Aged Receivables Report</h2>
<br />
<% if (html == null)
   { %>
    <asp:Panel ID="Panel1" GroupingText="Beginning Period" runat="server">
        <asp:DropDownList ID="DropDownListBeginMonth" runat="server">
            <asp:ListItem Value="1">January</asp:ListItem>
            <asp:ListItem Value="2">February</asp:ListItem>
            <asp:ListItem Value="3">March</asp:ListItem>
            <asp:ListItem Value="4">April</asp:ListItem>
            <asp:ListItem Value="5">May</asp:ListItem>
            <asp:ListItem Value="6">June</asp:ListItem>
            <asp:ListItem Value="7">July</asp:ListItem>
            <asp:ListItem Value="8">August</asp:ListItem>
            <asp:ListItem Value="9">September</asp:ListItem>
            <asp:ListItem Value="10">October</asp:ListItem>
            <asp:ListItem Value="11">November</asp:ListItem>            
            <asp:ListItem Value="12">December</asp:ListItem>            
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownListBeginYear" runat="server">
        </asp:DropDownList>
    </asp:Panel>
    <br />
    <br />
    <asp:Panel ID="Panel2" GroupingText="Ending Period" runat="server">
        <asp:DropDownList ID="DropDownListEndMonth" runat="server">            
            <asp:ListItem Value="1">January</asp:ListItem>
            <asp:ListItem Value="2">February</asp:ListItem>
            <asp:ListItem Value="3">March</asp:ListItem>
            <asp:ListItem Value="4">April</asp:ListItem>
            <asp:ListItem Value="5">May</asp:ListItem>
            <asp:ListItem Value="6">June</asp:ListItem>
            <asp:ListItem Value="7">July</asp:ListItem>
            <asp:ListItem Value="8">August</asp:ListItem>
            <asp:ListItem Value="9">September</asp:ListItem>
            <asp:ListItem Value="10">October</asp:ListItem>
            <asp:ListItem Value="11">November</asp:ListItem>            
            <asp:ListItem Value="12">December</asp:ListItem>                        
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownListEndYear" runat="server">
        </asp:DropDownList>        
    </asp:Panel>    
    <br />
    <br />
    <asp:Button ID="ButtonGenerate" runat="server" Text="Create Report" OnClick="ButtonGenerate_Click" />
    <% } else { %>
        <a href="<%= ResolveUrl("~/Reports/Print.aspx?" + GenerateParams()) %>">Print Friendly</a>
        <%= html %>        
    <%  html = null;
        } %>
</asp:Content>

