<%@ Page Title="" Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_EditUnit" Codebehind="EditUnit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
    <% if (unit != null)
       { %>
       <table>
<tr>
    <th>Tenant</th>        
    <th>Unit#</th>
    <th>Sq.Ft.</th>
    <th>Area Size<br /> (e.g. 40ftx20ft)</th>
    <th>Has Outside Area?</th>
    <th>Sq.Ft.<br />Outside Area</th>
    <th>Outside Area Size<br />(e.g. 40ftx20ft)</th>    
    <th>Notes</th>
</tr>
<tr>
    <td>
        <asp:DropDownList ID="DropDownListTenant" runat="server">
        </asp:DropDownList>
    </td>
    <td><asp:TextBox ID="TextBoxUnitNumber" runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="TextBoxSqFt" runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="TextBoxAreaSize" runat="server"></asp:TextBox></td>
    <td>
        <asp:RadioButtonList ID="RadioButtonListHasOutside" runat="server">
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:RadioButtonList>
    </td>
    <td><asp:TextBox ID="TextBoxSqFtOutside" runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="TextBoxOutsideAreaSize" runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="TextBoxNotes" runat="server"></asp:TextBox></td>
    
    <td><asp:Button ID="ButtonSave"
        runat="server" Text="Finished" OnClick="ButtonSave_Click" /></td>
</tr>            
</table>
       
    <% } %>
</asp:Content>

