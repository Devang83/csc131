<%@ Control Language="C#" AutoEventWireup="true" Inherits="Tenant_PersonControl" Codebehind="PersonControl.ascx.cs" %>
<script runat="server">
    
</script>
<asp:Panel ID="Panel3" runat="server" GroupingText="Key Contact">
        <table>
            <tr><td align="right">
                    <table><tr><td align="right">Name</td><td align="right"><asp:TextBox ID="TextBoxContactName" runat="server"></asp:TextBox></td></tr></table>
                    
                    </td>
                
                <td align="right">
                    <table>
                        <tr>
                            <td align="right">Business Telephone</td>
                            <td align="right"><asp:TextBox ID="TextBoxContactOfficePhone" runat="server"></asp:TextBox>
                            </td>
                         </tr>
                     </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table><tr><td align="right">Title</td><td align="right">
                    <asp:TextBox ID="TextBoxContactTitle" runat="server"></asp:TextBox></td></tr></table>
                </td>
                <td align="right">
                    <table><tr><td align="right">Cell Telephone</td><td align="right">
                    <asp:TextBox ID="TextBoxContactCellPhone" runat="server"></asp:TextBox></td></tr></table>
                </td>                    
            </tr>
            <tr>
                <td align="right"></td>
                <td align="right">
                    <table><tr><td align="right">Home Telephone</td><td align="right">
                    <asp:TextBox ID="TextBoxContactHomePhone" runat="server"></asp:TextBox></td></tr></table></td>
                <td align="right"></td>
             </tr>
             <tr>
             <td align="right"></td>
                    <td align="right"><table><tr><td align="right">Fax</td>
                    <td align="right"><asp:TextBox ID="TextBoxContactFax" runat="server" 
                            ></asp:TextBox></td></tr></table></td>
                    
            </tr>
            <tr>
             <td align="right"></td>
                    <td align="right"><table><tr><td align="right">Email</td>
                    <td align="right"><asp:TextBox ID="TextBoxContactEmail" runat="server" 
                            Width="173px"></asp:TextBox></td></tr></table></td>
                    
            </tr>
            
            <tr>
             <td align="right"></td>
                    <td align="right"><table><tr><td align="right">Address</td>
                    <td align="right"><asp:TextBox ID="TextBoxContactAddress" runat="server" 
                            Width="280px"></asp:TextBox></td></tr></table></td>
                    
            </tr>
        </table>
        </asp:Panel>
