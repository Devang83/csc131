<%@ Page Title="" Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" CodeBehind="EditMaterialLineItem.aspx.cs" Inherits="QuickPMWebsite.WorkOrders.EditMaterialLineItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Edit Material Line Item</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table>
        <tr>
            <th>
                Description of Part or Material
            </th>
            <th>
                Quantity Used
            </th>
            <th>
                Purchase Cost
            </th>
            <th>
                Chargeback
            </th>
        </tr>               
                
        <tr>
            <td>
                <asp:TextBox ID="TextBoxDescription" Width="200px" TextMode="MultiLine" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="TextBoxQuantityUsed" runat="server" Width="75px"></asp:TextBox>                
            </td>
            
            <td>
                <asp:TextBox ID="TextBoxPurchaseCost" runat="server"></asp:TextBox>
            </td>
            
            <td>
                <asp:TextBox ID="TextBoxChargeback" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:LinkButton ID="LinkButtonFinish" OnClick="Finish_Click" runat="server">Finish</asp:LinkButton> &nbsp; &nbsp;
                <a href="javascript: window.location=document.referrer;">Cancel</a>
            </td>
        </tr>
        
    </table>
</asp:Content>
