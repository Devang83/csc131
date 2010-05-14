<%@ Control Language="C#" Inherits="QuickPMWebsite.RentRoll" %>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<script type="text/javascript" src="<%= ResolveUrl("~/Util.js") %>"></script>
<script type="text/javascript">
    function stripeTable(){
        //stripe("<%= tableId %>");        
    }
    addLoadEvent(stripeTable);
</script>
    <% if (Session["RentRollHtml"] == null)
       { %>
    
    <table>
    
    <tr>
    <td><% if(Request["PropertyId"] == null) { %> Property# <% } %></td><td><asp:DropDownList ID="DropDownListProperty" runat="server">
    </asp:DropDownList></td>
    </tr>
    <tr>
    <td>Year</td>
    <td><asp:DropDownList ID="DropDownListYear" runat="server">
    </asp:DropDownList></td>
    </tr>
    <tr>
    <td>Month</td>
    <td><asp:DropDownList ID="DropDownListMonth" runat="server">
    <asp:ListItem Value = "1">January</asp:ListItem>
    <asp:ListItem Value = "2">February</asp:ListItem>
    <asp:ListItem Value = "3">March</asp:ListItem>
    <asp:ListItem Value = "4">April</asp:ListItem>
    <asp:ListItem Value = "5">May</asp:ListItem>
    <asp:ListItem Value = "6">June</asp:ListItem>
    <asp:ListItem Value = "7">July</asp:ListItem>
    <asp:ListItem Value = "8">August</asp:ListItem>
    <asp:ListItem Value = "9">September</asp:ListItem>
    <asp:ListItem Value = "10">October</asp:ListItem>
    <asp:ListItem Value = "11">November</asp:ListItem>
    <asp:ListItem Value = "12">December</asp:ListItem>
    </asp:DropDownList></td>
    </tr>
    </table>
    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
        onclick="ButtonSubmit_Click" />
    <% }
       else
       { %>
       <a href="<%= ResolveUrl("~/Reports/Property/DownloadRentRoll.aspx")%>?propertyid=<%= GetPropertyId() %>&year=<%= DropDownListYear.SelectedValue %>&month=<%=DropDownListMonth.SelectedValue%>">Print Friendly</a>    <br />
       <a href="<%= ResolveUrl("~/Reports/Property/DownloadCSVRentRoll.aspx")%>?propertyid=<%= GetPropertyId() %>&year=<%= DropDownListYear.SelectedValue %>&month=<%=DropDownListMonth.SelectedValue%>">Download Excel Spreadsheet</a>           
       <%= Session["RentRollHtml"] %>
        
    <% Session["ReportHtmlPrint"] = Session["RentRollHtml"];
       Session["ReportTitle"] = "Rent Roll";
       Session["RentRollHtml"] = null;
       } %>