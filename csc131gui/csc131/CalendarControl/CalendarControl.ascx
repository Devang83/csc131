<%@ Control Language="C#" AutoEventWireup="true" Inherits="CalendarControl_CalendarControl" Codebehind="CalendarControl.ascx.cs" %>
<table width = "20%">
    <tr>
    <% 
        VolunteerTracker.Period period = GetCurrentPeriod();
        
        VolunteerTracker.Period previousPeriod = period.SubtractMonth();
        VolunteerTracker.Period nextPeriod = period.AddMonth();        
    %>    
    <td><a href="javascript:__doPostBack('PreviousPeriod')">Previous</a></td>
    <td>    
        <asp:DropDownList ID="DropDownListSelectMonth" runat="server" OnSelectedIndexChanged="SelectMonth_TextChanged" AutoPostBack="true">
        </asp:DropDownList>                    
        </td>
    <td><a href="javascript:__doPostBack('NextPeriod')">Next, </a></td>    
    </tr>
    </table>