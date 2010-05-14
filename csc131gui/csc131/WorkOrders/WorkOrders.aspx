<%@ Page Title="" Language="C#" MasterPageFile="~/BaseMaster.master" AutoEventWireup="true" CodeBehind="WorkOrders.aspx.cs" Inherits="QuickPMWebsite.WorkOrders.WorkOrders" %>
<%@ Register TagPrefix="uc" TagName="Calendar" Src="~/CalendarControl/CalendarControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Work Orders</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
    <legend>Select Property, Year, & Month</legend>
        
    Property &nbsp;
    <asp:DropDownList ID="DropDownListProperty" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PropertyChanged_Click">
    
    </asp:DropDownList>
    <br />
    <br />
        
    <uc:Calendar ID="ucCalendar" runat="server" />        
    
</fieldset>
<% System.Collections.Generic.List<QuickPM.ServiceRequest> requests = 
               QuickPM.ServiceRequest.Find<QuickPM.ServiceRequest>("PropertyId", PropertyId);

   int i = 0;
   DateTime beginDate = new DateTime(GetCurrentPeriod().Year, GetCurrentPeriod().Month, 1);
   int daysInMonth = DateTime.DaysInMonth(beginDate.Year, beginDate.Month);
   DateTime endDate = new DateTime(beginDate.Year, beginDate.Month, daysInMonth, 23, 59, 59);
   while (i < requests.Count)
   {

       if (requests[i].RequestDate > endDate || requests[i].RequestDate < beginDate)
       {
           requests.RemoveAt(i);
       }
       else
       {
           i++;
       }
   }
    
    %>
<% if (requests.Count == 0)
   { %>   
   <br />
   <h2> No Service Requests On File For <%= GetCurrentPeriod().ToString() %></h2>
<% } else { %>
<fieldset>
    <legend>
        Service Requests
    </legend>

    <table>
        <tr>
            <th>
                Date
            </th>
            <th>
                Property
            </th>
            <th>Number</th> 
            <th>Description</th>
            <th>Completed</th>
        </tr>
               
         <% foreach (QuickPM.ServiceRequest request in requests)
            { %>
                <tr>
                    <td><%= request.RequestDate.ToShortDateString() %></td>
                    <td>
                        <% string pName = "Other";
                           if (request.PropertyId != -1)
                           {
                               pName = new QuickPM.Property(request.PropertyId).Name + " (#" + request.PropertyId + ")";
                           }    
                         %>
                        <%= pName %>
                    </td>
                    <td>
                        <a href="WorkOrder.aspx?ServiceRequestId=<%= request.Id %>"><%= request.Id %></a>
                    </td>
                    <td>                        
                        <%= request.Request.Length > 20 ? request.Request.Substring(0, 20) + "..." : request.Request%>
                    </td>
                    <%
                    	QuickPM.WorkOrderRequest invoice = new QuickPM.WorkOrderRequest(request);
						
             
                    %>
                    <td><%= invoice.WorkCompleted ? "Yes" : "No" %></td>
                </tr>
         <% } %>
    </table>
<br />
<br />


</fieldset>
<% } %>
<br />
<br />
    <asp:LinkButton ID="LinkButtonAddWorkOrder" OnClick="AddOrder_Click" runat="server">Add Service Request</asp:LinkButton>
    <br />
</asp:Content>
