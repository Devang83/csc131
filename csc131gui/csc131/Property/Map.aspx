<%@ Page Title="" Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_Map" Codebehind="Map.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Map" Src="~/Maps/MapControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">      
    
<h2>Map</h2>
    	<uc:Map ID="Map" runat="server" />
</asp:Content>


