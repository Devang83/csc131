<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="DeleteBilling.aspx.cs"
    Inherits="QuickPMWebsite.Views.Tenants.DeleteBilling" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
 "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <div>
        <h1><%= Html.Encode(ViewData["Message"]) %></h1>
    </div>
</body>
</html>