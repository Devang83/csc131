<%@ Control Language="C#" AutoEventWireup="true" Inherits="Billings_BillingsControl" Codebehind="BillingsControl.ascx.cs" %>
<% if(Session["BillingViewBillings"] != null) {
           //Session["BillingViewBillingsYear"] = Session["BillingViewBillings"];
           %>
           <a href="<%= ResolveUrl("~/Billings/ViewPrint.aspx") %>">View Billings</a>
            <%= Session["BillingViewBillings"] %>
    <%      Session["BillingViewBillings"] = null;
       } else { %>    
    
    <div>
    <table>        
        <tr>
        <% if (Request["PropertyId"] == null)
           { %>
           <th>Property#</th>            
           <%} %>
           <th>Month</th>
           <th>Year</th>
        </tr>
        
        <tr><% if(Request["PropertyId"] == null){ %>
            <td>
                <asp:DropDownList ID="DropDownListPropertyId" runat="server">
                </asp:DropDownList>  
            </td>                        
            <%} %>
            <td>
                <asp:DropDownList ID="DropDownListMonth" runat="server">
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
                </asp:DropDownList>
            </td>
            
            <td>
                <asp:DropDownList ID="DropDownListYear" runat="server">
                </asp:DropDownList>
            </td>
            
        </tr>
    </table>
        <asp:Button ID="ButtonSubmit" runat="server" Text="Download" 
                onclick="ButtonSubmit_Click" />
                
         <asp:Button ID="ButtonEmail" runat="server" Text="Email Billings" OnClick="ButtonEmail_Click" />
    </div>
    <br />    
    <br />
    
    <% if(Session["email"] != null) { %>
        <%= Session["email"] %>
            <script type="text/javascript" src="<%= ResolveUrl("~/Util.js") %>"></script>
            <script type="text/javascript">
            function stripeTables() {
                stripe("email");
            }
            addLoadEvent(stripeTables);
            </script>

    <% Session["email"] = null;
       } %>    
    
    <% } %>