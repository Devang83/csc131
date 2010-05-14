<%@ Page Title="" Language="C#" MasterPageFile="~/Tenant/Tenant.master" AutoEventWireup="true" Inherits="Tenant_Map" Codebehind="Map.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="Map" Src="~/Maps/MapControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<h3>Map</h3>        
<h2>Tenant Map</h2>
    	<uc:Map runat="server" Id="Map"></uc:Map>       
</asp:Content>

