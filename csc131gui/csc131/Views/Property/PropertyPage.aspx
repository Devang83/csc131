<%@ Page Language="C#" MasterPageFile="~/Views/Property/Property.master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ContentPlaceHolderID="ChildMainContent" ID="Content1" runat="server">


<div style="margin-left:auto;margin-right:auto;width:600px">
    <div class="float-left" style="padding-right:25px;" >    	
		<div class="round" style="width:200px;padding:10px;background-color: #e8eee5;">
			<h4>Property Profile</h4>
			<table>
			<tr>
			<td align="right">
				<a href="<%= ResolveClientUrl("~/Property/PropertyPage.aspx?propertyid=" + ViewData["Id"])%>"><%= new QuickPM.Property((long)ViewData["Id"]).Name %> Profile</a>
			</td>     
			</tr>    	
			</table>    
	    </div>        
	<br/>	
		<div class="round" style="width:200px;padding:10px;background-color: #e8eee5;">
			<h4>Tenants</h4>
			<table>	
			<tr>		
			<td align="right">	
				<a href="<%= ResolveClientUrl("~/Property/Tenants.aspx?propertyid=" + ViewData["Id"])%>">Tenants</a>    			
    		</td>
   			</tr>
    		</table>
		</div>    
		<br/>
		<div class="round" style="width:200px;padding:10px;background-color: #e8eee5;">
			<h4>Documents</h4>
			<table>					
			<tr>
			<td align="right">	
    			<a href="<%= ResolveClientUrl("~/Property/Documents.aspx?propertyid=" + ViewData["Id"])%>">Documents</a>    			
    		</td>    			   
    		</tr>
    		</table>
    	</div>
    	
    	<br/>
		<div class="round" style="width:200px;padding:10px;background-color: #e8eee5;">   
		<h4>Post Rents/<br/>Cash Journal</h4>
		<table>
		<tr>   
   		<td align="right">
   			<a href="<%= ResolveClientUrl("~/Property/CashJournal.aspx?propertyid=" + ViewData["Id"])%>">Post Rents/<br/>Cash Journal</a>    			    	
    	</td>
    	</tr>
    	</table>
		</div>    

		<br/>
		<div class="round" style="width:200px;padding:10px;background-color: #e8eee5;">   
		<h4>View Billings</h4>
		<table>
		<tr>   
   		<td align="right">
   			<a href="<%= ResolveClientUrl("~/Property/PrintBillings.aspx?propertyid=" + ViewData["Id"])%>">View Billings</a>    			    	
    	</td>
    	</tr>
    	</table>
		</div>    
				
     </div>
    <div class="float-right" style="padding-right:25px;" >

		<div class="round" style="width:200px;padding:10px;background-color: #e8eee5;">   
		<h4>Reports</h4>
		<table>
		<tr>   
   		<td align="right">
    			<a href="<%= ResolveClientUrl("~/Property/Reports/ReportsMenu.aspx?propertyid=" + ViewData["Id"])%>">Reports</a>    			    	
    	</td>    
   		</tr>
   		</table>
		</div>  
		<br/>
		<div class="round" style="width:200px;padding:10px;background-color: #e8eee5;">   
		<h4>Map</h4>
		<table>
		<tr>   
   		<td align="right">
   			<a href="<%= ResolveClientUrl("~/Property/Map.aspx?propertyid=" + ViewData["Id"])%>">Map</a>    			    	
    	</td>
    	</tr>
    	</table>
		</div>
		
		<br/>		
		<div class="round" style="width:200px;padding:10px;background-color: #e8eee5;">   
		<h4>Service Requests</h4>
		<table>
		<tr>   
   		<td align="right">
   			<a href="<%= ResolveClientUrl("~/Property/ServiceRequests.aspx?propertyid=" + ViewData["Id"])%>">Service Requests</a>    			    	
    	</td>
    	</tr>
    	</table>
		</div>    
		
		<br/>		
		<div class="round" style="width:200px;padding:10px;background-color: #e8eee5;">   
		<h4>Units</h4>
		<table>
		<tr>   
   		<td align="right">
   			<a href="<%= ResolveClientUrl("~/Property/Units.aspx?propertyid=" + ViewData["Id"])%>">Units</a>    			    	
    	</td>
    	</tr>
    	</table>
		</div>    
		
		
		
		
		
		
	</div>
</div>
<div style="height:70px;">
</div>
	<br/><br/>
	<div style="height:600px"> 
    </div>
    <script type="text/javascript">    	    	
    	$(document).ready(function () {       	    		
			
			$("#expiringleases").load("/Home/ExpiringLeases/<%= ViewData["Id"] %>", {}, function (responseText, textStatus, XMLHttpRequest) {  				
			});
    	
    	});
    	function loadAllLeases()
    	{
    		$("#expiringleases").load("/Home/ExpiringLeases/<%= ViewData["Id"] %>?Count=90", {}, function (responseText, textStatus, XMLHttpRequest) {  				
			});
    	}
    </script>
    <div id="expiringleases" class="round" style="width:600px;margin-left:auto;margin-right:auto;background-color: #e8eee5;">
    	
    </div>
    
	
&nbsp;&nbsp;&nbsp; 
<script type="text/javascript">
    	
    	 $(document).ready(function() {    	 		
	   			$('.round').corner();	   			
 			});   
</script>

</asp:Content>     
