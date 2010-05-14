<%@ Page Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_Tenants" Title="Untitled Page" Codebehind="Tenants.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<center><h2>Tenants</h2></center>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
<script type="text/javascript" src="<%= ResolveUrl("~/Util.js") %>"></script>
<script type="text/javascript">
    function stripeTables(){
        
            stripe("tableTenants");
        
    }
    addLoadEvent(stripeTables);
    
    
</script>

<% int PropertyId = -1;
    if (Request["PropertyId"] != null)
   {
       Int32.TryParse(Request["PropertyId"], out PropertyId);
   } %>
<% if (PropertyId != -1)
   { %>
   <% QuickPM.Property property = new QuickPM.Property(PropertyId);
   	System.Collections.Generic.List<string> tenantIds = property.GetTenantIds();
    if (this.all)
    {
    	tenantIds = property.GetAllTenantIds();
    }
   %>
   <br />
    <asp:CheckBox ID="CheckBoxInactive" runat="server" Text="List Inactive Tenants" AutoPostBack="true" />
   
   <br/>
   <a href="DownloadTenantContactInformation.aspx?propertyId=<%= Request["PropertyId"] %>">Download Contact Information</a>   
   <table cellpadding="10px" id="tableTenants" cellspacing="0">
   <tr>
   <th>Tenant#</th><th>Name</th><th>Location<br />Phone</th><th>Contact<br />Name</th><th>Title</th><th>Phone</th>
   <th>Fax</th><th>Email</th>
   </tr>
   <% foreach (string tenantId in tenantIds)
      {
          QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
          %>
      <tr>      
        <td><a href="<%= ResolveUrl("~/Tenants/TenantPage/" + tenant.Id + "?tenantid=" + tenant.TenantId) %>"><%= tenant.TenantId %></a></td>
        <td><%= tenant.Name %></td>
        <td><%= tenant.Phone %></td>
      <% System.Collections.Generic.List<QuickPM.Person> contacts = QuickPM.Person.GetContacts(tenant);
          if (contacts.Count > 0) { %>
          <td><%= contacts[0].Name %></td>
          <td><%= contacts[0].Title %></td>
          <td><%= contacts[0].OfficePhone %></td>
          <td><%= contacts[0].Fax %></td>
          <td><%= contacts[0].Email %></td>
          <% } %>
      </tr>
   <% } %>
   </table>
   <br />
    <asp:LinkButton ID="LinkButtonAddTenant" runat="server" OnClick="LinkButtonAddTenant_Click">Add Tenant</asp:LinkButton>
<% } %>

</asp:Content>

