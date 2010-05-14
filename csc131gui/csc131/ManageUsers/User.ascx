<%@ Control Language="C#" AutoEventWireup="true" Inherits="ManageUsers_User" Codebehind="User.ascx.cs" %>
    <script type="text/javascript" src="<%= ResolveUrl("~/Util.js") %>"></script>
<script type="text/javascript">
    function stripeTables(){        
        stripe("accesstable");
    }
    addLoadEvent(stripeTables);
</script>

    
    <h2></h2>
    <% if(msg != null){   %>   
        <font color="red"><%=  msg %></font>
    <%  msg = null;
        } %>
    <br />
    <table>
    <tr>
    <td><asp:Label ID="Label1" runat="server" Text="Email:"></asp:Label></td>
    <td>
    <asp:TextBox ID="TextBoxEmailAddress" runat="server" Width="241px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td><asp:Label ID="Label2" runat="server" Text="Name:"></asp:Label></td>
    <td><asp:TextBox ID="TextBoxName" runat="server" Width="241px"></asp:TextBox></td>
    </tr>
    </table>
    <asp:LinkButton ID="LinkButtonSaveEmailName" runat="server" 
        onclick="LinkButtonSaveEmailName_Click">Save Email & Name</asp:LinkButton>
    <br />

    <a href="EditPassword.aspx?userName=<%= Request["userName"]%>">Change Password</a>
    <br />
    <br />
    <div>
         
        <asp:Panel ID="Panel1" runat="server" GroupingText="User Profile">               
        <asp:RadioButton AutoPostBack="true" ID="RadioButtonManager" runat="server" 
                Text="Administrator (Can Add/Delete Users)" 
                oncheckedchanged="RadioButtonManager_CheckedChanged" /><br />
        <asp:RadioButton AutoPostBack = "true" ID="RadioButtonUser" runat="server" Text="User" 
                oncheckedchanged="RadioButtonUser_CheckedChanged" />
        </asp:Panel>
        
        
        <br />
        <br />
       
        <!--  -->
        <br />
        
    </div>
    <br />