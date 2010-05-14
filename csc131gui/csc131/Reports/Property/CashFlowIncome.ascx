<%@ Control Language="C#" AutoEventWireup="true" Inherits="Reports_CashFlowIncome" Codebehind="CashFlowIncome.ascx.cs" %>
<!--[if IE]><script language="javascript" type="text/javascript" src="<%= ResolveUrl("~/Javascript/flot/excanvas.pack.js") %>"></script><![endif]-->
<script type="text/javascript" src="<%= ResolveUrl("~/Javascript/flot/jquery.js") %>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/Javascript/flot/jquery.flot.js") %>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/Javascript/dojo/dojo.js") %>">        
</script>

<script type="text/javascript" src="<%= ResolveUrl("~/Util.js") %>"></script>
<div id="content">
    <% if (moneyReceived == null)
       { %>
    <table>
    <tr><th>Income Report</th></tr>
    <tr>
        <td><asp:Label ID="Label1" runat="server" Text="Property:"></asp:Label></td>
        <td>
            <asp:DropDownList ID="PropertyList" runat="server">
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
        <asp:Button ID="ButtonGenerateChart" runat="server" Text="Generate Report" OnClientClick="Progress();" OnClick="ButtonGenerateChart_Click"/>            
    <% } %>
    <span id="LoadingPage">
    </span>
    <% if (months != null)           
           {
               
           %>
           <center><b><%= "#" + property.Id + " " + property.Name %></b></center>
           
           <asp:Panel ID="Panel3" GroupingText="Rents Received Chart" runat="server">
            <div id="chart" style="width:800px;height:300px"></div>
            
            </asp:Panel>
            <asp:Panel ID="Panel4" GroupingText="Income By Tenant" runat="server">
                <div id="barchart" style="width:800px;height:300px"></div>
            </asp:Panel>
            
            <script id="source" language="javascript" type="text/javascript">                
                dojo.require("dojo.currency");                
                $(function() {
                var d1 = [
                    <% for(int i = 0; i < months.Count; i++) { %>
                        <% if(i < months.Count - 1) { %>
                            [<%= months[i]/10000 %>, <%= moneyReceived[i] %>],
                        <% } else { %> 
                            [<%= months[i]/10000 %>, <%= moneyReceived[i] %>]
                        <% } %>
                    <% } %>
                    ];                                                                            
                    
                    
                    $.plot($("#chart"), [ { data: d1, label: "Received ($)"}], 
                                        { xaxis: {mode: "time"}, yaxis : {tickFormatter: function(v, axis) { return "$" + dojo.currency.format(v);}}});
                 });
                 
                 <%= GenerateFunction(amounts, names) %>
                 
            </script>
            
            
            
           <% 
           } %>    
           </div>