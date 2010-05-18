<%@ Page Language="C#"
    AutoEventWireup="true"    
    Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>

<div style="padding-left:20px;font-size:x-large;">
    Current And Future Events 
    <%
    	System.Collections.Generic.List<VolunteerTracker.Event> events = (System.Collections.Generic.List<VolunteerTracker.Event>)(ViewData["Events"]);
    %>
    <table cellpadding="10px" cellspacing="0px">
    <tr>
    	<th>Name</th>
    	<th>Date</th>
    	<th>Time</th>
    </tr>
    <% foreach(VolunteerTracker.Event e in events) { %>
    	<% string hour = "";
    	if (e.Date.Hour < 12) 
    	{
    		hour = e.Date.Hour.ToString() + "am";
    	} else {
    		if (e.Date.Hour == 12)
    		{
    			hour = "12pm";
    		}
    		hour = (e.Date.Hour - 12).ToString() + "pm";
    	}
    	%>
    	
    	<tr>
    		<td><a href="Event/Event.aspx?Id=<%= e.Id %>"><%= e.Name %></a></td><td><%= e.Date.ToShortDateString() %></td>
    		<td><%= hour %></td>
    		
    	</tr>
    <% } %>
    </table>
    </div>
	


