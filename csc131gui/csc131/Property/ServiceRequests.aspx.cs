using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuickPMWebsite.Property
{
    public partial class ServiceRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
			QuickPM.Property property = new QuickPM.Property(GetPropertyId());
			LinkButtonAddWorkOrder.Visible = property.ACL.CanWrite(QuickPM.Database.GetUserId());			
        }

         protected void AddOrder_Click(object sender, EventArgs e)
        {			
            QuickPM.ServiceRequest request = new QuickPM.ServiceRequest();
            request.PropertyId = GetPropertyId();
			QuickPM.User user = new QuickPM.User(Context.Profile.UserName);
			if (user.Name.Trim() != "")
			{
				request.ReceivedBy = user.Name;
			} 
			else 
			{
				request.ReceivedBy = user.Email;
			}
            request.Save();
            QuickPM.WorkOrderRequest invoice = new QuickPM.WorkOrderRequest(request);
            invoice.Save();
            Response.Redirect("~/WorkOrders/WorkOrder.aspx?ServiceRequestId=" + request.Id + "&PropertyId=" + GetPropertyId());
        }


        protected long GetPropertyId()
        {
            long iPropertyId = -1;
            if (Request["PropertyId"] != null)
            {
                if(!Int64.TryParse(Request["PropertyId"], out iPropertyId))
                {
                    return -1;
                }
            }
            return iPropertyId;
        }

        protected QuickPM.Property GetProperty()
        {
            return new QuickPM.Property(GetPropertyId());
        }

        protected QuickPM.Period GetCurrentPeriod()
        {
            int year, month;
            if (Request["Year"] != null && Int32.TryParse(Request["Year"], out year))
            {

            }
            else
            {
                year = DateTime.Now.Year;
            }

            if (Request["Month"] != null && Int32.TryParse(Request["Month"], out month))
            {

            }
            else
            {
                month = DateTime.Now.Month;
            }
            return new QuickPM.Period(year, month);
        }
    }
}

