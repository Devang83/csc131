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

public partial class Property_Documents : System.Web.UI.Page
{
    protected QuickPM.Property property = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.MaintainScrollPositionOnPostBack = true;
        
        // if (this.IsPostBack)
        // {
        //     return;
        // }
        string PropertyId = Request["PropertyId"];
        int num;
        if (PropertyId == null || !Int32.TryParse(PropertyId, out num))
        {
            return;
        }
        //if (!UserAuthorization.CanView(tenantId, this.Profile))
        //{
        //    throw new Exception("Can't view tenant");
        //}        
        LoadPropertyValues();
        
        ((Documents_DocumentsControl)Documents).DocumentIds = this.property;

    }

    private void LoadPropertyValues()
    {
        LoadProperty();

    }


    protected void LoadProperty()
    {
        string sPropertyId = Request["PropertyId"];
        if (sPropertyId == null)
        {
            return;
        }
        long PropertyId = long.Parse(sPropertyId);
        property = new QuickPM.Property(PropertyId);        
    }


}
