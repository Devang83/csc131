<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="ForgotPassword.aspx.cs"
    Inherits="QuickPMWebsite.Views.Home.ForgotPassword" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
 "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1">
    <title>Forgot Password</title>
</head>
<body>
	<h2><a href="<%= Page.ResolveUrl("~/") %>">Back</a></h2>
    <% using (Html.BeginForm()) { %> 
    
    <% if (ViewData["Error"] != null)
       { %>
            <%= ViewData["Error"] %>
    <% ViewData["Error"] = null;
       } %>
    <% if(ViewData["ForgotPasswordSent"] == null){  %>
    <div>
    
    <h2>Reset Your Password</h2>
    
        Email <%= Html.TextBox("TextBoxEmail", "") %>               
            <br />
        <br />
        <input type="submit" value="Reset Password"></input>        
    </div>
    
    <% } else { %>
        <h3>New password sent to <%= ViewData["ForgotPasswordEmail"].ToString() %></h3>
    <% 
        ViewData["ForgotPasswordSent"] = null;
        ViewData["ForgotPasswordEmail"] = null;
       } %>
    
    <% } %>
</body>
</html>