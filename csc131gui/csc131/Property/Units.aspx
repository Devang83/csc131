<%@ Page Title="" Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_Units" Codebehind="Units.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<center><h2>Units</h2></center>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<script type="text/javascript">
    function stripeTables() {
        stripe("units");
    }
    addLoadEvent(stripeTables);
</script>
<% if (Session["Message"] != null)
   { %>
   <br />
   <%= Session["Message"] %>
<% Session["Message"] = null;
   } %>    
<% if(units != null) { %>
    <table cellpadding="10px" id="units">
    <tr>
        <th>Tenant</th>        
        <th>Unit Number</th>
        <th>Sq.Ft.</th>
        <th>Area Size<br />(e.g. 40ftx20ft)</th>
        <th>Has Outside<br />Area?</th>
        <th>Sq.Ft. Outside<br />Area</th>
        <th>Outside Area<br />Size (e.g. 40ftx20ft)</th>    
        <th>Notes</th>    
     </tr>
    <% System.Collections.Generic.Dictionary<long, string> tenantIds = QuickPM.PropertyUnit.GetCurrentTenantIds(property.Id);
       int totalSqFt = 0;
       int totalSqFtOutside = 0;
       foreach (QuickPM.PropertyUnit unit in units)
       {
           totalSqFt += unit.SqFt;
           totalSqFtOutside = unit.HasOutside ? unit.SqFtOutside : 0;
           string unitText = "Vacant";
           if (tenantIds[unit.Id] != "")
           {
               unitText = tenantIds[unit.Id] + " " + new QuickPM.Tenant(tenantIds[unit.Id]).Name;
               unitText = unitText.Substring(0, unitText.Length > 20 ? 20 : unitText.Length);
           }
           %>
        <tr>
            <% if (tenantIds[unit.Id] != "")
               { %>
            <td><a href="../Tenants/TenantPage/<%= new QuickPM.Tenant(tenantIds[unit.Id]).Id %>"><%= unitText%></a></td>
            <% } else { %>
                <td><%= unitText%></td>
            <% } %>
            <td><%= unit.UnitNumber %></td> 
            <td><%= unit.SqFt.ToString("n0") %></td>
            <td><%= unit.AreaSize %></td>
            <td><%= unit.HasOutside ? "Yes" : "No" %></td>
            <td><%= unit.SqFtOutside.ToString("n0") %></td>
            <td><%= unit.AreaSizeOutside %></td>
            <td><%= unit.Notes %></td>
            <% if (property.ACL.CanWrite(QuickPM.Database.GetUserId()))
               { %>
            <td>
            <a href="EditUnit.aspx?id=<%= unit.Id %>">Edit</a>, <a href="javascript:__doPostBack('DeleteUnit','<%= unit.Id %>')" onclick="javascript:return confirm('Delete?')">Delete</a>
            </td>
            <% } %>
        </tr>        
    <% } %>
        <tr>
            <td>Total</td>
            <td></td> 
            <td><%= totalSqFt.ToString("n0") %></td>
            <td></td>
            <td></td>
            <td><%= totalSqFtOutside.ToString("n0") %></td>
            <td></td>
            <td></td>
            <td>            
            </td>
        </tr>        


    </table>
<% } %>

<table>
<% if(property.ACL.CanWrite(QuickPM.Database.GetUserId())) { %>
<tr>

        <th>Tenant</th>        
        <th>Unit Number</th>
        <th>Sq.Ft.</th>
        <th>Area Size<br />(e.g. 40ftx20ft)</th>
        <th>Has Outside<br />Area?</th>
        <th>Sq.Ft. Outside<br />Area</th>
        <th>Outside Area<br />Size (e.g. 40ftx20ft)</th>    
        <th>Notes</th> 
        
</tr>
<% } %>
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
            <asp:ListItem Selected="False">Yes</asp:ListItem>
            <asp:ListItem Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
    </td>
    <td><asp:TextBox ID="TextBoxSqFtOutside" runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="TextBoxOutsideAreaSize" runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="TextBoxNotes" runat="server"></asp:TextBox></td>
    
    <td><asp:Button ID="ButtonAdd"
        runat="server" Text="Add Unit" OnClick="ButtonAdd_Click" /></td>
</tr>            
</table>
</asp:Content>

