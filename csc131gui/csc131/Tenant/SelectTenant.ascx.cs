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

public partial class Tenant_SelectTenant : System.Web.UI.UserControl
{
    protected List<QuickPM.Tenant> tenants = null;
    protected List<QuickPM.Property> properties = null;
    public delegate void TextChangedDel(string selectedText);
    public TextChangedDel TextChanged = null;
    private bool visible = true;
    public override bool Visible
    {
        get
        {
            return visible;
        }
        set
        {
            visible = value;
            DropDownListTenant.Visible = visible;
            Label1.Visible = visible;
            
        }
    }

    public string TenantId
    {
        get
        {
            return DropDownListTenant.SelectedValue;
        }
    }



    protected void DropDownListTenant_TextChanged(object sender, EventArgs e)
    {
        string tenantId = DropDownListTenant.SelectedValue;
        if (TextChanged != null)
        {
            TextChanged(tenantId);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, this.Request);
        if (IsPostBack)
        {
            return;
        }
        tenants = QuickPM.Tenant.Find<QuickPM.Tenant>();

        foreach (QuickPM.Tenant tenant in tenants)
        {
            DropDownListTenant.Items.Add(new ListItem("" + tenant.TenantId + " (" + tenant.Name + ")", tenant.TenantId));
        }
        if (DropDownListTenant.Items.Count > 0 && !IsPostBack)
        {
            DropDownListTenant.Items[0].Selected = true;
        }


    }
}
