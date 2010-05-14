<%@ Page Title="" Language="C#" MasterPageFile="~/Tenant/Tenant.master" AutoEventWireup="true" Inherits="Tenant_SendBill" Codebehind="SendBill.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">   
<h3>Send Bill</h3>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
    <div>
    <table>        
        <tr>
           <th>Month</th>
           <th>Year</th>
        </tr>
        
        <tr><td>
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
        <asp:Button ID="ButtonSubmit" runat="server" Text="View PDF" 
                onclick="ButtonSubmit_Click" />
                
         <asp:Button ID="ButtonEmail" runat="server" Text="Email Billing" OnClick="ButtonEmail_Click" />
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
    
        
</asp:Content>

