<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="RecentActivity.aspx.cs"
    Inherits="QuickPMWebsite.Views.Home.RecentActivity" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<% if ((bool)ViewData["fullPage"] == true) { %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
 "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
<% } %>    
<div style="padding-left:20px;font-size:xx-large;">
    Recent Activity
    </div>
	<div id="recentactivity" style="padding-left:10px;">
		<table cellpadding="10px">
			<tr>
				<th>Type</th><th>Name/Id</th><th>Modified</th><th>Created</th><th>Modified By</th>
			</tr>
			<%foreach(VolunteerTracker.ActiveRecord record in (List<VolunteerTracker.ActiveRecord>)ViewData["Activity"]) { %>
        	<tr>
        		<%= DisplayRecord(record) %>
        	</tr>
        	<% } %>  
        </table>

</div>
<% if ((bool)ViewData["fullPage"] == true) { %>
</body>
</html>
<% } %>
