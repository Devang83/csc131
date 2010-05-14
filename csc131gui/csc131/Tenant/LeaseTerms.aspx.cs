using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

public partial class Tenant_Lease : System.Web.UI.Page
{
    protected QuickPM.Tenant tenant = null;
    protected List<QuickPM.RentCommencement> rentCommencements = new List<QuickPM.RentCommencement>();
    protected QuickPM.LeaseOption leaseOption = null;
	protected QuickPM.Lease lease = null;
    protected bool newLeaseOption = true;
	protected bool hasOptions = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.MaintainScrollPositionOnPostBack = true;                
		LoadTenant();				
		
        if (tenant != null)
        {
            if (QuickPM.ActiveRecord.Find<QuickPM.LeaseOption>("TenantId", tenant.TenantId).Count > 0)
            {
                newLeaseOption = false;
            }
            bool canWrite = tenant.ACL.CanWrite(QuickPM.Database.GetUserId());            
			LinkButtonSubmit.Visible = canWrite;
            LinkButtonSubmit.Enabled = canWrite;
            LinkButtonRentCommencementAdd.Visible = canWrite;
            LinkButtonRentCommencementAdd.Enabled = canWrite;            
			LinkButtonOptions.Visible = canWrite;
            LinkButtonOptions.Enabled = canWrite;
            LinkButtonLeaseNotes.Visible = canWrite;
            LinkButtonLeaseNotes.Enabled = canWrite;
            if (!canWrite)
            {
                QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);        
				DropDownListRentCommencementType.Visible = false;
				DropDownListRentCommencementType.Enabled = false;
				TextBoxDate.Visible = false;
				TextBoxDate.Enabled = false;				
            }
        }

        if (Request.Form["__EVENTTARGET"] == "DeleteCommencement")
        {
            DeleteCommencement(Request.Form["__EVENTARGUMENT"]);
        }
		LoadOption();
		LoadCommencementDates();
        if (!this.IsPostBack) {
			
        	LoadLease();
        	
            
        	if (tenant != null)
        	{
        		foreach (string rentType in tenant.RentTypes)
            	{
            		DropDownListRentCommencementType.Items.Add(new ListItem(rentType, tenant.RentTypes.IndexOf(rentType).ToString()));
            	}
            	if (DropDownListRentCommencementType.Items.Count > 0)
            	{
                	DropDownListRentCommencementType.Items[0].Selected = true;
            	}
        	}
        	return;
		}
        
        
    }

    public void DeleteCommencement(string id)
    {        
        QuickPM.RentCommencement r = new QuickPM.RentCommencement(long.Parse(id));
        r.Delete();
        //Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void LoadTenant()
    {
        string tenantid = Request["TenantId"];
        if (tenantid == null)
        {
            return;
        }
        tenantid = QuickPM.Util.FormatTenantId(tenantid);
        tenant = new QuickPM.Tenant(tenantid);
		lease = new QuickPM.Lease(tenant.TenantId);		
    }

    protected void LoadLease()
    {
        if (tenant == null)
        {
            return;
        }
        QuickPM.Lease lease = new QuickPM.Lease(tenant.TenantId);        
        if (!lease.NewLease)
        {
            TextBoxLeaseExpiration.Text = lease.LeaseExpirationDate.ToShortDateString();
            TextBoxLeaseDocumentDate.Text = lease.DateOfLeaseDocument.ToShortDateString();
            TextBoxLeaseCommencement.Text = lease.LeaseCommencementDate.ToShortDateString();
            TextBoxLeaseNotes.Text = lease.Notes;
        }
    }

    protected void LoadCommencementDates()
    {
        if (tenant != null)
        {
            rentCommencements = QuickPM.ActiveRecord.Find<QuickPM.RentCommencement>("TenantId", tenant.TenantId);
        }
    }

    protected void LoadOption()
    {
        if (tenant != null && !newLeaseOption)
        {            
            leaseOption = new QuickPM.LeaseOption(tenant.TenantId);
			/*if (leaseOption.Id != -1) 
			{
				hasOptions = true;
			}*/
            if (leaseOption.ExercisedBy != "" || leaseOption.NotificationDate != 0 || 
			    leaseOption.NumberOptions != 0 || leaseOption.OptionLength != 0)
            {
				hasOptions = true;
                if (TextBoxOptionLength.Text.Trim() == "" && TextBoxNumberOptions.Text.Trim() == "" && 
				    TextBoxOptionNotificationDate.Text.Trim() == "") 
				{
					TextBoxOptionLength.Text = leaseOption.OptionLength.ToString();
					TextBoxNumberOptions.Text = leaseOption.NumberOptions.ToString();
                	TextBoxOptionNotificationDate.Text = leaseOption.NotificationDate.ToString();
                	TextBoxOptionExercisedBy.Text = leaseOption.ExercisedBy;
				}
                
            }
        }
    }

    protected void LinkButtonLeaseNotes_Click(object sender, EventArgs e)
    {

        if (tenant == null)
        {
            Session["Message"] = "Couldn't Save";
            return;
        }
        QuickPM.Lease lease = new QuickPM.Lease(tenant.TenantId);
        lease.Notes = TextBoxLeaseNotes.Text.Trim();
        lease.Save();
        Session["Message"] = "Saved";
    }

    protected void RentCommencementAdd_Click(object sender, EventArgs e)
    {
        if (tenant == null)
        {
            return;
        }
        int rentTypeIndex = Int32.Parse(DropDownListRentCommencementType.SelectedValue);
        DateTime date;
        if (!DateTime.TryParse(TextBoxDate.Text, out date))
        {
            Session["Message"] = "Please enter a date for the " + DropDownListRentCommencementType.SelectedItem.Text + " commencement";
            Response.Redirect(Request.Url.PathAndQuery);
            return;
        }
        QuickPM.RentCommencement rentCommencement = new QuickPM.RentCommencement();
        rentCommencement.Date = date;
        rentCommencement.RentTypeIndex = rentTypeIndex;
        rentCommencement.TenantId = tenant.TenantId;
        rentCommencement.Save();
        rentCommencements = QuickPM.ActiveRecord.Find<QuickPM.RentCommencement>("TenantId", tenant.TenantId);
        //Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void LinkButtonSubmit_Click(object sender, EventArgs e)
    {
        DateTime leaseDocumentDate = DateTime.MinValue;
        if (!DateTime.TryParse(TextBoxLeaseDocumentDate.Text, out leaseDocumentDate))
        {
            leaseDocumentDate = DateTime.MinValue;            
        }

        DateTime leaseCommencementDate = DateTime.MinValue;
        if (!DateTime.TryParse(TextBoxLeaseCommencement.Text, out leaseCommencementDate))
        {
            leaseCommencementDate = DateTime.MinValue;
        }

        DateTime leaseExpirationDate = DateTime.MinValue;
        if (!DateTime.TryParse(TextBoxLeaseExpiration.Text, out leaseExpirationDate))
        {
            leaseExpirationDate = DateTime.MinValue;
        }

        QuickPM.Lease lease = new QuickPM.Lease(tenant.TenantId);
        lease.DateOfLeaseDocument = leaseDocumentDate;
        lease.LeaseCommencementDate = leaseCommencementDate;
        lease.LeaseExpirationDate = leaseExpirationDate;
        lease.Save();
		this.lease = lease;
        Session["Message"] = "Saved";
        
    }

    protected void LinkButtonOptions_Click(object sender, EventArgs e)
    {
		if (leaseOption == null)
		{
			leaseOption = new QuickPM.LeaseOption(tenant.TenantId);
		}
		
		leaseOption.ExercisedBy = TextBoxOptionExercisedBy.Text.Trim();
		int notificationDate = 0;
		if (!int.TryParse(TextBoxOptionNotificationDate.Text, out notificationDate))
		{
			Session["Message"] = "<font color=\"red\">Please enter a number for the option notification date field</font>";
			return;
		}		
		leaseOption.NotificationDate = notificationDate;
		
		int numberOptions = 0;
		if (!int.TryParse(TextBoxNumberOptions.Text, out numberOptions))
		{
			Session["Message"] = "<font color=\"red\">Please enter a number the #options field</font>";
			return;
		}
		
		leaseOption.NumberOptions = numberOptions;
		
		leaseOption.ExercisedBy = TextBoxOptionExercisedBy.Text.Trim();
		
		int optionLength = 0;
		if (!int.TryParse(TextBoxOptionLength.Text, out optionLength))
		{
			Session["Message"] = "<font color=\"red\">Please enter a whole number for the option length</font>";
			return;
		}
		leaseOption.OptionLength = optionLength;
		
		leaseOption.Save();		
		hasOptions = true;
		return;		       
    }

}
