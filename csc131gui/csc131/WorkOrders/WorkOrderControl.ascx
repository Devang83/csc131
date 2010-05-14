<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkOrderControl.ascx.cs" Inherits="QuickPMWebsite.WorkOrders.WorkOrderControl" %>

<script type="text/javascript" src="../Javascript/rounded.js"></script>
<center><h2>Service Request Order </h2></center>
<% if (errorMessage.Trim() != "")
   { %>
<h2><font color="red">
<%= errorMessage%>
</font></h2>
<% 
    errorMessage = "";
   }  %>

<div class="requestpanel" style="background-color:#E0EAF1;width:1200px;">

&nbsp;&nbsp;Date of Request &nbsp;&nbsp; <asp:TextBox ID="TextBoxRequestDate" Width="150px" runat="server"></asp:TextBox>
            
&nbsp;&nbsp;&nbsp;Time of Request
    <asp:DropDownList ID="DropDownTime" runat="server">
    </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;            
    &nbsp;&nbsp;Received By <asp:TextBox ID="TextBoxReceivedBy" runat="server"></asp:TextBox>                
&nbsp;&nbsp; Service Request #<%= GetWorkOrderNumber() %>
</asp:TextBox>
<br />
</div>
<br />
<fieldset style=" width:1200px;">
    <legend>Service Request</legend>



<div id="container-block" style="position:relative;">

<div style="position:absolute; padding-left:10px; top:0px; left:0px">
<b>Property</b> 
    <% if (Request["PropertyId"] != null)
       { %>
            <%= new QuickPM.Property(long.Parse(Request["PropertyId"])).Name + " (#" + Request["PropertyId"] + ")" %>

    <% } %>

<div class="requestpanel" style="background-color:#E0EAF1;width:300px;position:relative; top:10px;">
    
    <div style="padding-left:10px;">
        <table>
        <tr>
        <td><b>Name</b></td><td> <%= GetProperty().Name %></td>
        </tr>
        
        <td><b>Address</b></td> <td>  <%= GetPropertyAddress() %>    </td>
        </table>
    </div>

</div>       

<div style="position:absolute; top:0px; left:325px;">    
    
        <strong>Tenant</strong> &nbsp; <asp:DropDownList ID="DropDownListTenant" OnSelectedIndexChanged="TenantChanged_Click" runat="server" AutoPostBack="true" >
                       </asp:DropDownList>
                                    
    <div class="requestpanel" style="background-color:#E0EAF1;width:400px;position:relative; top:10px;">    
        <div style="padding-left:10px;">
    <table>        
        <tr>
            <td>
                <strong>Name</strong> 
            </td>
            <td><%= GetTenantName() %></td>
        </tr>
        <tr>
            <td>                                
                <strong>Address</strong> 
            </td>
            <td style="width:300px;">
                <%= GetTenantAddress() +  (GetTenantAddress().Trim() != "" ? "," : "")%>
       
            <%= GetTenantCity().Trim() + (GetTenantCity().Trim() != "" ? "," : "") %>                                
            &nbsp; <%= GetTenantState() %>            
            </td>                    
        </tr>                
               
        <tr>            
            <td style="width:150px;">
                <strong>Key Contact </strong>
            </td>
            <td>
                <asp:TextBox ID="TextBoxContactName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>            
            <td>            
                <strong>Phone Number </strong>
            </td>                
            <td>
                <asp:TextBox ID="TextBoxContactPhone" runat="server"></asp:TextBox>            
            </td>
        </tr>                       
    </table>                
    </div>
    </div>
    </div> 
    
         
    <div style="position:absolute; top:0px; left:740px;">
    <b>Other</b>
    <div class="requestpanel" style="background-color:#E0EAF1;width:450px; position:relative;top:10px;">    
        <div style="padding-left:10px;">
    <table>
        <tr>
            <td>                                
                <strong>Address</strong>
            </td>
            <td><asp:TextBox ID="TextBoxOtherAddress" TextMode="MultiLine" Width="300px" runat="server"></asp:TextBox>                                
            </td>
                        
        </tr>
        
        <tr>
            <td>                                
                <strong>City</strong>
            </td>
            <td>            
                <asp:TextBox ID="TextBoxOtherCity" Width="200px" runat="server"></asp:TextBox>                                
                &nbsp;<strong>State</strong> <asp:TextBox ID="TextBoxOtherState" Width="30px" runat="server"></asp:TextBox>            
            </td>            
        </tr>                
               
        <tr>            
            <td>
                <strong>Key Contact</strong>
            </td>
            <td>
                <asp:TextBox ID="TextBoxOtherKeyContact" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>            
            <td>            
                <strong>Phone Number</strong> 
            </td>                
            <td>
                <asp:TextBox ID="TextBoxOtherNumber" runat="server"></asp:TextBox>            
            </td>
        </tr>                       
    </table>                
    </div>
    </div>
    </div>  
</div>   
    <br />
    
    <br />
    <br />
    <br />
    
    <br /> 
    <br />
<br />    
    <br />
    <br />
    <br />
<br />   
<br /> 
    <div>
        <div style="float:left;padding-right:10px;padding-left:10px;">
            <span style="font-size:larger; font-weight:bold;">Description of Work Area </span> &nbsp; &nbsp; <br /> <asp:TextBox ID="TextBoxAreaOfWork" TextMode="MultiLine" Width="573px" Height="250px" runat="server"></asp:TextBox>
        </div>
        <div style="float:left;padding-right:10px;padding-left:10px;">
            <span style="font-size:larger;font-weight:bold;">Description of Problem</span>
            <br /><asp:TextBox ID="TextBoxRequest" TextMode="MultiLine" Width="573px" Height="250px" runat="server"></asp:TextBox>
        </div>
    </div>


<br />


<fieldset style="width:1200px;">

    <legend>Action Taken</legend>
    <div style="position:relative;height:125px;">
    <div class="requestpanel" style="background-color:#E0EAF1;width:600px;position:absolute; top:0px; left:0px;">    
    <table>
        <tr>
            <td>&nbsp;&nbsp;Contractor</td>
            <td><asp:TextBox ID="TextBoxContractor" Width="200px" runat="server"></asp:TextBox></td>
            
            <td>&nbsp;Date Contacted</td>
            <td><asp:TextBox ID="TextBoxDateContacted" Width="100px" runat="server"></asp:TextBox></td>
            
        </tr>
        
        <tr>
            <td>&nbsp;&nbsp;Contact Person </td>
            <td><asp:TextBox ID="TextBoxContractorContactName" Width="200px" runat="server"></asp:TextBox></td>
            
            <td>&nbsp;Time Contacted</td>
            <td>    <asp:DropDownList ID="DropDownTime2" runat="server">
    </asp:DropDownList></td>
            
        </tr>
        <tr>
            <td>&nbsp;&nbsp;Phone #</td>
            <td><asp:TextBox ID="TextBoxPhone" runat="server"></asp:TextBox></td>            
        </tr>
        
                
        
    </table>          
      
    </div>
    
    <div class="requestpanel" style="background-color:#E0EAF1;width:500px;position:absolute; top:0px; left:700px;">    
    	<table>
    		<tr>
    			<td><b>Work Completed</b></td>
    		
        		<td><asp:CheckBox ID="CheckBoxCompleted" runat="server" /></td>
        	</tr>
        	<tr>
        		<td><b>Work Completion Date </b> &nbsp;</td>
    			<td><asp:TextBox ID="TextBoxCompletionDate" Width="80px" runat="server"></asp:TextBox></td>    	
        	</tr>
        
    
        </table>
        <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxCompletionDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>
    
    </div>
    
    </div>
    <br />
    <div >
    
        <div style="float:left;padding-right:10px;">
        <span style="font-size:larger; font-weight:bold;">Work That Was Done:</span>
                <br />
        <asp:TextBox ID="TextBoxWorkDone" TextMode="MultiLine" Width="573px" Height="200px" runat="server"></asp:TextBox>                
        </div>
        
        <div style="float:inherit;">
            <span style="font-size:large; font-weight:bold;">Follow-up Instructions:</span>
            <br />
        <asp:TextBox ID="TextBoxNotes" TextMode="MultiLine" Width="573px" Height="200px" runat="server"></asp:TextBox>               
        </div>
    <br />
    
    </div>    
          
</fieldset>
<br />
&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButtonDone" Font-Size="Large" Font-Bold="true" OnClick="Done_Click" runat="server">Done</asp:LinkButton>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButtonCancel" Font-Size="Large" Font-Bold="true" OnClick="Cancel_Click" runat="server">Cancel</asp:LinkButton>
<br />

<script type="text/javascript">
    Rounded('requestpanel', 16, 16);
</script>	 