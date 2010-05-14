<%@ Page Language="C#"
    AutoEventWireup="true"    
    Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="System.Web.Routing" %>
    <div>
    	<% if (ViewData["Message"] != null) { %>
    	<h3><%= Html.Encode(ViewData["Message"]) %></h3>
    	<% } %>
    	<% if (ViewData["Error"] != null) { %>
    	<h3><%= Html.Encode(ViewData["Error"]) %></h3>
    	<% } %>
        
    </div>