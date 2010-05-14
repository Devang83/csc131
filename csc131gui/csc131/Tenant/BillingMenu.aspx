<%@ Page Language="C#" AutoEventWireup="true" Inherits="Tenant_BillingMenu" MasterPageFile="~/Tenant/Tenant.master" Codebehind="BillingMenu.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Person" Src="~/PersonControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="Documents" Src="~/Documents/DocumentsControl.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ChildMainContent" runat="server">
<h3>Rent Schedule &amp; Billing Contact</h3>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<script type="text/javascript" src="../Javascript/rounded.js"></script>
<% if (tenant != null)
   { %>   
   <h2><a href="SendBill.aspx?tenantid=<%= tenant.TenantId %>">View Billing Statement</a></h2>   
    
<div class="panel">

			    <div style="padding-left:10px;font-size:xx-large">
        			Change Rent Schedule
        		</div>
		
<% for(int i = 0; i < tenant.RentTypes.Count; i++)
       { %>
       &nbsp;&nbsp;<a href="../Tenants/Billing/<%= tenant.Id %>?rentNum=<%= i.ToString() %>"><%= tenant.RentTypes[i] %></a>
       <br />
<% } %>   
</div>      
<br />
<div class="panel">

			    <div style="padding-left:10px;font-size:xx-large">
        			Billing Information
        		</div>
				  <br />
				  <table>
				    <tr>
				      <td align="right">Billing Address Same As Physical Location</td>
				      <td align="left">
					<asp:RadioButton ID="RadioButtonBillingSame" runat="server" Text="Yes" 
							 oncheckedchanged="RadioButtonBillingSame_CheckedChanged" AutoPostBack="true" /></td>                   
				    </tr>
				    <tr>
				      <td align="right"></td>
				      <td align="left">
					<asp:RadioButton ID="RadioButtonBillingNotSame" runat="server" Text="No" 
							 oncheckedchanged="RadioButtonBillingNotSame_CheckedChanged" AutoPostBack="true" /></td>
				    </tr>           
				  </table>
				  
				  <table>
				    <tr>
				      <td align="right">Name</td>
				      <td align="right">
					<asp:TextBox ID="TextBoxBillName" runat="server" TextMode="MultiLine"></asp:TextBox>
				      </td>
				    </tr>
				    
				    <tr>
				      <td align="right">Address</td>
				      <td align="right">
					<asp:TextBox ID="TextBoxBillingAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
				      </td>
				    </tr>
				    <tr>
				      <td align="right">City</td>
				      <td align="right">
					<asp:TextBox ID="TextBoxBillingCity" runat="server"></asp:TextBox></td>
				      <td align="right">State</td>
				      <td align="right">
					<asp:TextBox ID="TextBoxBillingState" runat="server"></asp:TextBox></td>
				      <td align="right">Zip</td>
				      <td align="right">
					<asp:TextBox ID="TextBoxBillingZip" runat="server"></asp:TextBox></td>
				    </tr>
				  </table>
</div>			
<br />
				<uc:Person ID="BillingContact"  runat="server"/>
				<br />    

<script type="text/javascript">
    Rounded('panel', 16, 16);
</script>	 
<br />

<asp:Button ID="ButtonSubmit" runat="server" onclick="ButtonSubmit_Click" onclientclick = "return IsNumber('Tenant_TextBoxUnitSize', 'Please enter a number for unit size');"
					    Text="Save" />   
				    
<% } %>
</asp:Content>

