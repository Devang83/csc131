<%@ Page Language="C#" MasterPageFile="~/Tenant/Tenant.master" AutoEventWireup="true" Inherits="Tenant_BasicLeaseInfo" Title="Untitled Page" Codebehind="BasicLeaseInfo.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<h3>Basic Lease Information</h3>
<% if (Session["Message"] != null)
   { %>
   <%= Session["Message"] %>
<% Session["Message"] = null;
   } %>
   <script language="ecmascript" type="text/ecmascript">
       function clearText(currentElement) {
           if (document.getElementById("NewLease").value == "True" && (currentElement.value == "Hot & Cold Food Products" || currentElement.value == "Yes for Hot Food Products" || currentElement.value == "None")) {
                currentElement.value = '';
            }
        }
   </script>
<br />
<input type="hidden" name="NewLease" id="NewLease" value="<%= lease.NewLease.ToString() %>" />
<table>
<% foreach(QuickPM.PropertyUnit unit in GetUnits()) { %>
<tr>
<th>Unit/Suite#<%= unit.UnitNumber %></th>
</tr>
<tr>
<%
	string sqFt = "1200";
	string insideArea = "40ftx30ft";
	string outsideSqFt = "600";
	string outsideArea = "20ftx30ft"; 
	//if (!IsPostBack)
	//{
		sqFt = unit.SqFt.ToString();
		insideArea = unit.AreaSize;
		outsideSqFt = unit.SqFtOutside.ToString();
		outsideArea = unit.AreaSizeOutside;
	//}
%>
<td><b>Sq.Ft. Unit/Suite Premises</b> &nbsp;</td><td><input type="text" value="<%= sqFt %>" name="<%= unit.Id + "SqFt" %>" id="<%= unit.Id + "SqFt" %>" ></input></td>
</tr>
<tr>
<td><b>Area Size of Premises</b> &nbsp;</td><td><input type="text" value="<%= insideArea %>" name="<%= unit.Id + "InsideArea" %>" id="<%= unit.Id + "InsideArea" %>" ></input></td>
</tr>
<tr>
<td><b>Sq.Ft. Outside Premises</b> &nbsp;</td><td><input type="text" value="<%= outsideSqFt %>" name="<%= unit.Id + "SqFtOutside" %>" id="<%= unit.Id + "SqFtOutside" %>" ></input></td>
</tr>
<tr>
<td><b>Area Size of Outside Premises</b> &nbsp;</td><td><input type="text" value="<%= outsideArea %>" name="<%= unit.Id + "OutsideArea" %>" id="<%= unit.Id + "OutsideArea" %>" ></input></td>
</tr>
<tr>
<td>&nbsp;&nbsp;</td>
</tr>
<% } %>
<tr>
<td><b>Use Of Premises</b> &nbsp;</td><td><asp:TextBox ID="TextBoxUseOfPremises" TextMode="MultiLine" runat="server" Width="500px" Text="Hot & Cold Food Products" OnClick="clearText(this);"></asp:TextBox></td>
</tr>
<tr>
<td><b>Exlusive Rights</b> &nbsp;</td><td><asp:TextBox ID="TextBoxExlusiveRights" TextMode="MultiLine" runat="server" Width="500px" Text="Yes for Hot Food Products" OnClick="clearText(this);"></asp:TextBox></td>
</tr>
<tr>
<td><b>Restriction of Use</b> &nbsp;</td><td><asp:TextBox ID="TextBoxRestrictionOfUse" TextMode="MultiLine" runat="server" Width="500px" Text="None" OnClick="clearText(this);"></asp:TextBox></td>
</tr>
</table>
<br />
    <asp:LinkButton ID="LinkButtonSubmit" runat="server" OnClick="LinkButtonSubmit_Click">Submit</asp:LinkButton>
</asp:Content>
