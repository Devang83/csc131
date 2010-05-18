<%@ Page Language="C#" AutoEventWireup="true" Inherits="Home" MasterPageFile="~/BaseMaster.master" Codebehind="Default.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Login" Src="~/Login.ascx" %>
    
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>Volunteer-Tracker</title>    
<script type="text/javascript">
    function onbodyload(){		
    }
    function onbodyunload(){

    }
    
</script> 
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
        
    <%                 
        if (Profile.IsAnonymous)
       { %>
        <uc:Login id="Login" runat="server" />
    <% } else { %>
    <br />
    Welcome to <a href="About.aspx">Volunteer-Tracker</a>.     
    <div style="margin-left:auto;margin-right:auto;width:900px">
    <div class="float-left" style="padding-right:25px;" >
        <div class="highlight" style="width:375px">        		
        		<div style="padding-left:10px;font-size:xx-large">
        			Volunteers
        		</div>        		
        		<br/>
                <table cellpadding="5px">
                <tr>
                <td>
                <a class="button-link" href="<%= ResolveUrl("~/SearchForms/ListVolunteers.aspx") %>">List Volunteers</a>
                </td>
                <td>                
                <a class="button-link" href="<%= ResolveUrl("~/Add/AddVolunteer.aspx") %>">Add Volunteer</a>                        
                </td>                
                </tr>
                <tr>
                <td>
                <a class="button-link" href="<%= ResolveUrl("~/SearchForms/FindVolunteers.aspx") %>">Find Volunteers</a>
                </td>
                </tr>
                </table>
        </div>
        
        <br />
        <br />
        <div class="highlight" style="width:375px">        		
        		<div style="padding-left:10px;font-size:xx-large" >
        			Statistics
        		</div>        		
        		<br/>
                &nbsp;<a class="button-link" href="<%= ResolveUrl("~/Reports/Statistics.aspx") %>">Statistics</a>
		</div>
        
    </div>
    <div class="float-right" >
    	<div class="highlight">        		
        		<div style="padding-left:10px;font-size:xx-large" >
        			Events
        		</div>
        		<br/>
                <table cellpadding="5px">
                <tr>                
                <td><a class="button-link" href="<%= ResolveUrl("~/SearchForms/ListEvents.aspx") %>">List Events</a>
                </td>
                <td>
                <a class="button-link" href="<%= ResolveUrl("~/Add/AddEvent.aspx") %>">Add Event</a>
                </td>
                
                </tr>
                </table>
        </div>
        
        <br />
        <div style="width:500px" class="highlight">         	
        	<div style="padding-left:10px;font-size:xx-large">
        		Reports
        	</div>        	
        	<br/>
            <table cellpadding="5px">
            <tr>
            <td><a class="button-link" href="<%= ResolveUrl("~/Reports/PropertyMonthlyReport.aspx") %>">Volunteer Report</a></td>
            <td><a class="button-link" href="<%= ResolveUrl("~/Reports/Property/CashFlowIncome.aspx") %>">Events Report</a></td>
            </tr>            
            </table>
        </div>
        
        
    	
    </div>
    </div>
    
    <div style="height:300px"> 
    </div>
    <script type="text/javascript">    	    	
    	$(document).ready(function () {       	
    		$("#calendar").load("/Home/Events/-1", {}, function (responseText, textStatus, XMLHttpRequest) {
  				//Rounded('highlight2', 16, 16);
			});
						
    	
    	});    	
    </script>
    
    
    <div style="height:100px"> 
    </div>
    
    <div id="calendar" class="highlight" style="width:800px;margin-left:auto;margin-right:auto;">
    	
    </div>
    
    <div style="height:100px"> 
    </div>
    
    
    <script type="text/javascript">    	    	
    	$(document).ready(function () {       	
    		$("#recentactivity").load("/Home/RecentActivity/-1", {}, function (responseText, textStatus, XMLHttpRequest) {
  				Rounded('highlight', 16, 16);
			});
						
    	
    	});    	
    </script>
    <div id="recentactivity" class="highlight" style="width:800px;margin-left:auto;margin-right:auto;">
    	
    </div>
    
    <br/>
    <br/>
    
    
    
    <script type="text/javascript">        
	</script>
    <% } %>  
</asp:Content>  
