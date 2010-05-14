<%@ Page Language="C#" Inherits="QuickPMWebsite.RentRollReport" MasterPageFile="~/BaseMaster.master" %>
<%@ MasterType VirtualPath="~/BaseMaster.master" %>
<%@ Register TagPrefix="uc" TagName="RentRoll" Src="~/Reports/Property/RentRoll.ascx" %>
<%@ Register TagPrefix="uc" TagName="ReportsMenu" Src="~/Reports/ReportsMenu.ascx" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
<title>Rent Roll</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
<uc:ReportsMenu runat="server" />
<div id="content">
	<uc:RentRoll runat="server"></uc:RentRoll>
</div>
</asp:Content>

