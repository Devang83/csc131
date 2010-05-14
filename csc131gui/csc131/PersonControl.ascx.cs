using System;
using System.Collections;
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

public partial class Tenant_PersonControl : System.Web.UI.UserControl
{
    public String Name
    {
        get
        {
            return this.TextBoxContactName.Text;
        }
        set
        {
            this.TextBoxContactName.Text = value.Trim();
        }
    }

    public String CellPhone
    {
        get
        {
            return this.TextBoxContactCellPhone.Text;
        }
        set
        {
            this.TextBoxContactCellPhone.Text = value.Trim();
        }
    }

    public String OfficePhone
    {
        get
        {
            return this.TextBoxContactOfficePhone.Text;
        }

        set
        {
            this.TextBoxContactOfficePhone.Text = value.Trim();
        }
    }

    public String HomePhone
    {
        get
        {
            return this.TextBoxContactHomePhone.Text;
        }

        set
        {
            this.TextBoxContactHomePhone.Text = value.Trim();
        }
    }


    public String Email
    {
        get
        {
            return this.TextBoxContactEmail.Text;
        }

        set
        {
            this.TextBoxContactEmail.Text = value.Trim();
        }
    }

    public String Fax
    {
        get
        {
            return this.TextBoxContactFax.Text;
        }

        set
        {
            this.TextBoxContactFax.Text = value.Trim();
        }
    }

    public String Title
    {
        get
        {
            return this.TextBoxContactTitle.Text;
        }

        set
        {
            this.TextBoxContactTitle.Text = value.Trim();
        }
    }

    public String Address
    {
        get
        {
            return this.TextBoxContactAddress.Text;
        }

        set
        {
            this.TextBoxContactAddress.Text = value.Trim();
        }
    }

    public string GroupingText
    {
        get
        {
            return this.Panel3.GroupingText;
        }
        set
        {
            this.Panel3.GroupingText = value.Trim();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
         
    }
}
