<%@ Page Language="C#" MasterPageFile="~/Views/Tenants/Tenant.master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ContentPlaceHolderID="ChildMainContent" ID="Content1" runat="server">

<script type="text/javascript">
    function onbodyload()
    {

    }
    
    function onbodyunload()
    {

    }
    
</script>
<% if (ViewData["Message"] != null) { %>
	<%= ViewData["Message"].ToString() %>
<% } %>
<h3>Edit Unit</h3>
<% long unitId = (long)ViewData["UnitId"];
	QuickPM.PropertyUnit unit = new QuickPM.PropertyUnit(unitId);
	QuickPM.Tenant tenant = new QuickPM.Tenant((long)ViewData["Id"]);
	
%>
<table id="UnitTable" cellspacing="0px" cellpadding="10px">
<tr>
	<th>Unit#/Suite#</th>
	<th>Move In Date</th>
	<th>Move Out Date</th>
</tr>
<tr>
	<td><%= unit.UnitNumber %></td>
	<% DateTime startDate = DateTime.MinValue;
		DateTime endDate = DateTime.MaxValue;
		if (tenant.UnitStartDates.ContainsKey(unitId)) {
			startDate = tenant.UnitStartDates[unitId];
		}
		if (tenant.UnitEndDates.ContainsKey(unitId)){
			endDate = tenant.UnitEndDates[unitId];
		}
	%>
	<td><input type="text" name="startdate" id="startdate" value="<%= startDate.ToShortDateString() %>"></input></td>
	<td><input type="text" name="enddate" id="enddate" value="<%= endDate.ToShortDateString() %>"></input></td>
</tr>
</table>
<br/>
<br/>
&nbsp;&nbsp;<input type="submit" value="Done"></input>
<input type="button" onclick="window.location = '<%= Server.UrlDecode(Request["backlink"]) %>'" value="Cancel"></input>

&nbsp;&nbsp; 
<script type="text/javascript">
    	
    	 $(document).ready(function() {    	 		
	   			$('.round').corner();	   			
 			});   
    	</script>
</asp:Content>     


