<%@ Page Language="C#" MasterPageFile="~/Views/Tenants/Tenant.master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ContentPlaceHolderID="ChildMainContent" ID="Content1" runat="server">

<%
	List<Dictionary<string, string>> menuItems = new List<Dictionary<string, string>>();
	QuickPM.Tenant tenant = new QuickPM.Tenant();    
    if (ViewData["Id"] != null && (long)ViewData["Id"] >= 0)
    {    	
    	tenant = new QuickPM.Tenant((long)ViewData["Id"]);
    }
    
    if (Request["TenantId"] != null)
    {    	
    	tenant = new QuickPM.Tenant(Request["TenantId"]);
    	
    }
    
    
    string tenantId = tenant.TenantId;
    %>

<div style="margin-left:auto;margin-right:auto;width:1024px">
    <div class="float-left" style="padding-right:25px;" >    	
		<div class="round" style="width:400px;padding:10px;background-color: #e8eee5;">
			<h2>Lease Information</h2>
			<table>
			<tr>
			<td align="right">
    		<a id="Tenant.aspx" title="Tenant Profile" class="plain-directory"  href="../../Tenant/TenantProfile.aspx?tenantid=<%= tenant.TenantId %>">Tenant Name & Premises</a> &nbsp; 
    		</td>     
			<td align="right">
    		<a id="BasicLeaseInfo.aspx" title="Basic Lease Information" class="plain-directory"  href="../../Tenant/BasicLeaseInfo.aspx?tenantid=<%= tenant.TenantId %>">Basic Lease Info</a> &nbsp; 
    		</td>
			</tr>    
		
			<tr>
			<td align="right">	
    		<a id="LeaseTerms.aspx" title="Tenant Lease Information" class="plain-directory"  href="../../Tenant/LeaseTerms.aspx?tenantid=<%= tenant.TenantId %>">Lease Terms & Options</a> &nbsp; 
    		</td>	
    		<td align="right">   	
    		<a id="SecurityDeposit.aspx" title="View and Modify Security Deposit" class="plain-directory"  href="../../Tenant/SecurityDeposit.aspx?tenantid=<%= tenant.TenantId %>">Security Deposit</a> &nbsp; 
    		</td>
			
    		</tr>   
			</table>    
	    </div>    
    
	<br/><br/>
	
		<div class="round" style="width:400px;padding:10px;background-color: #e8eee5;">
			<h2>Billing</h2>
			<table>	
			<tr>		
			<td align="right">	
    		<a id="BillingMenu.aspx" title="Change Billing Information" class="plain-directory"  href="../../Tenant/BillingMenu.aspx?tenantid=<%= tenant.TenantId %>">Billing Address &amp; Rent Schedule</a> &nbsp;     		
    		</td>
   			<td align="right">
    		<a id="AR.aspx" title="View Tenant Ledger" class="plain-directory"  href="../../Tenant/AR.aspx?tenantid=<%= tenant.TenantId %>">Ledger</a> &nbsp; 
    		</td>
    		</tr>
    		</table>
		</div>    
		<br/>
		<br/>
		<div class="round" style="width:400px;padding:10px;background-color: #e8eee5;">
			<h2>Insurance</h2>
			<table>					
			<tr>
			<td align="right">	
    		<a id="Insurance.aspx" title="View and Edit Insurance Information" class="plain-directory"  href="../../Tenant/Insurance.aspx?tenantid=<%= tenant.TenantId %>">Insurance</a> &nbsp; 
    		</td>    			   
    		</tr>
    		</table>
    	</div>
     </div>
    <div class="float-right" style="padding-right:25px;" >

		<div class="round" style="width:400px;padding:10px;background-color: #e8eee5;">   
		<h2>Reports & Documents</h2>
		<table>
		<tr>   
   		<td align="right">
    	<a id="Reports.aspx" title="View Tenant Reports" class="plain-directory"  href="../../Tenant/Reports/Reports.aspx?tenantid=<%= tenant.TenantId %>">Reports</a> &nbsp; 
    	</td>    
   		</tr>
   		<tr>
   		<td>
   		<a id="Documents.aspx" title="Upload or View Tenant Documents" class="plain-directory"  href="../../Tenant/Documents.aspx?tenantid=<%= tenant.TenantId %>">Documents</a> &nbsp;     	
   		</td>
   		</tr>
    	</table>
		</div>  
<br/>
<br/>		
		<div class="round" style="width:400px;padding:10px;background-color: #e8eee5;">   
		<h2>Map</h2>
		<table>
		<tr>   
   		<td align="right">
    	<a id="Map.aspx" title="Map" class="plain-directory"  href="../../Tenant/Map.aspx?tenantid=<%= tenant.TenantId %>">Map</a> &nbsp;     
    	</td>
    	</tr>
    	</table>
		</div>    
		
	</div>
</div>
<div style="height:600px;">
</div>
	<br/><br/>
&nbsp;&nbsp;&nbsp; 
<script type="text/javascript">
    	
    	 $(document).ready(function() {    	 		
	   			$('.round').corner();	   			
 			});   
    	</script>

</asp:Content>     
