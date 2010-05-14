<%@ Page Language="C#" AutoEventWireup="true" Inherits="Notices_GenerateNotice" MasterPageFile="~/BaseMaster.master" Codebehind="GenerateNotice.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>Create Notice</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table>
        <tr>
        <td>Tenant#</td> <td>
            <asp:DropDownList ID="DropDownListTenants" runat="server" OnSelectedIndexChanged="DropDownListTenants_IndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </td> 
        </tr>
        
        <tr>
        
            <td>Rent Type</td>
            <td><asp:DropDownList ID="DropDownListRentTypes" runat="server">
            </asp:DropDownList></td>
        </tr>
        
        <tr>
        <td><asp:DropDownList ID="DropDownListDays" runat="server">
        <asp:ListItem Value="3" Text="3" Selected="True"></asp:ListItem>
        <asp:ListItem Value="5" Text="5" Selected="False"></asp:ListItem>        
        <asp:ListItem Value="10" Text="10" Selected="False"></asp:ListItem>        
        <asp:ListItem Value="15" Text="15" Selected="False"></asp:ListItem>        
        <asp:ListItem Value="30" Text="30" Selected="False"></asp:ListItem>        
        </asp:DropDownList> &nbsp; Day Notice
        </td>
        </tr>
        <tr>
        <td>Owner/Creditor Name:</td> <td><asp:TextBox ID="TextBoxCreditorName" runat="server" 
                        Width="399px"></asp:TextBox></td> 
        </tr>
        <tr>
        <td>Location To Mail <br /> or Deliver in Person Monies:</td> <td> 
                    <asp:TextBox ID="TextBoxDeliveryLocation" runat="server" Width="399px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>Agent Telephone# </td>
        <td><asp:TextBox ID="TextBoxAgentTelephone" runat="server" Width="145px"></asp:TextBox></td>
        </tr>
        <tr>
        <td><asp:Button ID="ButtonCreateNotice" runat="server" Text="View Notice" 
                onclick="ButtonCreateNotice_Click" style="height: 26px" /></td>
        </tr>
        </table>
    </div>
</asp:Content>