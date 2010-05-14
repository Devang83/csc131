using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Web.UI.WebControls;

public partial class Maps_Map : System.Web.UI.Page
{
    protected List<QuickPM.Property> properties = new List<QuickPM.Property>();
    protected void Page_Load(object sender, EventArgs e)
    {		
        List<object> places = new List<object>();

        foreach (QuickPM.Property p in QuickPM.Property.Util.GetProperties())
        {
            places.Add(p);
        }
        ((Maps_MapControl)Map).Places = places;
    }

  
}
