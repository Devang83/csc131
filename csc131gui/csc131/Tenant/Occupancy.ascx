<%@ Control Language="C#" Inherits="QuickPMWebsite.Occupancy" %>
<script type="text/javascript" src="<%= ResolveUrl("~/Util.js") %>"></script>
<script type="text/javascript">
    function stripeTables(){        
        stripe("UnitNumbers");        
    }    
</script>
<% if (Visible) { %>
<fieldset>
	<%= message %>
	<legend>Units/Suites</legend>
	

	<% if (GetTenantUnitIds().Count > 0) 
		{ %>
		<table id="UnitNumbers" cellspacing="0px" cellpadding="10px">
			<tr>
				<th>Unit#/Suite#</th><th>Move In Date</th><th>Move Out Date</th>
			</tr>
	<% } %>
	<% foreach (long unitId in GetTenantUnitIds())
	{ 
		QuickPM.PropertyUnit unit = new QuickPM.PropertyUnit(unitId);
		DateTime startDate = GetStartDate(unitId);
		DateTime endDate = GetEndDate(unitId);
	%>
		<tr>
			<td><%= unit.UnitNumber%></td>
			<td><%= startDate.ToShortDateString() %></td>
			<td><%= endDate.ToShortDateString() %></td>
			<% if (CanEdit()) { %>
			<td>(<a href="javascript:__doPostBack('EditUnit', '<%= unit.Id %>' + '^' + window.location)")>Edit</a>, <a onclick="javascript: return confirm('Delete?')" href="javascript:__doPostBack('DeleteUnit', '<%= unit.Id %>')")>Delete</a>)</td>
			<% } %>
		</tr>
		
	<% } %>
	
	<% if (GetTenantUnitIds().Count > 0) 
	{ %>
		</table>
	<% } %>
	<br/>
	<script type="text/ecmascript">		      
		 $(document).ready(function() {
	   		hideAddNew();	   
	   		stripeTables();		
 		});
		function showAddNew(selectedValue) 
		{					
			if (selectedValue == "New")
			{
				$('#AddNewUnit').css("color", "red");	
				$('#AddNewUnit').fadeIn('normal');
			} else {
				hideAddNew();
			}
			
		}		
		function hideAddNew(selectedValue)
		{
			$('#AddNewUnit').hide();			
		};
	</script>
	<div id="AddNewUnit" style="border-style:solid;padding:5px">
		Unit#<asp:TextBox Id="TextBoxUnitNumber" runat="server"></asp:TextBox>
		&nbsp;&nbsp;
		Unit Sq.Ft. <asp:TextBox Id="TextBoxUnitSqFt" runat="server"></asp:TextBox>
		<asp:Button Id="ButtonAddNewUnit" runat="server" Text="Add Unit" OnClientClick="hideAddNew();" OnClick="ButtonAddNewUnit_Click"></asp:Button>
	</div>
	<br/>				
	<asp:DropDownList id="DropDownListSelectUnit" 
	OnSelectedIndexChanged="DropDownListSelectUnit_SelectedIndexChanged" OnChange="showAddNew(this.options[this.selectedIndex].value);" runat="server">		
	</asp:DropDownList>
	<% if (CanEdit()) { %>
	&nbsp;&nbsp;Move In Date
	<% } %>
	<asp:TextBox Id="TextBoxStartDate" runat="server"></asp:TextBox>	
	<% if (CanEdit()) { %>
	&nbsp;&nbsp;Move Out Date
	<% } %>
	<asp:TextBox Id="TextBoxEndDate" runat="server"></asp:TextBox>
	&nbsp;&nbsp;
	<script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxStartDate.UniqueID.Replace("$", "_") %>").datepicker();
		        $("#<%= TextBoxEndDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });
	</script>
	
	<asp:Button runat="server" id="ButtonAddUnit" Text="Add Unit" OnClick="ButtonAddUnit_Click" ></asp:Button>
</fieldset>
<% } %>