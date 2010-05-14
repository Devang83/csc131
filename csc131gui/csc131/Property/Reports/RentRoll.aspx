<%@ Page Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_RentRoll" Title="Untitled Page" Codebehind="RentRoll.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="RentRoll" Src="~/Reports/Property/RentRoll.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<center><h2><a href="<%= ResolveClientUrl("~/Property/Reports/ReportsMenu.aspx?propertyid=" + Request["PropertyId"])%>">Reports</a></h2></center>
<h3>Rent Roll</h3>
<uc:RentRoll runat="server"></uc:RentRoll>
</asp:Content>

