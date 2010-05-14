<%@ Control Language="C#" AutoEventWireup="true" Inherits="TopbarControl" Codebehind="Topbar.ascx.cs" %>
<%@ Import Namespace="System.Collections.Generic" %>


<script language="C#" runat="server">

    

</script>


<% 
     int numDisplayedItems = 4;
     Dictionary<string, Dictionary<string, string>> menuItems = new Dictionary<string,Dictionary<string,string>>();
     List<string> menuItemsIds = new List<string>(new string[] { "/", 
     "Volunteer/", "Event/", "Reports/", });
     //menuItems.Add("EditUser.aspx", CreateMenu.CreateMenuItem("MyAccount","~/ManageUsers", "EditUser.aspx", "?username=" + this.Profile.UserName, "My Account"));
     menuItems.Add("/", CreateMenu.CreateMenuItem("~", "Home", "Default.aspx", "", "Home", 1));
     menuItems.Add("Volunteer/", CreateMenu.CreateMenuItem("~/Volunteer", "Volunteer Menu", "VolunteerMenu.aspx", "", "Volunteers", 1));
     menuItems.Add("Event/", CreateMenu.CreateMenuItem("~/Event", "Event Menu", "EventMenu.aspx", "", "Events", 1));
     menuItems.Add("Reports/", CreateMenu.CreateMenuItem("~/Reports", "Reports Menu", "ReportsMenuPage.aspx", "", "Reports", 1));
     if(!this.Profile.IsAnonymous && Roles.IsUserInRole("Manager")) {
        menuItems.Add("ManageUsers/", CreateMenu.CreateMenuItem("~/ManageUsers", "Create, Delete, and Modify users", "ManageUsersMenuPage.aspx", "", "Users", 1));     
        menuItemsIds.Add("ManageUsers/");
     }
     
     string currentPage = CreateMenu.GetCurrentPage(this.Request, 1);
     if(menuItemsIds.IndexOf(currentPage) >= numDisplayedItems - 1)
     {
        menuItemsIds.Remove(currentPage);
        menuItemsIds.Insert(1, currentPage);
     }
    
    %>
<link rel="stylesheet" href="<%= ResolveUrl("~/Css/dropdown.css") %>" type="text/css" /> 
<script type="text/javascript" src="<%= ResolveUrl("Javascript/dropdown.js") %>"></script>
<div class="header">
    
   <span id="right-element">
        <% if(!this.Profile.IsAnonymous) {
               string email = Membership.GetUser(Profile.UserName).Email;
               %>
        &nbsp; <%= email %> &nbsp; | &nbsp;             
        <% } %>
       
        &nbsp; <asp:LoginStatus ID="UserLoginStatus" runat="server" 
        OnLoggedOut="UserLoginStatus_LoggingOut" />&nbsp;
        <% if (!this.Profile.IsAnonymous)
           { %>
        | &nbsp;
        <%= CreateMenu.CreateMenuItemHtml(this.Page, CreateMenu.CreateMenuItem("~/ManageUsers", "Edit Your Preferences", "EditUser.aspx", "?username=" + this.Profile.UserName, "My Account", 1), false)%>&nbsp; 
        <% } %> 
    </span> 
    <span id="left-element">
    &nbsp;
    <% for(int i = 0; i < numDisplayedItems; i++){ %>    
        <%= CreateMenu.CreateMenuItemHtml(this.Page, menuItems[menuItemsIds[i]], false) %>        
    <% } %>
    </span>   
    <div>
    <dl class="dropdown"> 
         <dt id="one-ddheader" onmouseover="ddMenu('one',1)" onmouseout="ddMenu('one',-1)">More</dt>
        
        <dd id="one-ddcontent" onmouseover="cancelHide('one')" onmouseout="ddMenu('one',-1)">
            <ul class="dropdown">
            <% for(int i = numDisplayedItems; i < menuItemsIds.Count; i++) 
               { %>
                <li class="dropdown"><%= CreateMenu.CreateMenuItemHtml(this.Page, menuItems[menuItemsIds[i]], true) %></li>                
            <% } %>
            </ul>
        </dd> 
    </dl>
    </div>    
</div>
<br />
<br />
