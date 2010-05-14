<%@ Control Language="C#" AutoEventWireup="true" Inherits="Tenant_TenantTabs" Codebehind="TenantTabs.ascx.cs" %>
<%@ Import Namespace="System.Collections.Generic" %>


<div id="tenanttabs">

	<% if (Request["TenantId"] != null)
       {
       		QuickPM.Tenant tenant = new QuickPM.Tenant(Request["TenantId"]);
           long PropertyId = tenant.Property;
           string propertyid = tenant.TenantId.Split(new char[] { '-' })[0];
			QuickPM.Property p = new QuickPM.Property(PropertyId);           
           string tenantNumber = tenant.TenantId.Split(new char[] { '-' })[1];            
           QuickPM.Property property = new QuickPM.Property(tenant.Property);
           List<string> tenantIds = property.GetTenantIds();
           if (tenant.BeginDate > DateTime.Now || tenant.EndDate < DateTime.Now)
           {
           		tenantIds = property.GetAllTenantIds();
           }
           
           %> 
    <a class="plain-directory" href="<%= ResolveClientUrl("~/Tenants/TenantPage/" + tenant.Id)%>">Main Tenant Page</a>
	&nbsp;&nbsp;
    
    <b>Property</b> <a href="<%= ResolveUrl("~/Property/PropertyPage/" + PropertyId)%>"><%= p.Name %></a>
    <b>Tenant</b>  
    <script type="text/javascript">
    	    function newUrl(location, selectControl)
	    {
		var u = location.href.replace("<%= tenant.TenantId %>", selectControl.options[selectControl.selectedIndex].value);
		return u
	    }
    </script>
    <select onchange="location.href = newUrl(location, this);">
    	<% foreach(string tId in tenantIds) 
    		{
    			QuickPM.Tenant t = new QuickPM.Tenant(tId);
    		%>
    			<% if (t.Id == tenant.Id) { %>
    				<option selected="selected" value="<%= t.TenantId %>"><%= t.Name + " (" + t.TenantId + ")" %></option>
    			<% } else { %>
    				<option value="<%= t.TenantId %>"><%= t.Name + " (" + t.TenantId + ")" %></option>
    			<% } %>
    	<% } %>
    
    </select>    
    <br/>
	<br/>
	

	<% } %>
</div>