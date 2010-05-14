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

public partial class People_Person : System.Web.UI.Page
{

    protected bool adding = false;

    VolunteerTracker.Person contact = null;
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Request["NewContact"] != null)
        {
            return;
        }
        if (Request["Id"] == null)
        {
            return;
        }
        int id;
        if (!Int32.TryParse(Request["Id"], out id))
        {
            return;
        }
        contact = new VolunteerTracker.Person(id);
        if (!IsPostBack)
        {
            UpdatePersonControl();
        }
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        UpdateContact();
        if (Request["return"] != null)
        {
            Response.Redirect(Request["return"]);
        }
    }

    protected void UpdateContact()
    {
        if (Request["NewContact"] != null)
        {
            contact = new VolunteerTracker.Person();
            contact.AssociatedKey = Request["AssociatedKey"];
        }
        contact.CellPhone = ((Tenant_PersonControl)KeyContact).CellPhone;
        contact.Email = ((Tenant_PersonControl)KeyContact).Email;
        contact.Fax = ((Tenant_PersonControl)KeyContact).Fax;
        contact.HomePhone = ((Tenant_PersonControl)KeyContact).HomePhone;
        contact.Name = ((Tenant_PersonControl)KeyContact).Name;
        contact.OfficePhone = ((Tenant_PersonControl)KeyContact).OfficePhone;
        contact.Title = ((Tenant_PersonControl)KeyContact).Title;
        contact.Address = ((Tenant_PersonControl)KeyContact).Address;
        contact.Save();
    }

    protected void UpdatePersonControl()
    {
        ((Tenant_PersonControl)KeyContact).CellPhone = contact.CellPhone;
        ((Tenant_PersonControl)KeyContact).Email = contact.Email;
        ((Tenant_PersonControl)KeyContact).Fax = contact.Fax;
        ((Tenant_PersonControl)KeyContact).HomePhone = contact.HomePhone;
        ((Tenant_PersonControl)KeyContact).Name = contact.Name;
        ((Tenant_PersonControl)KeyContact).OfficePhone = contact.OfficePhone;
        ((Tenant_PersonControl)KeyContact).Title = contact.Title;
        ((Tenant_PersonControl)KeyContact).Address = contact.Address;

    }
}
