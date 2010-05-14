<%@ Page Title="" Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" Inherits="Maps_Map" Codebehind="Map.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Map" Src="~/Maps/MapControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Map Of Properties</title>      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">    	
    	<h2>Map of Properties</h2>

<uc:Map runat="server" Id="Map"></uc:Map>       	
    	
					
	
</asp:Content>

