using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuickPMWebsite.WorkOrders
{
    public partial class WorkOrders : System.Web.UI.Page
    {
        private QuickPM.Property property = null;
        protected long PropertyId = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (QuickPM.Util.GetPropertyIds().Length == 0)
            {
                return;
            }

            long iPropertyId = QuickPM.Util.GetPropertyIds()[0];



            if (Request["PropertyId"] != null)
            {
                Int64.TryParse(Request["PropertyId"], out iPropertyId);
            }

            if (!IsPostBack)
            {
                foreach (int PropertyId in QuickPM.Util.GetPropertyIds())
                {
                    QuickPM.Property p = new QuickPM.Property(PropertyId);
                    ListItem item = new ListItem(p.Name + " (#" + PropertyId + ")", p.Id.ToString());
                    item.Selected = p.Id == iPropertyId;
                    DropDownListProperty.Items.Add(item);
                }
                ListItem it = new ListItem("Other", "-1");
                it.Selected = iPropertyId == -1;
                DropDownListProperty.Items.Add(it);

            }
            string sPropertyId = DropDownListProperty.SelectedValue;
            if (sPropertyId == null)
            {
                return;
            }
            long pNumber;
            if (!long.TryParse(sPropertyId, out pNumber))
            {
                return;
            }
            this.PropertyId = pNumber;
        }

        protected void PropertyChanged_Click(object sender, EventArgs e)
        {
            string sPropertyId = DropDownListProperty.SelectedValue;            
            if (!long.TryParse(sPropertyId, out PropertyId))
            {
                throw new Exception("sPropertyId:" + sPropertyId);

            }

            QuickPM.Period period = GetCurrentPeriod();

            Response.Redirect(Request.Url.AbsolutePath + "?PropertyId=" + PropertyId + "&year=" + period.Year + "&month=" + period.Month);
        
        }


        protected void AddOrder_Click(object sender, EventArgs e)
        {
            QuickPM.ServiceRequest request = new QuickPM.ServiceRequest();
			QuickPM.User user = new QuickPM.User(Context.Profile.UserName);
			if (user.Name.Trim() != "")
			{
				request.ReceivedBy = user.Name;
			} 
			else 
			{
				request.ReceivedBy = user.Email;
			}
			//request.ContactName = 
            request.PropertyId = PropertyId;
            request.Save();
            QuickPM.WorkOrderRequest invoice = new QuickPM.WorkOrderRequest(request);
			invoice.Save();
            Response.Redirect("WorkOrder.aspx?ServiceRequestId=" + request.Id);
        }


        protected QuickPM.Property GetProperty()
        {
            if (property == null)
            {
                property = new QuickPM.Property(PropertyId);
            }
            return property;
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
