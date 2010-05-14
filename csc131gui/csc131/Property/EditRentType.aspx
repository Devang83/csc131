<%@ Page Title="" Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_EditRentType" Codebehind="EditRentType.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
  <br />
  <% if(Session["Error"] != null){ %>
    <%= Session["Error"] %>
    <br />
  <%    Session["Error"] = null;
     } %>
     
     <table>
    <tr><td align="right">Name </td><td><asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Chart Of Account </td><td><asp:TextBox ID="TextBoxChartOfAccount" runat="server"></asp:TextBox></td>
  </tr>
  </table>
<br />
<br />
    <asp:Button ID="ButtonSubmit" runat="server" OnClick="ButtonSubmit_Click" Text="Finished" />
</asp:Content>

