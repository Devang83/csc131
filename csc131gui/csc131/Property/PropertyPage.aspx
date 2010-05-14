<%@ Page Language="C#" AutoEventWireup="true" Inherits="Property_PropertyPage" MasterPageFile="~/Property/Property.master" Codebehind="PropertyPage.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Property" Src="~/Property/Property.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="ChildMainContent">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<% string name = "";
if (Request["PropertyId"] != null && Request["PropertyId"].Trim() != "") {
	name = new QuickPM.Property(long.Parse(Request["PropertyId"])).Name;
}
%>
<center><h3><%= name %> Profile</h3></center>
   <div>
    <uc:Property Id="Property" runat="server" />
    </div>
</asp:Content> 