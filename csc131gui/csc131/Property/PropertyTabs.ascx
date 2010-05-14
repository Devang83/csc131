<%@ Control Language="C#" AutoEventWireup="true" Inherits="Property_PropertyTabs" Codebehind="PropertyTabs.ascx.cs" %>
<%@ Import Namespace="System.Collections.Generic" %>


<div id="propertytabs">

	<% if (Request["PropertyId"] != null)
       {
       		long PropertyId = long.Parse(Request["PropertyId"]);
           QuickPM.Property p = new QuickPM.Property(PropertyId);           
           
           %> 
    	   <a class="plain-directory" href="<%= ResolveClientUrl("~/Property/PropertyPage/" + PropertyId)%>">Main Property Page</a>
    
	
	
	<script type="text/javascript">
    	function newUrl(location, selectControl)
	    {
		var u = location.href.replace("<%= "propertyid=" + p.Id %>", "propertyid=" + selectControl.options[selectControl.selectedIndex].value);
		u = u.replace("<%= "PropertyId=" + p.Id %>", "PropertyId=" + selectControl.options[selectControl.selectedIndex].value);
		return u
	    }
    </script>
    <select onchange="location.href = newUrl(location, this);">
    	<% 
    	List<long> propertyIds = new List<long>(QuickPM.Util.GetPropertyIds());
    	if (!propertyIds.Contains(p.Id)) {
    		propertyIds.Add(p.Id);
    	}
    	foreach(long propertyId in propertyIds) 
    		{
    			QuickPM.Property property = new QuickPM.Property(propertyId);
    		%>
    			<% if (property.Id == p.Id) { %>
    				<option selected="selected" value="<%= property.Id %>"><%= property.Name + " (#" + property.Id + ")" %></option>
    			<% } else { %>
    				<option value="<%= property.Id %>"><%= property.Name + " (#" + property.Id + ")" %></option>
    			<% } %>
    	<% } %>
    
    </select>    
    <% } %>
</div>