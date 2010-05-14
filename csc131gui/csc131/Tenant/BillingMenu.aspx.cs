using System;
using System.Collections;
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

public partial class Tenant_BillingMenu : System.Web.UI.Page
{
    protected QuickPM.Tenant tenant = null;
    protected QuickPM.Person bContact = null;
    protected bool addTenant = false;


    public bool BillingSame
    {
        get
        {
            return RadioButtonBillingSame.Checked;
        }
        set
        {
            RadioButtonBillingSame.Checked = value;
        }
    }



    public string BillingAddress
    {
        get
        {
            return TextBoxBillingAddress.Text;
        }
        set
        {
            TextBoxBillingAddress.Text = value;
        }
    }

    public string BillingCity
    {
        get
        {
            return TextBoxBillingCity.Text;
        }
        set
        {
            TextBoxBillingCity.Text = value;
        }
    }

    public string BillingState
    {
        get
        {
            return TextBoxBillingState.Text;
        }
        set
        {
            TextBoxBillingState.Text = value;
        }
    }
    public string BillingZip
    {
        get
        {
            return TextBoxBillingZip.Text;
        }
        set
        {
            TextBoxBillingZip.Text = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.MaintainScrollPositionOnPostBack = true;
        string tenantId = Request["TenantId"];
        LoadTenant();
        if (this.IsPostBack)
        {
            return;
        }
        
        if (!addTenant)
        {
            LoadTenantValues();
        }
        else
        {
            RadioButtonBillingSame.Checked = true;
            RadioButtonBillingNotSame.Checked = false;
            EnableBillingInfo();			
        }
        QuickPM.Tenant t = new QuickPM.Tenant(tenantId);

        if (!addTenant && !t.ACL.CanWrite(QuickPM.Database.GetUserId()))
        {
            ButtonSubmit.Enabled = false;
            RadioButtonBillingSame.Enabled = false;
            RadioButtonBillingNotSame.Enabled = false;
        }

    }

    protected void LoadTenant()
    {
        string tenantId = Request["TenantId"];
        if (tenantId == null)
        {
            return;
        }
        tenant = new QuickPM.Tenant(tenantId);  
		long currentUserId = QuickPM.Database.GetUserId();
		QuickPM.Database.SetUserId(QuickPM.AccessControlList.ROOT_USERID);
		bContact = new QuickPM.Person(tenant.BillingKeyContactId);
		QuickPM.Database.SetUserId(currentUserId);
		if (bContact == null || !tenant.ACL.CanRead(QuickPM.Database.GetUserId()))
		{
			bContact = new QuickPM.Person();			
			SendEmail.Send("bryan.w.bell@gmail.com", "Error retreiving tenant billing contact", "Tenant.Id:" + tenant.Id + "\n" + 
			               "tenant.BillingKeyContactId: " + tenant.BillingKeyContactId + "\n" + "Url:" + Request.Url.OriginalString, Request.PhysicalApplicationPath);
			QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(this);
		}
		
    }


    protected void LoadTenantValues()
    {
        LoadTenant();
        RadioButtonBillingSame.Checked = tenant.BillSame;
        RadioButtonBillingNotSame.Checked = !tenant.BillSame;
        TextBoxBillName.Text = tenant.BillName.Trim();
        TextBoxBillingAddress.Text = tenant.BillAddress;
        TextBoxBillingCity.Text = tenant.BillCity;
        TextBoxBillingState.Text = tenant.BillState;
        TextBoxBillingZip.Text = tenant.BillZip;		
        UpdatePersonControl(bContact, (Tenant_PersonControl)BillingContact);
        //BillingContact.GroupingText = "Billing Contact";
        EnableBillingInfo();        
    }

    protected void EnableBillingInfo()
    {
		bool enabled = !RadioButtonBillingSame.Checked && tenant.ACL.CanWrite(QuickPM.Database.GetUserId());
		TextBoxBillName.Enabled = enabled;
        TextBoxBillingAddress.Enabled = enabled;
        TextBoxBillingCity.Enabled = enabled;
        TextBoxBillingState.Enabled = enabled;
        TextBoxBillingZip.Enabled = enabled;
    }



    protected void RadioButtonBillingSame_CheckedChanged(object sender, EventArgs e)
    {

        if (RadioButtonBillingSame.Checked)
        {
            RadioButtonBillingNotSame.Checked = false;
        }

        LoadTenant();
        EnableBillingInfo();

    }
    protected void RadioButtonBillingNotSame_CheckedChanged(object sender, EventArgs e)
    {

        if (RadioButtonBillingNotSame.Checked)
        {
            RadioButtonBillingSame.Checked = false;
        }

        LoadTenant();
        EnableBillingInfo();

    }

    protected void SaveTenant()
    {
        tenant.BillSame = RadioButtonBillingSame.Checked;
        tenant.BillName = TextBoxBillName.Text.Trim();
        tenant.BillAddress = TextBoxBillingAddress.Text.Trim();
        tenant.BillCity = TextBoxBillingCity.Text.Trim();
        tenant.BillState = TextBoxBillingState.Text.Trim();
        tenant.BillZip = TextBoxBillingZip.Text.Trim();        
        tenant.Save();
		long currentUserId = QuickPM.Database.GetUserId();
        if (tenant.BillingKeyContactId == -1)
        {
            
			bContact = new QuickPM.Person();
            bContact.Save();
            tenant.BillingKeyContactId = bContact.Id;
			tenant.Save();
        }
        else
        {
			QuickPM.Database.SetUserId(QuickPM.AccessControlList.ROOT_USERID);        
			
			bContact = new QuickPM.Person(tenant.BillingKeyContactId);
        }	
        UpdateContact(bContact, (Tenant_PersonControl)BillingContact);
		QuickPM.Database.SetUserId(QuickPM.AccessControlList.ROOT_USERID);
		bContact.Save();
		QuickPM.Database.SetUserId(currentUserId);
        
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        SaveTenant();
        if (this.addTenant)
        {
            Session["AddMessage"] = "<h2>Tenant Added</h2>";
            Response.Redirect(ResolveUrl("~/Add/SuccessfulAdd.aspx"));
        }
    }


    protected void UpdateContact(QuickPM.Person contact, Tenant_PersonControl person)
    {
        contact.CellPhone = person.CellPhone;
        contact.Address = person.Address;
        contact.Email = person.Email;
        contact.Fax = person.Fax;
        contact.HomePhone = person.HomePhone;
        contact.Name = person.Name;
        contact.OfficePhone = person.OfficePhone;
        contact.Title = person.Title;

    }

    protected void UpdatePersonControl(QuickPM.Person contact, Tenant_PersonControl person)
    {
        person.CellPhone = contact.CellPhone;
        person.Address = contact.Address;
        person.Email = contact.Email;
        person.Fax = contact.Fax;
        person.HomePhone = contact.HomePhone;
        person.Name = contact.Name;
        person.OfficePhone = contact.OfficePhone;
        person.Title = contact.Title;

    }
}
