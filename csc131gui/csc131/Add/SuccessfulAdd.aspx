<%@ Page Language="C#" AutoEventWireup="true" Inherits="Add_SuccessfulAdd" Codebehind="SuccessfulAdd.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Added</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <% if (Session["AddMessage"] != null)
       { %>
    <%= Session["AddMessage"].ToString()%>
    <% Session["AddMessage"] = null; %>
    <% } %>
    <h3></h3><a href="<%= ResolveUrl("~/Tenants/TenantPage/-1?tenantid=" + Session["AddTenantId"].ToString()) %>">Goto Tenant</a></h3>
    <br />
    <br />
    <a href="AddMenu.aspx">Add Menu</a>
    </div>
    </form>
</body>
</html>
