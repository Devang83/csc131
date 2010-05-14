
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
// using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
// using System.Xml.Linq;

public partial class Tenant_Tenant : System.Web.UI.UserControl
{
    protected QuickPM.Tenant tenant = null;
    protected List<QuickPM.Person> contacts = new List<QuickPM.Person>();    
    protected bool addTenant = false;

    public bool AddingTenant
    {
        get
        {
            return addTenant;
        }
        set
        {
            addTenant = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.MaintainScrollPositionOnPostBack = true;       
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);                
        
        string tenantId = Request["TenantId"];
		LoadTenant();
 		if (addTenant)
		{
			Units.Visible = false;
		}
		if (IsPostBack)
		{
			return;
		}	
 
        if (!addTenant && !IsPostBack)
        {
            LoadTenantValues();
        }
        else
        {
            TextBoxTenantId.Enabled = true;
            RadioButtonActive.Checked = true;
            RadioButtonInactive.Checked = false;
            if (Request["PropertyId"] != null)
            {
                TextBoxTenantId.Text = Request["PropertyId"] + "-";
            }
        }

        QuickPM.Tenant t = !addTenant ? new QuickPM.Tenant(tenantId) : null;		
        if (!addTenant && !t.ACL.CanWrite(QuickPM.Database.GetUserId()))
        {
            ButtonSubmit.Enabled = false;
            ButtonSubmit.Visible = false;
            RadioButtonActive.Enabled = false;
            RadioButtonActive.ForeColor = System.Drawing.Color.Black;
            RadioButtonInactive.Enabled = false;
            RadioButtonInactive.ForeColor = System.Drawing.Color.Black;
			
            QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
        }

        
    }

    
    /*protected List<string> SelectBillings()
    {
       // List<QuickPM.Bill> bills = QuickPM.Bill.GetBills(tenant.TenantId, 2900, 12);
        
       // foreach (QuickPM.Bill bill in bills)
        //{
                                        
        //}
        return new List<string>();
    }
    */
    private void LoadTenantValues()
    {
        LoadTenant();
        bool currentlyActive = DateTime.Now <= tenant.EndDate && tenant.CreatedDate <= DateTime.Now;
        contacts = QuickPM.Person.GetContacts(tenant);             
        RadioButtonActive.Checked = currentlyActive;
        RadioButtonInactive.Checked = !currentlyActive;
        TextBoxCity.Text = tenant.City;
        TextBoxLocation.Text = tenant.Address;
        TextBoxName.Text = tenant.Name;
        TextBoxTenantId.Text = tenant.TenantId;
        TextBoxTenantId.Enabled = false;
        TextBoxPhone.Text = tenant.Phone;
        TextBoxState.Text = tenant.State;                
        TextBoxBillingEmail.Text = tenant.BillingEmail;
        TextBoxZip.Text = tenant.Zip;
        //UpdatePersonControl(keyContact, KeyContact1);        
    }

    public bool SaveTenant()
    {

        string tenantid = Request["TenantId"];
        if (addTenant)
        {
            tenantid = TextBoxTenantId.Text;
        }
        if (tenantid == null)
        {
            return false;
        }               
        if (!QuickPM.Util.TryFormatTenantId(tenantid, out tenantid))
        {
            Session["Error"] = "<font color=\"red\">" + "Invalid tenant id" + "</font>";
            return false;
        }

        long property = QuickPM.Util.GetPropertyId(tenantid);
        long[] PropertyIds = QuickPM.Util.GetPropertyIds(true);
        bool hasProperty = false;
        foreach (int p in PropertyIds)
        {
            if (p == property)
            {
                hasProperty = true;
                break;
            }
        }
        if (!hasProperty)
        {
            Session["Error"] = "<font color=\"red\">" + "Property number " + property.ToString() + " does not exist" + "</font>";
            return false;
        }

        QuickPM.Tenant tmp = new QuickPM.Tenant(tenantid);
        if (!tmp.NewTenant && addTenant)
        {
            Session["Error"] = "<font color=\"red\">" + "Tenant already exists" + "</font>";
            return false;
        }

        if (!addTenant)
        {
            tenant = new QuickPM.Tenant(tenantid);
        }
        else
        {
            tenant = new QuickPM.Tenant();
            tenant.TenantId = tenantid;
            QuickPM.Person billingContact;
            billingContact = new QuickPM.Person();
            billingContact.Save();
            tenant.BillingKeyContactId = billingContact.Id;                        
        }                
        
        //keyContact = contacts[0];
        if (RadioButtonActive.Checked)
        {
            tenant.EndDate = DateTime.MaxValue;
        }
        else
        {
            if (tenant.EndDate >= DateTime.Today)
            {
                tenant.EndDate = DateTime.Today;
            }
        }        
        tenant.City = TextBoxCity.Text;
        //UpdateContact(keyContact, KeyContact1);
        tenant.Address = TextBoxLocation.Text;
        tenant.Name = TextBoxName.Text;
        tenant.Phone = TextBoxPhone.Text;
        tenant.State = TextBoxState.Text;
        tenant.Zip = TextBoxZip.Text;
        
        //keyContact.Save();               
        tenant.BillingEmail = TextBoxBillingEmail.Text;
        tenant.Save();
        Session["Message"] = "Saved";
        LoadTenant();
        return true;
    }    

    //public ASP.personcontrol_ascx BillingContact
    //{
    //    get
    //    {
    //        return BillingContact;
    //    }
    //}
     
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {

        if (!SaveTenant())
        {
            return;
        }
        if (this.addTenant)
        {
            Session["AddMessage"] = "<h2>Tenant Added</h2>";
            Session["AddTenantId"] = this.tenant.TenantId;
            Response.Redirect(ResolveUrl("~/Add/SuccessfulAdd.aspx"));
        }
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
        //SqlTenantBillingsData.SelectParameters["tenantId"].DefaultValue = tenant.TenantId;
        contacts = QuickPM.Person.GetContacts(tenant);
        //keyContact = contacts[0];
        
      
    }
    
    protected void RadioButtonActive_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButtonActive.Checked)
        {
            RadioButtonInactive.Checked = false;
        }
        SaveTenant();
        LoadTenant();
      
    }
    protected void RadioButtonInactive_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButtonInactive.Checked)
        {
            RadioButtonActive.Checked = false;
        }
        SaveTenant();
        LoadTenant();
      
    }

      
}
