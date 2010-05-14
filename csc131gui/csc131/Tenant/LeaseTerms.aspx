<%@ Page Language="C#" AutoEventWireup="true" Inherits="Tenant_Lease" MasterPageFile="~/Tenant/Tenant.master" Codebehind="LeaseTerms.aspx.cs" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ChildMainContent" runat="server">
<h3>Lease Terms &amp; Options</h3>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
   <script language="ecmascript" type="text/ecmascript">
       function clearText1(currentElement) {
           if (document.getElementById("NewLeaseOption").value == "True" && document.getElementById("ClearText1").value == "True") {
               currentElement.value = '';
               document.getElementById("ClearText1").value = "False";
           }
       }

       function clearText2(currentElement) {
           if (document.getElementById("NewLeaseOption").value == "True" && document.getElementById("ClearText2").value == "True") {
               currentElement.value = '';
               document.getElementById("ClearText2").value = "False";
           }
       }

       function clearText3(currentElement) {
           if (document.getElementById("NewLeaseOption").value == "True" && document.getElementById("ClearText3").value == "True") {
               currentElement.value = '';
               document.getElementById("ClearText3").value = "False";
           }
       }
   </script>
<br />
<input type="hidden" name="NewLeaseOption" id="NewLeaseOption" value="<%= newLeaseOption.ToString() %>" />

<input type="hidden" name="ClearText1" id="ClearText1" value="<%= newLeaseOption.ToString() %>" />
<input type="hidden" name="ClearText2" id="ClearText2" value="<%= newLeaseOption.ToString() %>" />
<input type="hidden" name="ClearText3" id="ClearText3" value="<%= newLeaseOption.ToString() %>" />



<% if(Session["Message"] != null) { %>
    <font color="red"> <%= Session["Message"] %></font>
<% Session["Message"] = null;
   } %>
   
   
   
<asp:Panel runat = "server" ID="PanelLeaseTerm"  GroupingText="Original Lease Term">
<table>

<tr>
<td align="right">
Lease Document Date:
</td>
<td align="right">
    <asp:TextBox ID="TextBoxLeaseDocumentDate" runat="server"></asp:TextBox>    
    <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxLeaseDocumentDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>
</td>
</tr>
<tr>
<td align="right">
Lease Commencement Date:
</td>
<td align="right">
    <asp:TextBox ID="TextBoxLeaseCommencement" runat="server"></asp:TextBox>    
    <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxLeaseCommencement.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>
</td>
</tr>
<tr>
<td align="right">
Lease Expiration Date:
</td>
<td align="right">
    <asp:TextBox ID="TextBoxLeaseExpiration" runat="server"></asp:TextBox>    
    <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxLeaseExpiration.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>
</td>
</tr>
</table>
    <asp:LinkButton ID="LinkButtonSubmit" runat="server" OnClick="LinkButtonSubmit_Click">Save</asp:LinkButton>

</asp:Panel>
<br />



<asp:Panel runat = "server" ID="Panel1" GroupingText="Rent Commencement">
<% if(rentCommencements.Count == 0) { %>
<h4>No Rent Commencements Entered</h4>
<% } %>

<table> 
<% foreach(QuickPM.RentCommencement rentCommencement in rentCommencements) { %>
    <tr>
        <td align="right"><%= tenant.RentTypes[rentCommencement.RentTypeIndex] %> :</td>
        <td align="right"><%= rentCommencement.Date.ToShortDateString() %></td>
        <% if (tenant.ACL.CanWrite(QuickPM.Database.GetUserId()))
           { %>
        <td><a href="javascript:__doPostBack('DeleteCommencement', '<%= rentCommencement.Id.ToString() %>')" onclick="javascript: return confirm('Delete?')">delete</a></td>
        <% } %>
    </tr>
<% } %>
</table>
<br />
    <% if(DropDownListRentCommencementType.Visible) { %>
    Rent Type: 
    <% } %>
    <asp:DropDownList ID="DropDownListRentCommencementType" runat="server">
    </asp:DropDownList> &nbsp; 
    <% if(DropDownListRentCommencementType.Visible) { %>    
    	Commencement Date:
    <% } %>
    
    <asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>    
    <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>
    <asp:LinkButton ID="LinkButtonRentCommencementAdd" OnClick="RentCommencementAdd_Click" runat="server">Add</asp:LinkButton>
</asp:Panel>

<br/>

<asp:Panel runat="server" ID="PanelOptionPeriods" GroupingText="Option Periods">
<% if (!hasOptions) { %>
<a href="javascript:" onclick="$('#editoptions').show()" id="addoptions">Add Options</a>
<script type="text/ecmascript">		      
		      $(document).ready(function() {		      	
		        $("#editoptions").hide();
		      });

		  </script>
<% } %>

<% if (hasOptions) { %>

		  
<br/>		  
<div id="options">
	<%
		DateTime date = lease.LeaseExpirationDate;		
		if (lease.LeaseExpirationDate >= DateTime.MaxValue.AddYears(-500)) {
			date = DateTime.Today;
		}
		date = date.AddDays(1);
		DateTime notificationDate = DateTime.Now;
		try {
			notificationDate = date.AddDays(-1 * leaseOption.NotificationDate + -1);
		} catch (Exception excep) {
			notificationDate = DateTime.Now;
		} 
	%>
			
			<%= leaseOption.NumberOptions %> <%= leaseOption.NumberOptions > 1 ? "options" : "option" %> of 
			<%= leaseOption.OptionLength %> <%= leaseOption.OptionLength > 1 ? "years" : "year" %> 
			<%= leaseOption.NumberOptions > 1 ? "each" : "" %>  
			with a notification date of <%= notificationDate.ToShortDateString() + (leaseOption.ExercisedBy.Trim() != String.Empty ? " via " + leaseOption.ExercisedBy : "") + "."%><br/>
			<br/> 					
	<table cellpadding="5px">
	<tr>
		<th>Option #</th><th>Start Date</th><th>End Date</th><th>Duration</th>
	</tr>			
		<% 
		for (int optionNumber = 1; optionNumber <= leaseOption.NumberOptions; optionNumber++) { %>
		<tr>
			<td>Option <%= optionNumber %></td> <td><%= date.ToShortDateString() %></td> <td><%= date.AddYears(leaseOption.OptionLength).ToShortDateString() %></td>
			<td><%= leaseOption.OptionLength.ToString() + (leaseOption.OptionLength > 1 ? " years" : " year") %></td>
		</tr>
		<% 
			date = date.AddYears(leaseOption.OptionLength);
		} %>
	</table>

</div>
<a href="javascript:" onclick="$('#editoptions').toggle('slow')" id="editoptionslink">Edit Options</a>
<script type="text/ecmascript">		      
		      $(document).ready(function() {		      
		        $('#editoptions').hide();		        
		        
		      });

		  </script>

<% } %>

<div id="editoptions">
<table>

<tr>
<td align="right">
Number of Options:
#</td>
<td align="left">
    <asp:TextBox ID="TextBoxNumberOptions" runat="server" Width="40px"> 
    </asp:TextBox>Options
</td>
</tr>

<tr>
<td align="right">
Option Length:
</td>
<td align="left">
    <asp:TextBox ID="TextBoxOptionLength" runat="server" Width="40px">     
    </asp:TextBox>Years
</td>
</tr>


<tr>
<td align="right">
Option Exercised By:
</td>
<td align="left">
    <asp:TextBox ID="TextBoxOptionExercisedBy" runat="server" Text="" OnClick="clearText2(this);" Width="400px"></asp:TextBox> &nbsp; &nbsp; Example: Written Notice By Tenant
</td>
</tr>
<tr>
<td align="right">
Notification Date:
</td>
<td align="left">
    <asp:TextBox ID="TextBoxOptionNotificationDate" runat="server" Width="40px"></asp:TextBox> Days Before Lease Expiration
</td>
</tr>
</table>

    <asp:LinkButton ID="LinkButtonOptions" OnClick="LinkButtonOptions_Click" runat="server" >Save</asp:LinkButton>
</div>
</asp:Panel>


<asp:Panel runat="server" ID="Panel2" GroupingText="Lease Notes">
    <asp:TextBox ID="TextBoxLeaseNotes" runat="server" Text="" Width="700px" Height="200px" TextMode="MultiLine"></asp:TextBox><br />
    <asp:LinkButton ID="LinkButtonLeaseNotes" OnClick="LinkButtonLeaseNotes_Click" runat="server">Save</asp:LinkButton>        
</asp:Panel>

</asp:Content>

