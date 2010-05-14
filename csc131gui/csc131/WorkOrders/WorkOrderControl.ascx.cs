using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuickPM;

namespace QuickPMWebsite.WorkOrders
{
    public partial class WorkOrderControl : System.Web.UI.UserControl
    {
        protected WorkOrderRequest workOrder = null;
        protected ServiceRequest serviceRequest = null;
        protected string laborError = "";
        protected string errorMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(Context.Profile, Request);
            serviceRequest = new ServiceRequest();
            serviceRequest.PropertyId = -1;
            workOrder = new WorkOrderRequest();
            long id = 0;
            if (Request["ServiceRequestId"] != null && long.TryParse(Request["ServiceRequestId"], out id))
            {
                serviceRequest = new QuickPM.ServiceRequest(id);
                List<WorkOrderRequest> workOrders = QuickPM.WorkOrderRequest.Find<QuickPM.WorkOrderRequest>("ServiceRequestId", id);
                if (workOrders.Count > 0)
                {
                    workOrder = workOrders[0];                    
                }
                else
                {
                    serviceRequest.Save();
                    workOrder.ServiceRequestId = serviceRequest.Id;
                    workOrder.Save();                    
                }
            }
            if (!IsPostBack)
            {
                PopulateControls();                
                ListItem tenantItem = new ListItem("None", "");
                tenantItem.Selected = serviceRequest.TenantId == "";
                DropDownListTenant.Items.Add(tenantItem);
                ListItem it = new ListItem("Other", "-1");
                it.Selected = serviceRequest.PropertyId == long.Parse(it.Value);
                //DropDownListProperty.Items.Add(it);				
                foreach (int PropertyId in QuickPM.Util.GetPropertyIds())
                {
                    QuickPM.Property p = new QuickPM.Property(PropertyId);
                    ListItem item = new ListItem(p.Name + " (#" + PropertyId + ")", p.Id.ToString());
                    if (p.Id == serviceRequest.PropertyId)
                    {
                        item.Selected = true;

                    }
                    //DropDownListProperty.Items.Add(item);
                }
                PopulateTenantDropDown();
                
            }
            //DropDownListProperty.Enabled = Request["PropertyId"] == null;
            //DropDownListProperty.Visible = Request["PropertId"] == null;
            TextBoxContactName.Visible = DropDownListTenant.SelectedValue != "";
            TextBoxContactPhone.Visible = DropDownListTenant.SelectedValue != "";
        }

        protected void PopulateTenantDropDown()
        {
            if (serviceRequest.PropertyId != -1)
            {
                QuickPM.Property prop = new QuickPM.Property(serviceRequest.PropertyId);
                List<string> tenantIds = prop.GetTenantIds();
                foreach (string tenantId in tenantIds)
                {
                    QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
                    ListItem item = new ListItem(tenant.GetShortName() + " (#" + tenantId + ")", tenantId);
                    item.Selected = serviceRequest.TenantId == item.Value;
                    DropDownListTenant.Items.Add(item);
                    
                }

                if (serviceRequest.TenantId.Trim() == "")
                {
                    TextBoxContactName.Text = "";
                    TextBoxContactName.Enabled = false;
                    TextBoxContactPhone.Text = "";
                    TextBoxContactPhone.Enabled = false;

                    TextBoxOtherAddress.Enabled = true;
                    TextBoxOtherCity.Enabled = true;
                    TextBoxOtherKeyContact.Enabled = true;
                    TextBoxOtherNumber.Enabled = true;
                    TextBoxOtherState.Enabled = true;

                }
                else
                {

                    TextBoxContactName.Enabled = true;
                    TextBoxContactPhone.Enabled = true;



                    TextBoxOtherAddress.Enabled = false;
                    TextBoxOtherCity.Enabled = false;
                    TextBoxOtherKeyContact.Enabled = false;
                    TextBoxOtherNumber.Enabled = false;
                    TextBoxOtherState.Enabled = false;

                    TextBoxOtherAddress.Text = "";
                    TextBoxOtherCity.Text = "";
                    TextBoxOtherKeyContact.Text = "";
                    TextBoxOtherNumber.Text = "";
                    TextBoxOtherState.Text = "";                   
                }
            
            }
        }

        protected void PropertyChanged_Click(object sender, EventArgs e)
        {
            //long propertyId = long.Parse(DropDownListProperty.SelectedValue);
            //serviceRequest.PropertyId = propertyId;
            if (!serviceRequest.Save())
            {
                errorMessage = "We're sorry, there was an error saving the service request.";
            }

            
			DropDownListTenant.Items.Clear();
            ListItem tenantItem = new ListItem("None", "");
            tenantItem.Selected = true;
            DropDownListTenant.Items.Add(tenantItem);
            PopulateTenantDropDown();
        }

        protected string GetPropertyAddress()
        {
            if (serviceRequest.PropertyId == -1)
            {
                return "";
            }
            return QuickPMWebsite.AppCode.GoogleMapsFunctions.GetPropertyAddress(GetProperty());            
        }

        protected QuickPM.Property GetProperty()
        {
            if (serviceRequest.PropertyId == -1)
            {
                return new QuickPM.Property();
            }
            return new QuickPM.Property(serviceRequest.PropertyId);
        }

        protected void TenantChanged_Click(object sender, EventArgs e)
        {
            string tenantId = DropDownListTenant.SelectedValue;
            serviceRequest.TenantId = tenantId;
            if (tenantId.Trim() == "")
            {
                TextBoxContactName.Text = "";
                TextBoxContactName.Enabled = false;
                TextBoxContactPhone.Text = "";
                TextBoxContactPhone.Enabled = false;

                TextBoxOtherAddress.Enabled = true;
                TextBoxOtherCity.Enabled = true;
                TextBoxOtherKeyContact.Enabled = true;
                TextBoxOtherNumber.Enabled = true;
                TextBoxOtherState.Enabled = true;

            }
            else
            {

                TextBoxContactName.Enabled = true;
                TextBoxContactPhone.Enabled = true;



                TextBoxOtherAddress.Enabled = false;
                TextBoxOtherCity.Enabled = false;
                TextBoxOtherKeyContact.Enabled = false;
                TextBoxOtherNumber.Enabled = false;
                TextBoxOtherState.Enabled = false;

                TextBoxOtherAddress.Text = "";
                TextBoxOtherCity.Text = "";
                TextBoxOtherKeyContact.Text = "";
                TextBoxOtherNumber.Text = "";
                TextBoxOtherState.Text = "";
            }
            
            if (!serviceRequest.Save())
            {
                errorMessage = "We're sorry, there was an error saving the service request.";
            }

        }

        protected string GetTenantName()
        {
            return GetTenant().Name;
        }

        protected QuickPM.Tenant GetTenant()
        {
            string tenantId = DropDownListTenant.SelectedValue;
            string tId;
            if (QuickPM.Util.TryFormatTenantId(tenantId, out tId))
            {
                return new QuickPM.Tenant(tenantId);
                
            }
            return new QuickPM.Tenant();
        }

        protected string GetTenantAddress()
        {
            return GetTenant().Address;
        }

        protected string GetTenantCity()
        {
            return GetTenant().City;
        }

        protected string GetTenantState()
        {
            return GetTenant().State;
        }

        private Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
            {
                return root;
            }

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        } 


		public string GetWorkOrderNumber()
		{
			return serviceRequest != null ? serviceRequest.DisplayId : "";
		}
		
		
		public void PopulateContactControls()
		{
			if (serviceRequest.TenantId.Trim() == "")
            {
                TextBoxContactName.Text = "";
                TextBoxContactName.Enabled = false;
                TextBoxContactPhone.Text = "";
                TextBoxContactPhone.Enabled = false;

                TextBoxOtherAddress.Enabled = true;
                TextBoxOtherCity.Enabled = true;
                TextBoxOtherKeyContact.Enabled = true;
                TextBoxOtherNumber.Enabled = true;
                TextBoxOtherState.Enabled = true;

				
				TextBoxOtherAddress.Text = serviceRequest.Address;
                TextBoxOtherCity.Text = serviceRequest.City;
                TextBoxOtherKeyContact.Text = serviceRequest.ContactName;
                TextBoxOtherNumber.Text = serviceRequest.ContactPhone;
                TextBoxOtherState.Text = serviceRequest.State;

            }
            else
            {

                TextBoxContactName.Enabled = true;
                TextBoxContactPhone.Enabled = true;

				TextBoxContactName.Text = serviceRequest.ContactName;
                TextBoxContactPhone.Text = serviceRequest.ContactPhone;


                TextBoxOtherAddress.Enabled = false;
                TextBoxOtherCity.Enabled = false;
                TextBoxOtherKeyContact.Enabled = false;
                TextBoxOtherNumber.Enabled = false;
                TextBoxOtherState.Enabled = false;

                TextBoxOtherAddress.Text = "";
                TextBoxOtherCity.Text = "";
                TextBoxOtherKeyContact.Text = "";
                TextBoxOtherNumber.Text = "";
                TextBoxOtherState.Text = "";
            }
            
            
		}
        
        public void PopulateControls()
        {           		
            TextBoxRequestDate.Text = serviceRequest.RequestDate.ToShortDateString();
            PopulateContactControls();
			PopulateTimeControls();
            SetTimeControl(serviceRequest.RequestDate);
            SetTimeControl2(workOrder.DateContacted);       
            if (serviceRequest.RequestDate == new DateTime())
            {
                TextBoxRequestDate.Text = DateTime.Today.ToShortDateString();
                SetTimeControl(DateTime.Now);       
            }

            if (workOrder.DateContacted == new DateTime())
            {
                TextBoxDateContacted.Text = DateTime.Today.ToShortDateString();
                SetTimeControl2(DateTime.Now);
            }
            
            TextBoxRequest.Text = serviceRequest.Request;
            TextBoxAreaOfWork.Text = serviceRequest.AreaOfWork;
            CheckBoxCompleted.Checked = workOrder.WorkCompleted;
            TextBoxCompletionDate.Text = workOrder.CompletionDate != DateTime.MinValue ?
                workOrder.CompletionDate.ToShortDateString() : DateTime.Today.ToShortDateString();

            TextBoxReceivedBy.Text = serviceRequest.ReceivedBy.Trim();
            TextBoxContractor.Text = workOrder.Contractor;
            TextBoxContractorContactName.Text = workOrder.ContactPerson;
            TextBoxPhone.Text = workOrder.ContactPhone;
            TextBoxDateContacted.Text = workOrder.DateContacted.ToShortDateString();
            TextBoxWorkDone.Text = workOrder.WorkDone;
            TextBoxNotes.Text = workOrder.Notes;

        }

        public void SetTimeControl(DateTime date)
        {
            int hrs = date.Hour;
            int min = date.Minute >= 30 ? 1 : 0;
            int index = hrs * 2 + min;
            for (int i = 0; i < 48; i++)
            {
                DropDownTime.Items[i].Selected = false;
            }
            DropDownTime.Items[index].Selected = true;
        }

        public void SetTimeControl2(DateTime date)
        {
            int hrs = date.Hour;
            int min = date.Minute >= 30 ? 1 : 0;
            int index = hrs * 2 + min;
            for (int i = 0; i < 48; i++)
            {
                DropDownTime2.Items[i].Selected = false;
            }
            DropDownTime2.Items[index].Selected = true;
        }

        public void PopulateTimeControls()
        {
            for (int i = 0; i < 48; i++)
            {
                int hrs = ((int)(i/2));
                if(hrs > 12)
                {
                    hrs = hrs - 12;
                }
                string hr = hrs.ToString();
                if (hrs == 0)
                {
                    hr = "12";
                }
                string min = i % 2 == 0 ? "00" : "30";
                string ampm = i < 24 ? "am" : "pm";
                DropDownTime.Items.Add(new ListItem(hr + ":" + min + ampm, i.ToString()));
                DropDownTime2.Items.Add(new ListItem(hr + ":" + min + ampm, i.ToString()));
            }
        }

        public int[] ParseTime()
        {
            int index = DropDownTime.SelectedIndex;
            return new int[] { index / 2, index % 2 == 0 ? 0 : 30 };
        }

        public int[] ParseTime2()
        {

            int index = DropDownTime2.SelectedIndex;
            return new int[] { index / 2, index % 2 == 0 ? 0 : 30 };
        }

        public void Cancel_Click(object sender, EventArgs e)
        {
            if (Request["PropertyId"] == null)
            {
                Response.Redirect(Page.ResolveClientUrl("~/WorkOrders/WorkOrders.aspx"));
            }
            else
            {
                Response.Redirect(Page.ResolveClientUrl("~/Property/ServiceRequests.aspx?PropertyId=" + Request["PropertyId"]));
            }
        }

        public void Done_Click(object sender, EventArgs e)
        {
            if (TextBoxContactName.Enabled)
            {
                serviceRequest.ContactName = TextBoxContactName.Text.Trim();
                serviceRequest.ContactPhone = TextBoxContactPhone.Text.Trim();
                serviceRequest.Address = GetTenantAddress();
                serviceRequest.City = GetTenantCity();
                serviceRequest.State = GetTenantState();
            }
            else
            {
                serviceRequest.ContactName = TextBoxOtherKeyContact.Text.Trim();
                Console.WriteLine("serviceRequest.ContactName " + serviceRequest.ContactName);
				serviceRequest.ContactPhone = TextBoxOtherNumber.Text.Trim();
                serviceRequest.Address = TextBoxOtherAddress.Text.Trim();
                serviceRequest.City = TextBoxOtherCity.Text.Trim();
                serviceRequest.State = TextBoxOtherState.Text.Trim();
            }

            //serviceRequest.PropertyId = long.Parse(DropDownListProperty.SelectedValue);
            serviceRequest.TenantId = DropDownListTenant.SelectedValue != null ? DropDownListTenant.SelectedValue : "";
            serviceRequest.ReceivedBy = TextBoxReceivedBy.Text.Trim();
            serviceRequest.Request = TextBoxRequest.Text.Trim();
            serviceRequest.AreaOfWork = TextBoxAreaOfWork.Text.Trim();
            serviceRequest.RequestDate = DateTime.Parse(TextBoxRequestDate.Text);
            int hour = ParseTime()[0];
            int min = ParseTime()[1];
            serviceRequest.RequestDate = new DateTime(serviceRequest.RequestDate.Year, serviceRequest.RequestDate.Month,
                serviceRequest.RequestDate.Day, hour, min, 0);
            serviceRequest.Save();
            DateTime completionDate;
            if (DateTime.TryParse(TextBoxCompletionDate.Text, out completionDate))
            {
                workOrder.CompletionDate = completionDate;
            }


            DateTime contactedDate;
            if (DateTime.TryParse(TextBoxDateContacted.Text, out contactedDate))
            {
                workOrder.DateContacted = contactedDate;
            }

            int hour2 = ParseTime2()[0];
            int min2 = ParseTime2()[1];

            workOrder.DateContacted = new DateTime(workOrder.DateContacted.Year, workOrder.DateContacted.Month,
                workOrder.DateContacted.Day, hour2, min2, 0);

            

            workOrder.WorkCompleted = CheckBoxCompleted.Checked;
            workOrder.Contractor = TextBoxContractor.Text;
            workOrder.ContactPerson = TextBoxContractorContactName.Text;
            workOrder.ContactPhone = TextBoxPhone.Text;
            workOrder.WorkDone = TextBoxWorkDone.Text;
            workOrder.Notes = TextBoxNotes.Text;
            workOrder.Save();
            if (Request["PropertyId"] != null)
            {
                Response.Redirect(Page.ResolveClientUrl("~/Property/ServiceRequests.aspx?PropertyId=" + Request["PropertyId"]));
            }
            Response.Redirect("WorkOrders.aspx?PropertyId=" + serviceRequest.PropertyId);
        }
    }
}