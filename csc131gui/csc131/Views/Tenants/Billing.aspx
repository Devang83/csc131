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
<h3>Rent Schedule </h3>
<%
		int rentNum = 0;
		QuickPM.Tenant tenant = null;
		if (Request["TenantId"] != null)
		{
			tenant = new QuickPM.Tenant(Request["TenantId"]);
		}
		if (ViewData["Id"] != null)
		{
			tenant = new QuickPM.Tenant((long)ViewData["Id"]);
		}
		if (Request["RentNum"] != null) {
			int.TryParse(Request["RentNum"], out rentNum);
		}
		if (tenant == null)
		{
			tenant = new QuickPM.Tenant("0000-0000");
		}
%>	

    <br />
    <h4>&nbsp;&nbsp;
    <% for(int i = 0; i < tenant.RentTypes.Count; i++) { %>
           <% if (i == rentNum)
           { %>
               <%= tenant.RentTypes[i] %>&nbsp;|&nbsp;
          <% continue; }
           %>
           <a href="../Billing/<%= tenant.Id %>?rentNum=<%= i %>"><%= tenant.RentTypes[i] %></a>&nbsp; |&nbsp;
    <% } %></h4>
    
	
          
	  
				    
	      <h2>Billings</h2>	    	      
	    	<% System.Collections.Generic.List<QuickPM.BillingRecord> records = QuickPM.BillingRecord.GetBillingRecords(tenant.TenantId, rentNum); %>

	    <script type="text/ecmascript">		      
				$(document).ready(function() {
					// add markup to container and apply click handlers to anchors
					
   					<% foreach(QuickPM.BillingRecord record in records) { %>   						
   						hideEdit(<%= record.Id %>)
   						$("#editbilling<%= record.Id %>").click(function(e){     						
     						// stop normal link click
     						e.preventDefault();
     						showEdit(<%= record.Id %>)		
   						
					});
					<% } %>
				});
				
				
				function formatCurrency(num) {
					num = num.toString().replace(/\$|\,/g,'');
					if(isNaN(num))
						num = "0";
					sign = (num == (num = Math.abs(num)));
					num = Math.floor(num*100+0.50000000001);
					cents = num%100;
					num = Math.floor(num/100).toString();
					if(cents<10)
					cents = "0" + cents;
					for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
						num = num.substring(0,num.length-(4*i+3))+','+
						num.substring(num.length-(4*i+3));
						return (((sign)?'':'-') + '$' + num + '.' + cents);
				}
				
				function hideEditLinks(recordId) 
				{
					$("#1-editlink" + recordId).hide("slow");
   					$("#2-editlink" + recordId).hide("slow");
   					
				}
				
				function showEditLinks(recordId)
				{
					$("#1-editlink" + recordId).show("slow");
   					$("#2-editlink" + recordId).show("slow");
   					
				}
				
				function showEdit(recordId) 
				{
					hideEditLinks(recordId);
					$("#1-editbillingshow" + recordId).show("slow");
   					$("#2-editbillingshow" + recordId).show("slow");
   					$("#3-editbillingshow" + recordId).show("slow");
   					$("#4-editbillingshow" + recordId).show("slow");
   					$("#5-editbillingshow" + recordId).show("slow");
   					$("#6-editbillingshow" + recordId).show("slow");
   					$("#7-editbillingshow" + recordId).show("slow");
				}				
				
				function hideEdit(recordId)
				{
					showEditLinks(recordId);
					$("#1-editbillingshow" + recordId).hide();
   					$("#2-editbillingshow" + recordId).hide();
   					$("#3-editbillingshow" + recordId).hide();
   					$("#4-editbillingshow" + recordId).hide();
   					$("#5-editbillingshow" + recordId).hide();
   					$("#6-editbillingshow" + recordId).hide();
   					$("#7-editbillingshow" + recordId).hide();
				}
								
				
				function save(recordId) 
				{
					// send request										
					var sDate = $("#editStartDate" + recordId).val();
					var eDate = $("#editEndDate" + recordId).val();
					var n = $("#editNotes" + recordId).val();
					var amt = $("#editAmount" + recordId).val();
					
					
     				$.post("../SaveBilling/" + recordId, {StartDate: sDate, EndDate : eDate, Amount : amt, Notes : n}, function(html) {
       						$("#message" + recordId).html(html);       						
       						if (html.indexOf("Saved") != -1) {
       							$("#startDate" + recordId).html(sDate);
								$("#endDate" + recordId).html(eDate);
								$("#notes" + recordId).html(n);
								$("#amount" + recordId).html(formatCurrency(amt));
								var url = window.location.href.replace("#", "");
								window.location.href = url;//reload(true);
							}
					});
					hideEdit(recordId);
					
				}
				
				function deleteRecord(recordId)
				{
					if (confirm("Delete?"))
					{
						$.post("../DeleteBilling/" + recordId, {}, function(html) {
       						location.reload(true);
						});
					}
				}
				
		</script>

	    	        <table border="1" rules="all" style="border-collapse: collapse;" cellspacing="0px">
	    
	    	<% for(int itemNum = 0; itemNum < records.Count; itemNum++) {
	    		QuickPM.BillingRecord record = records[itemNum];%>
	    		<tr> <td>
	        <% if(itemNum >= 1){ %>
	            <%= QuickPM.Util.FormatNumberAsOrdinal(itemNum) %> Adjustment
	      <% } %>	      
	      
		<table cellspacing="20px">
		    <tr>
		    <th>Rent Type</th>
		    <th>Start Date</th>
		    <th>End Date</th>
		    <th>Amount</th>
		    <th>Notes</th>		    
		    <th></th>
		    </tr>
		  <tr>		    
		    <td><%= tenant.RentTypes[rentNum] %></td>
		    <td id="startDate<%= record.Id %>"><%= record.StartDate.ToShortDateString() %></td>
		    <td id="endDate<%= record.Id %>"><%= record.EndDate.ToShortDateString() %></td>
		    <td id="amount<%= record.Id %>"><%= record.Amount.ToString("c") %></td>
		    <td id="notes<%= record.Id %>"><%= record.Notes %></td>
		    <% if (tenant.ACL.CanWrite(QuickPM.Database.GetUserId()))
		       { %><td><div id="1-editlink<%= record.Id %>"><a id="editbilling<%= record.Id%>" href="#">Edit</a></div></td>
		    <td><div id="2-editlink<%= record.Id %>"><a id="deletebilling<%= record.Id%>" href="#" onclick="javascript:deleteRecord(<%= record.Id %>)">Delete</a></div>
		    
		    </td>
		    <td><div id="message<%= record.Id %>"></div></td>
		    <% } %>
		  </tr>
		  <tr>
		  	<td><div id="1-editbillingshow<%= record.Id%>"><%= tenant.RentTypes[rentNum] %></div></td>
		    <td><div  id="2-editbillingshow<%= record.Id%>"><input id="editStartDate<%= record.Id %>" type="text" value="<%= record.StartDate.ToShortDateString() %>" /></div></td>
		    <td><div id="3-editbillingshow<%= record.Id%>"><input id="editEndDate<%= record.Id %>" type="text" value="<%= record.EndDate.ToShortDateString() %>" /></div></td>
		    <td><div id="4-editbillingshow<%= record.Id%>"><input id="editAmount<%= record.Id %>"type="text" value="<%= record.Amount.ToString("c") %>" /></div></td>
		    <td><div id="5-editbillingshow<%= record.Id%>"><input id="editNotes<%= record.Id %>" type="text" value="<%= record.Notes %>" /></div></td>
		    <td><div id="6-editbillingshow<%= record.Id%>"><a href="#" onclick="javascript:save(<%= record.Id%>)">Save</a></div></td>
		    <td><div id="7-editbillingshow<%= record.Id%>"><a href="#" onclick="javascript:hideEdit(<%= record.Id %>)">Cancel</a></div></td>
		  </tr>
		  
		</table>
		</td>
			</tr>
			<% } %>
			
	    </table>
		
	     <% 
            if (tenant.ACL.CanRead(QuickPM.Database.GetUserId()))
            {
	       %>
	    

	    <table>
	      <tr>
		<th>Rent Type</th>
		<th>Start Date</th>
		<th>End Date</th>
		<th>Amount</th>
		<th>Notes</th>
	      </tr>
	      <tr>
		<td>
		  <%= tenant.RentTypes[rentNum] %>
		</td>
		<td>
		  <input type="text" name="startdate" id="startdate"></input>
		  <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#startdate").datepicker();
		      });

		  </script>
		</td>
		<td>
		  <input type="text" name="enddate" id="enddate"></input>
		  <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#enddate").datepicker();
		      });

		  </script>
		</td>
		<td>
		  <input type="text" name="amount" id="amount" ></input>
		  
		</td>
		
		<td>
		    <input type="text" name="notes" id="notes"></input>
		</td>
		<td>
		  <input type="submit" value="Add" 
			      /></td>
	      </tr>
	    </table>	    
	    <% } %>
	    <div id="TenantErrorMessage">
	    	
	      	<%= ViewData["Error"] %>
	      	
	    </div>		
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
&nbsp;&nbsp;&nbsp; 
<script type="text/javascript">
    	
    	 $(document).ready(function() {    	 		
	   			$('.round').corner();	   			
 			});   
    	</script>
</asp:Content>     


