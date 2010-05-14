<%@ Control Language="C#" AutoEventWireup="true" Inherits="Property_PropertyControl" Codebehind="Property.ascx.cs" %>
   <style type="text/css">
        .style1
        {
            width: 146px;
        }
    </style>
<script type="text/javascript" src="../Util.js"></script>
<script type="text/javascript">
    function stripeTables() {
        stripe("RentTypes");        
    }
    addLoadEvent(stripeTables);
</script>
    <div id="ErrorMessage"> 
        <% if (Session["PropertyErrorMessage"] != null)
           { %>
                <%= Session["PropertyErrorMessage"] %>
        <% Session["PropertyErrorMessage"] = null;
           } %>
     </div>
    <% if (Session["AddPropertyFinished"] != null)
       {
           Session["AddPropertyFinished"] = null;
           %>
        <br /><h2>Property Successfully Added.</h2>
    <% } %>
    
    
    <% if (Session["PropertyChangesSubmitted"] != null)
       {
           Session["PropertyChangesSubmitted"] = null;
           %>
        <br /><h2>Changes Sucessfully Submitted.</h2>
    <% } %>
<br />
<asp:Panel ID="Panel1" runat="server" GroupingText="Property" Height="219px" 
    Width="550px">
    <table>
    <tr><td class="style1">Property #</td>
    <td><asp:TextBox ID="TextBoxPropertyId" runat="server"></asp:TextBox></td>
    </tr>
    
    <tr>
        <td class="style1">Property Name:</td>
        <td><asp:TextBox ID="TextBoxPropertyName" Width="400px" runat="server"></asp:TextBox></td>            
        
        <td>
        	<asp:Panel ID="Panel4" runat="server" GroupingText="Current Status"> 
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
				      </tr>
				    </table>
				  </asp:Panel>
        </td>
    </tr>
    <tr><td class="style1">Legal Name:</td><td><asp:TextBox ID="TextBoxLegalName" Height="100px" Width="400px" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr><td class="style1"></td>
                </tr>
    
    <tr>
    <td class="style1">Physical Property Address:</td><td><asp:TextBox ID="TextBoxPhysicalAddress" runat="server" 
        Height="50px" TextMode="MultiLine" Width="400px"></asp:TextBox>
        </td>
        </tr>
                    
    </table>
</asp:Panel>

<br />
<br />
<br />
<br />
<br />
<br />
<br />

<% foreach(QuickPM.Person contact in QuickPM.Person.GetContacts(GetProperty())) {%>
      <br />
      <%= Contact.DisplayContact(contact.Id, this.Page) %>				  
<% } %>

<br />
<% if(GetProperty().ACL.CanWrite(QuickPM.Database.GetUserId())) {  %>
<a href="<%= ResolveUrl("~/People/Person.aspx?NewContact=yes&AssociatedKey=" + typeof(QuickPM.Property).Name + " " + Request["PropertyId"]) + "&return=" + this.Request.RawUrl %>">Add Contact</a>
<% } %>
<br />
<br />
<asp:Panel ID="Panel2" runat="server" GroupingText="Income Types">
<table id="RentTypes">
<tr><th>Name</th><th>Chart of Account</th></tr>
<%  QuickPM.Property property = new QuickPM.Property(int.Parse(Request["PropertyId"]));
    for (int rentTypeIndex = 0; rentTypeIndex < property.RentTypes.Count; rentTypeIndex++ )
    { %>
        <tr>
        <td><%= property.RentTypes[rentTypeIndex]%></td>
        <td><%= property.ChartOfAccounts.ContainsKey(rentTypeIndex) ? property.ChartOfAccounts[rentTypeIndex].ToString() : "None"%></td>
        <% if (GetProperty().ACL.CanWrite(QuickPM.Database.GetUserId()))
           { %>
        <td><a href="EditRentType.aspx?PropertyId=<%= property.Id.ToString() %>&rentNum=<%= rentTypeIndex %>">edit</a> </td>
        <% } %>
        </tr>
    <% } %>
</table>
<br />
Name &nbsp; 
    <asp:TextBox ID="TextBoxRentTypeName" runat="server"></asp:TextBox> &nbsp; &nbsp; Chart Of Account &nbsp;
    <asp:TextBox ID="TextBoxRentTypeChartOfAccount" runat="server"></asp:TextBox>
    &nbsp;&nbsp;<asp:Button ID="ButtonAddRentType" runat="server" OnClick="ButtonAddRentType_Click" Text="Add" />
</asp:Panel> 
<br />

<asp:Panel ID="Panel3" runat="server" GroupingText="Remitance Information">
    <table>
        <tr>
        <td>Name:</td>
        <td><asp:TextBox ID="TextBoxRemitName" runat="server"></asp:TextBox></td>
        </tr>
    <tr>
    <td>Address:</td>
    <td><asp:TextBox ID="TextBoxRemitAddress" runat="server" Width="289px"></asp:TextBox></td>
    </tr>
    <tr>
    <td>City:</td><td><asp:TextBox ID="TextBoxRemitCity" runat="server" Width="188px"></asp:TextBox></td>
    </tr>
    <tr>
    <td>State:</td>
    <td><asp:TextBox ID="TextBoxRemitState" runat="server" Width="34px"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Zip:</td>
    <td><asp:TextBox ID="TextBoxRemitZip" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Telephone:</td>
    <td><asp:TextBox ID="TextBoxRemitTelephone" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Fax:</td>
    <td><asp:TextBox ID="TextBoxRemitFax" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Email:</td>
    <td><asp:TextBox ID="TextBoxRemitEmail" runat="server" Width="212px"></asp:TextBox></td>
    </tr>
    </table>
</asp:Panel>

<br />
<asp:Button ID="ButtonSubmit" runat="server" onclick="ButtonSubmit_Click" OnClientClick="return IsNumber('TextBoxPropertyId', 'Please enter a number for Property #.');"
    Text="Submit" />               