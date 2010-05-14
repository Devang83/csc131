<%@ Control Language="C#" AutoEventWireup="true" Inherits="Tenant_Tenant" Codebehind="Tenant.ascx.cs" %>
<%@ Register TagPrefix="uc" TagName="Units" Src="~/Tenant/Occupancy.ascx" %>
<script type="text/javascript" src="../Util.js"></script>
<script runat="server">
    

    public string Name
    {
        get
        {
            return TextBoxName.Text;
        }
        set
        {
            TextBoxName.Text = value;
        }
    }

    public string Phone
    {
        get
        {
            return TextBoxPhone.Text;
        }
        set
        {
            TextBoxPhone.Text = value;
        }
    }

    public string Address
    {
        get
        {
            return TextBoxLocation.Text;
        }
        set
        {
            TextBoxLocation.Text = value;
        }
    }

    public string State
    {
        get
        {
            return TextBoxState.Text;
        }
        set
        {
            TextBoxState.Text = value;
        }
    }

    public string Zip
    {
        get
        {
            return TextBoxZip.Text;
        }

        set
        {
            TextBoxZip.Text = value;
        }
    }

    public string City
    {
        get
        {
            return TextBoxCity.Text;
        }

        set
        {
            TextBoxCity.Text = value;
        }
    }

    public string BillingEmail
    {
        get
        {
            return TextBoxBillingEmail.Text;
        }
        set
        {
            TextBoxBillingEmail.Text = value != null ? value : "";
        }
        
    }
    
    public bool Active
    {
        get
        {
            return RadioButtonActive.Checked;
        }
        set
        {
            RadioButtonActive.Checked = value;
        }
    }

    
    /*public ASP.personcontrol_ascx Contact1
    {
        get
        {
            return KeyContact1;
        }
    }*/
</script>

<script type="text/javascript">
</script>
         <% if (Session["Message"] != null)
            { %>
            <br />
            <font color="red"><%= Session["Message"] %></font>
            <br />
            <%
                Session["Message"] = null;
                } %>

         <% if (Session["Error"] != null)
            { %>
            <%= Session["Error"].ToString() %>
            <%
                Session["Error"] = null;
                } %>                  
                  <div>
				  <asp:Panel ID="Panel1" runat="server" Height="60px">
				    <table>
				      <tr>
					<td align="right">Tenant#</td>
					<td align="left">
					  <asp:TextBox ID="TextBoxTenantId" runat="server"></asp:TextBox></td>
				      </tr>
				      <tr>
				  
					<td align="right">Tenant/Leasee Name:</td>
					<td align="right">
					  <asp:TextBox ID="TextBoxName" runat="server" Height="100px" Width="267px" 
                            TextMode="MultiLine"></asp:TextBox></td>
                           <td><asp:Panel ID="Panel4" runat="server" GroupingText="Current Status"> 
                            <table>
				      <tr>					
					<td align="left">
					  <asp:RadioButton ID="RadioButtonActive" runat="server" Text="Active" 
							   oncheckedchanged="RadioButtonActive_CheckedChanged" AutoPostBack="true" /></td>                   
				      </tr>
				      <tr>					
					<td align="left">
					  <asp:RadioButton ID="RadioButtonInactive" runat="server" Text="Inactive" 
							   oncheckedchanged="RadioButtonInactive_CheckedChanged" AutoPostBack="true" /></td>
							   <% if (tenant != null && tenant.EndDate < DateTime.Now)
                                { %>
                                    <td>As Of <%= tenant.EndDate.ToShortDateString() %></td>
							   <% } %>							   
				      </tr>
				    </table>
				  </asp:Panel>
				
                           </td>
				      </tr>
				    </table>
				  </asp:Panel>
				  <br />
				  <br />
				  <br />
				  <br />
				  <asp:Panel ID="Panel2" runat="server" GroupingText="Tenant/Lease Premises" 
					     Height="169px">
				    <table>
				      <tr>
					<td align="right">Address</td>
					<td align="right">
					  <asp:TextBox ID="TextBoxLocation" runat="server" Height="66px" TextMode="MultiLine" Width="246px"></asp:TextBox></td>
                    </tr>			      					  
					      <tr>
                              <td align="right">
                                  City</td>
                              <td align="left">
                                  <asp:TextBox ID="TextBoxCity" runat="server"></asp:TextBox>
                              </td>
                              <td align="right">
                                  State</td>
                              <td align="left">
                                  <asp:TextBox ID="TextBoxState" runat="server"></asp:TextBox>
                              </td>
                              <td align="right">
                                  Zip</td>
                              <td align="left">
                                  <asp:TextBox ID="TextBoxZip" runat="server"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td align="right">Phone</td>
                              <td align="left">
                                  <asp:TextBox ID="TextBoxPhone" runat="server"></asp:TextBox>
                              </td>
                              <td align="right">Email To Send Billing Statements</td>
                              <td align="left">
                                  <asp:TextBox ID="TextBoxBillingEmail" runat="server"></asp:TextBox> &nbsp; &nbsp; <% if (tenant != null)
                                                                                                                       { %><a href="SendBill.aspx?tenantid=<%= Request["TenantId"] %>">Send Bill</a> <% } %>
                              </td>
                          </tr>				      					
				    </table>
				    
				    <br/>
				    <asp:Button ID="ButtonSubmit" runat="server" onclick="ButtonSubmit_Click" onclientclick = "return IsNumber('Tenant_TextBoxUnitSize', 'Please enter a number for unit size');"
					    Text="Save" />      

				  </asp:Panel>
				  <br />
				  <br />
				  <br />				  
				  <% foreach(QuickPM.Person contact in contacts) {%>
				        <br />
                        <%= Contact.DisplayContact(contact.Id, this.Page) %>				  
				  <% } %>
				  <% if (tenant != null && tenant.ACL.CanWrite(QuickPM.Database.GetUserId())) { %>
				  <br />				  
				  <a href="<%= ResolveUrl("~/People/Person.aspx?NewContact=yes&AssociatedKey=" + typeof(QuickPM.Tenant).Name + " " + tenant.Id) + "&return=" + this.Request.RawUrl %>">Add Contact</a>
				  <% } %>
				</div>
				<br />
								
                 <uc:Units id="Units" runat="server"></uc:Units>
                          

				

